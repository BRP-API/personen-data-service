using Microsoft.AspNetCore.Http;
using Rvig.Data.Base.Postgres.Authorisation;
using Rvig.Data.Base.Postgres.DatabaseModels;
using Rvig.Data.Personen.Mappers;
using Rvig.Data.Personen.Repositories;
using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Shared.Exceptions;
using Rvig.HaalCentraalApi.Personen.RequestModels.BRP;
using Rvig.HaalCentraalApi.Personen.Interfaces;
using Rvig.HaalCentraalApi.Shared.Interfaces;
using Rvig.HaalCentraalApi.Personen.Helpers;
using Rvig.Data.Base.Postgres.Services;
using Rvig.Data.Base.Postgres.Repositories;
using Rvig.Data.Base.Services;

namespace Rvig.Data.Personen.Services;
public class GetAndMapGbaPersonenService : GetAndMapGbaServiceBase, IGetAndMapGbaPersonenService
{
	private readonly IRvigPersoonRepo _dbPersoonRepo;
	private readonly IRvigPersoonBeperktRepo _dbPersoonBeperktRepo;
	private readonly IRvIGDataPersonenMapper _persoonMapper;

    public GetAndMapGbaPersonenService(IAutorisationRepo autorisationRepo, IRvigPersoonRepo dbPersoonRepo, IRvigPersoonBeperktRepo dbPersoonBeperktRepo, IRvIGDataPersonenMapper persoonMapper,
		IHttpContextAccessor httpContextAccessor, IProtocolleringService protocolleringService)
		: base(httpContextAccessor, autorisationRepo, protocolleringService)
	{
		_dbPersoonRepo = dbPersoonRepo;
		_dbPersoonBeperktRepo = dbPersoonBeperktRepo;
		_persoonMapper = persoonMapper;
	}

	public async Task<(IEnumerable<(GbaPersoon persoon, long pl_id)>? personenPlIds, int afnemerCode)> GetPersonenMapByBsns(IEnumerable<string>? burgerservicenummers, string? gemeenteVanInschrijving, List<string> fields, bool checkAuthorization)
	{
		Afnemer afnemer = new();
		DbAutorisatie autorisatie = new();
		if (checkAuthorization)
		{
			(afnemer, autorisatie) = await GetAfnemerAutorisatie();
		}
		var isBinnenGemeentelijk = AuthorisationService.IsBinnenGemeentelijk(afnemer.Gemeentecode, gemeenteVanInschrijving);
		var fieldsRubrieken = PersonenApiToRubriekCategoryHelper.ConvertFieldsToRubriekCategory(fields, false);
		List<int> authorizedRubrieken = new();
		if (checkAuthorization)
		{
			authorizedRubrieken = AuthorisationService.GetAuthorizedRubrieken(autorisatie);
		}

		if (checkAuthorization && !isBinnenGemeentelijk)
		{
			AuthorisationService.CheckAuthorizationSearchRubrieken(authorizedRubrieken, fieldsRubrieken);

			if (string.IsNullOrWhiteSpace(autorisatie.ad_hoc_rubrieken))
			{
				throw new UnauthorizationParameterException($"U bent niet geautoriseerd voor het gebruik van parameter(s).");
			}

			var invalidParams = new List<string>();
			var adHocRubrieken = autorisatie.ad_hoc_rubrieken.Split(" ");
			if (burgerservicenummers?.Any() == true && !adHocRubrieken.Any(rubriek => rubriek.Contains("10120")))
			{
				invalidParams.Add(nameof(RaadpleegMetBurgerservicenummer.burgerservicenummer));
			}
			if (!string.IsNullOrWhiteSpace(gemeenteVanInschrijving) && !adHocRubrieken.Any(rubriek => rubriek.Contains("80910")))
			{
				invalidParams.Add(nameof(RaadpleegMetBurgerservicenummer.gemeenteVanInschrijving));
			}

			var invalidParamsString = string.Join(", ", invalidParams.OrderBy(param => param));

			if (!string.IsNullOrWhiteSpace(invalidParamsString))
			{
				throw new UnauthorizationParameterException($"U bent niet geautoriseerd voor het gebruik van parameter(s): {invalidParamsString}.");
			}
		}

		// It is impossible to have an empty or null array of bsns because the API request models already validate this and reject all non valid values.
		var dbPersonen = await _dbPersoonRepo.GetPersoonByBsns(burgerservicenummers!, gemeenteVanInschrijving, fields);
		var personenPlIds = (await Task.WhenAll(dbPersonen.Select(async dbPersoon =>
		{
			if (dbPersoon == null)
			{
				return default;
			}
			var persoonFiltered = checkAuthorization ? AuthorisationService.Apply(dbPersoon, autorisatie!, isBinnenGemeentelijk, authorizedRubrieken) : dbPersoon;

			if (persoonFiltered != null)
			{
				return (gbaPersoon: await _persoonMapper.MapFrom(persoonFiltered), dbPersoon.Persoon.pl_id);
			}

			return default;
		})))
        .Where(x => !x.gbaPersoon.Equals(default));

		if (!personenPlIds.Any())
		{
			return default;
		}

        // Check above already makes sure that a null value in the list isn't possible.
        return (personenPlIds.Select(x => (x.gbaPersoon, x.pl_id)), autorisatie.afnemer_code);
    }

	public async Task<(IEnumerable<(T persoon, long pl_id)>? personenPlIds, int afnemerCode)> GetMapZoekPersonen<T>(PersonenQuery model, List<string> fields, bool checkAuthorization) where T : GbaPersoonBeperkt
    {
        return model switch
        {
            ZoekMetGeslachtsnaamEnGeboortedatum => await GetMapZoekPersonenBase<T>(model, fields, checkAuthorization),
            ZoekMetNaamEnGemeenteVanInschrijving => await GetMapZoekPersonenBase<T>(model, fields, checkAuthorization),
            ZoekMetNummeraanduidingIdentificatie => await GetMapZoekPersonenBase<T>(model, fields, checkAuthorization, true),
            ZoekMetPostcodeEnHuisnummer => await GetMapZoekPersonenBase<T>(model, fields, checkAuthorization, true),
            ZoekMetStraatHuisnummerEnGemeenteVanInschrijving => await GetMapZoekPersonenBase<T>(model, fields, checkAuthorization, true),
			ZoekMetAdresseerbaarObjectIdentificatie => await GetMapZoekPersonenBase<T>(model, fields, checkAuthorization, true),
			_ => throw new CustomInvalidOperationException($"Onbekend type query: {model}"),
        };
    }

	/// <summary>
	/// Get personen on search criteria.
	/// </summary>
	/// <param name="model"></param>
	/// <param name="checkAdresVraagBevoegdheid"></param>
	/// <returns>List of persons with restricted data.</returns>
	/// <exception cref="AuthorizationException"></exception>
	private async Task<(IEnumerable<(T persoon, long pl_id)>? personenPlIds, int afnemerCode)> GetMapZoekPersonenBase<T>(PersonenQuery model, List<string> fields, bool checkAuthorization, bool checkAdresVraagBevoegdheid = false) where T : GbaPersoonBeperkt
	{
		Afnemer afnemer = new();
		DbAutorisatie autorisatie = new();
		if (checkAuthorization)
		{
			(afnemer, autorisatie) = await GetAfnemerAutorisatie();
		}
		var isBinnenGemeentelijk = AuthorisationService.IsBinnenGemeentelijk(afnemer.Gemeentecode, model.gemeenteVanInschrijving);
		var fieldsRubrieken = PersonenApiToRubriekCategoryHelper.ConvertFieldsToRubriekCategory(fields, false);
		List<int> authorizedRubrieken = new();
		if (checkAuthorization)
		{
			authorizedRubrieken = AuthorisationService.GetAuthorizedRubrieken(autorisatie);
		}
		if (checkAuthorization && !isBinnenGemeentelijk)
		{
			AuthorisationService.CheckAuthorizationSearchRubrieken(authorizedRubrieken, fieldsRubrieken);

			var (IsParamAuthorized, InvalidParams) = CheckParamAuthorization(model, autorisatie);

			if (!IsParamAuthorized)
			{
				throw new UnauthorizationParameterException($"U bent niet geautoriseerd voor het gebruik van parameter(s): {InvalidParams}.");
			}
			// This wasn't necessary anymore but I doubt it will be gone forever.
			//if (checkAdresVraagBevoegdheid && autorisatie.adres_vraag_bevoegdheid != 1)
			//{
			//	throw new AuthorisationException("Niet geautoriseerd voor adresbevragingen.");
			//}
		}

		var dbPersonen = await _dbPersoonBeperktRepo.SearchPersonen(model, fields);
		var personenPlIds = (await Task.WhenAll(dbPersonen.Select(async dbPersoon =>
		{
			if (dbPersoon == null)
			{
				return default;
			}
			var persoonFiltered = checkAuthorization ? AuthorisationService.Apply(dbPersoon, autorisatie!, isBinnenGemeentelijk, authorizedRubrieken) : dbPersoon;

			if (persoonFiltered != null)
			{
				return (gbaPersoon: await _persoonMapper.MapGbaPersoonBeperkt<T>(persoonFiltered), dbPersoon.Persoon.pl_id);
			}

			return default;
		})))
		.Where(x => !x.gbaPersoon.Equals(default));

		if (!personenPlIds.Any())
		{
			return default;
		}

		// Check above already makes sure that a null value in the list isn't possible.
		return (personenPlIds.Select(x => (x.gbaPersoon, x.pl_id)), autorisatie.afnemer_code);
	}

	private static (bool IsParamAuthorized, string? InvalidParams) CheckParamAuthorization(PersonenQuery model, DbAutorisatie afnemerAutorisatie)
	{
		if (string.IsNullOrWhiteSpace(afnemerAutorisatie.ad_hoc_rubrieken))
		{
			return (true, null);
		}
		var adHocRubrieken = afnemerAutorisatie.ad_hoc_rubrieken.Split(" ");
		var invalidParams = new List<string>();
		var isParamAuthorized = true;
		switch (model)
		{
			case ZoekMetGeslachtsnaamEnGeboortedatum zoekMetGeslachtsnaamEnGeboortedatumModel:
				if (!string.IsNullOrWhiteSpace(zoekMetGeslachtsnaamEnGeboortedatumModel.geslachtsnaam)
					&& !adHocRubrieken.Any(rubriek => rubriek.Contains("10240")))
				{
					invalidParams.Add(nameof(ZoekMetGeslachtsnaamEnGeboortedatum.geslachtsnaam));
					isParamAuthorized = false;
				}
				if (!string.IsNullOrWhiteSpace(zoekMetGeslachtsnaamEnGeboortedatumModel.voornamen)
					&& !adHocRubrieken.Any(rubriek => rubriek.Contains("10210")))
				{
					invalidParams.Add(nameof(ZoekMetGeslachtsnaamEnGeboortedatum.voornamen));
					isParamAuthorized = false;
				}
				if (!string.IsNullOrWhiteSpace(zoekMetGeslachtsnaamEnGeboortedatumModel.gemeenteVanInschrijving)
					&& !adHocRubrieken.Any(rubriek => rubriek.Contains("80910")))
				{
					invalidParams.Add(nameof(ZoekMetGeslachtsnaamEnGeboortedatum.gemeenteVanInschrijving));
					isParamAuthorized = false;
				}
				if (!string.IsNullOrWhiteSpace(zoekMetGeslachtsnaamEnGeboortedatumModel.geslacht)
					&& !adHocRubrieken.Any(rubriek => rubriek.Contains("10410")))
				{
					invalidParams.Add(nameof(ZoekMetGeslachtsnaamEnGeboortedatum.geslacht));
					isParamAuthorized = false;
				}
				if (!string.IsNullOrWhiteSpace(zoekMetGeslachtsnaamEnGeboortedatumModel.geboortedatum)
					&& !adHocRubrieken.Any(rubriek => rubriek.Contains("10310")))
				{
					invalidParams.Add(nameof(ZoekMetGeslachtsnaamEnGeboortedatum.geboortedatum));
					isParamAuthorized = false;
				}
				if (!string.IsNullOrWhiteSpace(zoekMetGeslachtsnaamEnGeboortedatumModel.voorvoegsel)
					&& !adHocRubrieken.Any(rubriek => rubriek.Contains("10230")))
				{
					invalidParams.Add(nameof(ZoekMetGeslachtsnaamEnGeboortedatum.voorvoegsel));
					isParamAuthorized = false;
				}
				break;
			case ZoekMetNaamEnGemeenteVanInschrijving zoekMetNaamEnGemeenteVanInschrijvingModel:
				if (!string.IsNullOrWhiteSpace(zoekMetNaamEnGemeenteVanInschrijvingModel.geslachtsnaam)
					&& !adHocRubrieken.Any(rubriek => rubriek.Contains("10240")))
				{
					invalidParams.Add(nameof(ZoekMetNaamEnGemeenteVanInschrijving.geslachtsnaam));
					isParamAuthorized = false;
				}
				if (!string.IsNullOrWhiteSpace(zoekMetNaamEnGemeenteVanInschrijvingModel.voornamen)
					&& !adHocRubrieken.Any(rubriek => rubriek.Contains("10210")))
				{
					invalidParams.Add(nameof(ZoekMetNaamEnGemeenteVanInschrijving.voornamen));
					isParamAuthorized = false;
				}
				if (!string.IsNullOrWhiteSpace(zoekMetNaamEnGemeenteVanInschrijvingModel.gemeenteVanInschrijving)
					&& !adHocRubrieken.Any(rubriek => rubriek.Contains("80910")))
				{
					invalidParams.Add(nameof(ZoekMetNaamEnGemeenteVanInschrijving.gemeenteVanInschrijving));
					isParamAuthorized = false;
				}
				if (!string.IsNullOrWhiteSpace(zoekMetNaamEnGemeenteVanInschrijvingModel.geslacht)
					&& !adHocRubrieken.Any(rubriek => rubriek.Contains("10410")))
				{
					invalidParams.Add(nameof(ZoekMetNaamEnGemeenteVanInschrijving.geslacht));
					isParamAuthorized = false;
				}
				if (!string.IsNullOrWhiteSpace(zoekMetNaamEnGemeenteVanInschrijvingModel.voorvoegsel)
					&& !adHocRubrieken.Any(rubriek => rubriek.Contains("10230")))
				{
					invalidParams.Add(nameof(ZoekMetNaamEnGemeenteVanInschrijving.voorvoegsel));
					isParamAuthorized = false;
				}
				break;
			case ZoekMetNummeraanduidingIdentificatie zoekMetNummeraanduidingIdentificatieModel:
				if (!string.IsNullOrWhiteSpace(zoekMetNummeraanduidingIdentificatieModel.gemeenteVanInschrijving)
					&& !adHocRubrieken.Any(rubriek => rubriek.Contains("80910")))
				{
					invalidParams.Add(nameof(ZoekMetNummeraanduidingIdentificatie.gemeenteVanInschrijving));
					isParamAuthorized = false;
				}
				if (!string.IsNullOrWhiteSpace(zoekMetNummeraanduidingIdentificatieModel.nummeraanduidingIdentificatie)
					&& !adHocRubrieken.Any(rubriek => rubriek.Contains("81190")))
				{
					invalidParams.Add(nameof(ZoekMetNummeraanduidingIdentificatie.nummeraanduidingIdentificatie));
					isParamAuthorized = false;
				}
				break;
			case ZoekMetPostcodeEnHuisnummer zoekMetPostcodeEnHuisnummerModel:
				if (!string.IsNullOrWhiteSpace(zoekMetPostcodeEnHuisnummerModel.gemeenteVanInschrijving)
					&& !adHocRubrieken.Any(rubriek => rubriek.Contains("80910")))
				{
					invalidParams.Add(nameof(ZoekMetPostcodeEnHuisnummer.gemeenteVanInschrijving));
					isParamAuthorized = false;
				}
				if (zoekMetPostcodeEnHuisnummerModel.huisnummer.HasValue && zoekMetPostcodeEnHuisnummerModel.huisnummer.Value != 0
					&& !adHocRubrieken.Any(rubriek => rubriek.Contains("81120")))
				{
					invalidParams.Add(nameof(ZoekMetPostcodeEnHuisnummer.huisnummer));
					isParamAuthorized = false;
				}
				if (!string.IsNullOrWhiteSpace(zoekMetPostcodeEnHuisnummerModel.huisletter)
					&& !adHocRubrieken.Any(rubriek => rubriek.Contains("81130")))
				{
					invalidParams.Add(nameof(ZoekMetPostcodeEnHuisnummer.huisletter));
					isParamAuthorized = false;
				}
				if (!string.IsNullOrWhiteSpace(zoekMetPostcodeEnHuisnummerModel.huisnummertoevoeging)
					&& !adHocRubrieken.Any(rubriek => rubriek.Contains("81140")))
				{
					invalidParams.Add(nameof(ZoekMetPostcodeEnHuisnummer.huisnummertoevoeging));
					isParamAuthorized = false;
				}
				if (!string.IsNullOrWhiteSpace(zoekMetPostcodeEnHuisnummerModel.postcode)
					&& !adHocRubrieken.Any(rubriek => rubriek.Contains("81160")))
				{
					invalidParams.Add(nameof(ZoekMetPostcodeEnHuisnummer.postcode));
					isParamAuthorized = false;
				}
				if (!string.IsNullOrWhiteSpace(zoekMetPostcodeEnHuisnummerModel.geboortedatum)
					&& !adHocRubrieken.Any(rubriek => rubriek.Contains("10310")))
				{
					invalidParams.Add(nameof(ZoekMetGeslachtsnaamEnGeboortedatum.geboortedatum));
					isParamAuthorized = false;
				}
				break;
			case ZoekMetStraatHuisnummerEnGemeenteVanInschrijving zoekMetStraatHuisnummerEnGemeenteVanInschrijvingModel:
				if (!string.IsNullOrWhiteSpace(zoekMetStraatHuisnummerEnGemeenteVanInschrijvingModel.gemeenteVanInschrijving)
					&& !adHocRubrieken.Any(rubriek => rubriek.Contains("80910")))
				{
					invalidParams.Add(nameof(ZoekMetStraatHuisnummerEnGemeenteVanInschrijving.gemeenteVanInschrijving));
					isParamAuthorized = false;
				}
				if (!string.IsNullOrWhiteSpace(zoekMetStraatHuisnummerEnGemeenteVanInschrijvingModel.straat)
					&& !adHocRubrieken.Any(rubriek => rubriek.Contains("81110")))
				{
					invalidParams.Add(nameof(ZoekMetStraatHuisnummerEnGemeenteVanInschrijving.straat));
					isParamAuthorized = false;
				}
				if (zoekMetStraatHuisnummerEnGemeenteVanInschrijvingModel.huisnummer != 0
					&& !adHocRubrieken.Any(rubriek => rubriek.Contains("81120")))
				{
					invalidParams.Add(nameof(ZoekMetStraatHuisnummerEnGemeenteVanInschrijving.huisnummer));
					isParamAuthorized = false;
				}
				if (!string.IsNullOrWhiteSpace(zoekMetStraatHuisnummerEnGemeenteVanInschrijvingModel.huisletter)
					&& !adHocRubrieken.Any(rubriek => rubriek.Contains("81130")))
				{
					invalidParams.Add(nameof(ZoekMetStraatHuisnummerEnGemeenteVanInschrijving.huisletter));
					isParamAuthorized = false;
				}
				if (!string.IsNullOrWhiteSpace(zoekMetStraatHuisnummerEnGemeenteVanInschrijvingModel.huisnummertoevoeging)
					&& !adHocRubrieken.Any(rubriek => rubriek.Contains("81140")))
				{
					invalidParams.Add(nameof(ZoekMetStraatHuisnummerEnGemeenteVanInschrijving.huisnummertoevoeging));
					isParamAuthorized = false;
				}
				break;
			case ZoekMetAdresseerbaarObjectIdentificatie zoekMetAdresseerbaarObjectIdentificatie:
				if (!string.IsNullOrWhiteSpace(zoekMetAdresseerbaarObjectIdentificatie.gemeenteVanInschrijving)
					&& !adHocRubrieken.Any(rubriek => rubriek.Contains("80910")))
				{
					invalidParams.Add(nameof(ZoekMetAdresseerbaarObjectIdentificatie.gemeenteVanInschrijving));
					isParamAuthorized = false;
				}
				if (!string.IsNullOrWhiteSpace(zoekMetAdresseerbaarObjectIdentificatie.adresseerbaarObjectIdentificatie)
					&& !adHocRubrieken.Any(rubriek => rubriek.Contains("81180")))
				{
					invalidParams.Add(nameof(ZoekMetAdresseerbaarObjectIdentificatie.adresseerbaarObjectIdentificatie));
					isParamAuthorized = false;
				}
				break;
			default:
				throw new CustomInvalidOperationException($"Onbekend type query: {model}");
		}

		return (isParamAuthorized, string.Join(", ", invalidParams.OrderBy(param => param)));
	}
}
