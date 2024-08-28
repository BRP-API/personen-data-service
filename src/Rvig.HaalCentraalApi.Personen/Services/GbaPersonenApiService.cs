using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.Extensions.Options;
using Rvig.Api.Gezag.ResponseModels;
using Rvig.Data.Base.Gezag.Repositories;
using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Personen.Fields;
using Rvig.HaalCentraalApi.Personen.Helpers;
using Rvig.HaalCentraalApi.Personen.Interfaces;
using Rvig.HaalCentraalApi.Personen.RequestModels.BRP;
using Rvig.HaalCentraalApi.Personen.ResponseModels.BRP;
using Rvig.HaalCentraalApi.Personen.Validation;
using Rvig.HaalCentraalApi.Shared.ApiModels.PersonenHistorieBase;
using Rvig.HaalCentraalApi.Shared.ApiModels.Universal;
using Rvig.HaalCentraalApi.Shared.Exceptions;
using Rvig.HaalCentraalApi.Shared.Fields;
using Rvig.HaalCentraalApi.Shared.Helpers;
using Rvig.HaalCentraalApi.Shared.Interfaces;
using Rvig.HaalCentraalApi.Shared.Options;
using Rvig.HaalCentraalApi.Shared.Services;
using Rvig.HaalCentraalApi.Shared.Util;

namespace Rvig.HaalCentraalApi.Personen.Services;
public interface IGbaPersonenApiService
{
	Task<(PersonenQueryResponse personenResponse, List<long>? plIds)> GetPersonen(PersonenQuery model);
}

public class GbaPersonenApiService : BaseApiService, IGbaPersonenApiService
{
	protected IGetAndMapGbaPersonenService _getAndMapPersoonService;
	private readonly IRepoGezagsrelatie _gezagsrelatieRepo;
	private readonly IGezagTransformer _gezagTransformer;
	private readonly GbaPersonenApiHelper _gbaPersonenApiHelper;
	private readonly GbaPersonenBeperktApiHelper _gbaPersonenBeperktApiHelper;

	// We didn't want to split personen and beperkt because they use the same operation. Too much of a hassle.
	// Because of this we ignored this abstract class implementation in favor for two seperate fields to make things easier.
	protected override FieldsSettings _fieldsSettings => throw new CustomNotImplementedException();

	private static PersonenFieldsSettings _persoonFieldsSettings => new();
	private static PersonenBeperktFieldsSettings _persoonBeperktFieldsSettings => new();

	public GbaPersonenApiService(IGetAndMapGbaPersonenService getAndMapPersoonService, IDomeinTabellenRepo domeinTabellenRepo, IProtocolleringService protocolleringService
		, ILoggingHelper loggingHelper, IRepoGezagsrelatie gezagsrelatieRepo, IGezagTransformer gezagTransformer, IOptions<ProtocolleringAuthorizationOptions> protocolleringAuthorizationOptions)
		: base(domeinTabellenRepo, protocolleringService, loggingHelper, protocolleringAuthorizationOptions)
	{
		_getAndMapPersoonService = getAndMapPersoonService;
		_gezagsrelatieRepo = gezagsrelatieRepo;
		_gezagTransformer = gezagTransformer;
		_gbaPersonenApiHelper = new GbaPersonenApiHelper(_fieldsExpandFilterService, _persoonFieldsSettings);
		_gbaPersonenBeperktApiHelper = new GbaPersonenBeperktApiHelper(_persoonBeperktFieldsSettings);
	}

	/// <summary>
	/// Get BRP (GBA) 2.0.0 personen via child of PersonenQuery.
	/// </summary>
	/// <param name="model">Child of PersonenQuery.</param>
	/// <returns>Child of PersonenQueryResponse</returns>
	/// <exception cref="InvalidOperationException"></exception>
	public async Task<(PersonenQueryResponse personenResponse, List<long>? plIds)> GetPersonen(PersonenQuery model)
	{
		return model switch
		{
			RaadpleegMetBurgerservicenummer raadpleegMetBurgerservicenummer => await GetPersonen(raadpleegMetBurgerservicenummer),
			ZoekMetGeslachtsnaamEnGeboortedatum zoekMetGeslachtsnaamEnGeboortedatum => await GetPersonenBeperkt(zoekMetGeslachtsnaamEnGeboortedatum),
			ZoekMetNaamEnGemeenteVanInschrijving zoekMetNaamEnGemeenteVanInschrijving => await GetPersonenBeperkt(zoekMetNaamEnGemeenteVanInschrijving),
			ZoekMetNummeraanduidingIdentificatie zoekMetNummeraanduidingIdentificatie => await GetPersonenBeperkt(zoekMetNummeraanduidingIdentificatie),
			ZoekMetPostcodeEnHuisnummer zoekMetPostcodeEnHuisnummer => await GetPersonenBeperkt(zoekMetPostcodeEnHuisnummer),
			ZoekMetStraatHuisnummerEnGemeenteVanInschrijving zoekMetStraatHuisnummerEnGemeenteVanInschrijving => await GetPersonenBeperkt(zoekMetStraatHuisnummerEnGemeenteVanInschrijving),
			ZoekMetAdresseerbaarObjectIdentificatie zoekMetAdresseerbaarObjectIdentificatie => await GetPersonenBeperkt(zoekMetAdresseerbaarObjectIdentificatie),
			_ => throw new CustomInvalidOperationException($"Onbekend type query: {model}"),
		};
	}

	/// <summary>
	/// Get personen via bsn(s).
	/// </summary>
	/// <param name="model"></param>
	/// <returns>Response object with list of complete person data</returns>
	private async Task<(RaadpleegMetBurgerservicenummerResponse personenResponse, List<long>? plIds)> GetPersonen(RaadpleegMetBurgerservicenummer model)
	{
		// Validation
		ValidationHelper.ValidateBurgerservicenummers(model.burgerservicenummer);

		await ValidationHelper.ValidateGemeenteInschrijving(model.gemeenteVanInschrijving, _domeinTabellenRepo);

		_fieldsExpandFilterService.ValidateScope(typeof(GbaPersoon), _persoonFieldsSettings.GbaFieldsSettings, model.fields);

		// Get personen
		var fieldsToUseForAuthorisations = model.fields.ConvertAll(field => _persoonFieldsSettings.GbaFieldsSettings.ShortHandMappings.ContainsKey(field)
			? _persoonFieldsSettings.GbaFieldsSettings.ShortHandMappings[field]
			: field);

		(IEnumerable<(GbaPersoon persoon, long pl_id)>? personenPlIds, int afnemerCode) = await _getAndMapPersoonService.GetPersonenMapByBsns(model.burgerservicenummer, model.gemeenteVanInschrijving, fieldsToUseForAuthorisations, _protocolleringAuthorizationOptions.Value.UseAuthorizationChecks);

		// Filter response by fields
		if (model?.fields?.Any() == true && personenPlIds != null)
		{
			_gbaPersonenApiHelper.HackLogicKinderenPartnersOuders(model.fields, personenPlIds?.ToList()?.ConvertAll(gbaPersoon => gbaPersoon.persoon));

			personenPlIds = (await Task.WhenAll(personenPlIds!.Select(async x =>
			{
				if (model.fields.Any(field => field.Contains("gezag", StringComparison.CurrentCultureIgnoreCase) && !field.StartsWith("indicatieGezagMinderjarige"))
				&& !string.IsNullOrWhiteSpace(x.persoon.Burgerservicenummer))
                {
                    var gezagsrelaties = (await _gezagsrelatieRepo.GetGezagsrelaties(x.persoon.Burgerservicenummer))?.ToList() ?? new List<Gezagsrelatie>();
                    bool? underage = gezagsrelaties.Count > 0 ? gezagsrelaties.Any(gezagsrelatie => gezagsrelatie.BsnMinderjarige == x.persoon.Burgerservicenummer) : null;
					int? leeftijd = 0;
					if (x.persoon.Geboorte != null)
					{
						leeftijd = CalculateAge(new DatumOnvolledig(x.persoon.Geboorte.Datum));
					}

					x.persoon.Gezag = new List<AbstractGezagsrelatie>();
					if (leeftijd < 18 && (underage == true || !underage.HasValue))
					{
						x.persoon.Gezag = await MapGezagMinderjarige(x.persoon.Burgerservicenummer, x.persoon.Partners, x.persoon.HistorischePartners, x.persoon.Kinderen, x.persoon.Ouders, gezagsrelaties, model.gemeenteVanInschrijving);
					}
					else
					{
						x.persoon.Gezag = await MapGezagMeerderjarige(x.persoon.Burgerservicenummer, x.persoon.GemeenteVanInschrijving, x.persoon.Partners, x.persoon.HistorischePartners, x.persoon.Kinderen, x.persoon.Ouders, gezagsrelaties, model.gemeenteVanInschrijving);
					}
				}

				x.persoon.Rni = GbaPersonenApiHelperBase.ApplyRniLogic(model.fields, x.persoon.Rni, _persoonFieldsSettings.GbaFieldsSettings);
				if (x.persoon.Verblijfplaats != null)
				{
					GbaPersonenApiHelper.AddVerblijfplaatsInOnderzoek(model.fields, x.persoon.Verblijfplaats);
				}
				if (x.persoon.Immigratie != null)
				{
					GbaPersonenApiHelper.AddLandVanwaarIngeschreven(model.fields, x.persoon.Immigratie);
				}

				model.fields = GbaPersonenApiHelper.OpschortingBijhoudingLogicRaadplegen(model.fields, x.persoon.OpschortingBijhouding);

				x.persoon = _fieldsExpandFilterService.ApplyScope(x.persoon, string.Join(",", model.fields), _persoonFieldsSettings.GbaFieldsSettings);
				_gbaPersonenApiHelper.RemoveInOnderzoekFromPersonCategoryIfNoFieldsAreRequested(model.fields, x.persoon);
				_gbaPersonenApiHelper.RemoveInOnderzoekFromGezagsverhoudingCategoryIfNoFieldsAreRequested(model.fields, x.persoon);

				return x;
			}))).ToList();

			if (_protocolleringAuthorizationOptions.Value.UseProtocollering)
			{
				await LogProtocolleringInDb(afnemerCode, personenPlIds?.Select(x => x.pl_id).ToList(),
								PersonenApiToRubriekCategoryHelper.ConvertModelParamsToRubrieken(model)
									.Where(x => !string.IsNullOrWhiteSpace(x))
									.OrderBy(rubriek => rubriek.Substring(0))
									.ToList(),
								PersonenApiToRubriekCategoryHelper.ConvertFieldsToRubriekCategory(fieldsToUseForAuthorisations, false)
									.ConvertAll(x => x.rubriek)
									.Where(x => !string.IsNullOrWhiteSpace(x))
									.OrderBy(rubriek => rubriek.Substring(0))
									.ToList());
			}
		}

		return (new RaadpleegMetBurgerservicenummerResponse { Personen = personenPlIds?.ToList()?.ConvertAll(gbaPersoon => gbaPersoon.persoon) ?? new List<GbaPersoon>() }, personenPlIds?.Select(x => x.pl_id).ToList());
	}

	private async Task<List<AbstractGezagsrelatie>?> MapGezagBase(string? persoonBurgerservicenummer, List<GbaPartner>? partners, List<GbaPartner>? historischePartners, List<GbaKind>? kinderen, List<GbaOuder>? ouders, List<Gezagsrelatie> gezagsrelaties, string? gemeenteVanInschrijving)
	{
		IEnumerable<Gezagsrelatie> gezagsrelatiesFromPartners = new List<Gezagsrelatie>();
		var hasPartners = partners?.Any(partner => !string.IsNullOrWhiteSpace(partner.Burgerservicenummer)) == true || historischePartners?.Any(partner => !string.IsNullOrWhiteSpace(partner.Burgerservicenummer)) == true;
		if (hasPartners && (!gezagsrelaties.Any() || gezagsrelaties.Any(gezagsrelatie => gezagsrelatie.SoortGezag != Gezagsrelatie.SoortGezagEnum.OG1Enum)))
		{
			var allPartners = partners!.Concat(historischePartners ?? new List<GbaPartner>()).Where(partner => !string.IsNullOrWhiteSpace(partner.Burgerservicenummer));
			(IEnumerable<(GbaPersoon persoon, long pl_id)>? partnersPlIds, int _) = await _getAndMapPersoonService.GetPersonenMapByBsns(allPartners.Select(partner => partner.Burgerservicenummer).Where(bsn => !string.IsNullOrWhiteSpace(bsn))!, gemeenteVanInschrijving, new List<string> { "kinderen.burgerservicenummer" }, _protocolleringAuthorizationOptions.Value.UseAuthorizationChecks);

			IEnumerable<string?>? partnerChildrenBsns = null;
			if (partnersPlIds?.Any() == true)
			{
				partnerChildrenBsns = partnersPlIds
												.Where(partner => partner.persoon.Kinderen?.Any() == true)
												.SelectMany(partner => partner.persoon.Kinderen!
																									.Select(kind => kind.Burgerservicenummer)
																									.Where(bsn => !string.IsNullOrWhiteSpace(bsn)));
			}

			if (partnerChildrenBsns?.Any() == true)
			{
				gezagsrelatiesFromPartners = (await Task.WhenAll(partnerChildrenBsns!.Select(async bsn => await _gezagsrelatieRepo.GetGezagsrelaties(bsn)))).SelectMany(x => x!).Where(x => x != null);
			}
			if (gezagsrelatiesFromPartners.Any())
			{
				gezagsrelaties.AddRange(gezagsrelatiesFromPartners);
			}

			// To ensure that there are no duplicates after getting gezag from partners.
			gezagsrelaties = gezagsrelaties.GroupBy(x => new { x.SoortGezag, x.BsnMinderjarige, x.BsnMeerderjarige }).Select(x => x.First()).ToList();
		}

		if ((!hasPartners || !gezagsrelatiesFromPartners.Any() || gezagsrelatiesFromPartners.Count() != gezagsrelaties.Count) && (!gezagsrelaties.Any() || gezagsrelaties.Any(gezagsrelatie => gezagsrelatie.SoortGezag != Gezagsrelatie.SoortGezagEnum.OG1Enum)))
		{
			var filteredGezagsrelaties = gezagsrelaties.Except(gezagsrelatiesFromPartners ?? Enumerable.Empty<Gezagsrelatie>());

			if (filteredGezagsrelaties.Any() && filteredGezagsrelaties.All(gezagsrelatie => gezagsrelatie.BsnMinderjarige != persoonBurgerservicenummer))
			{
				var childGezagsrelaties = (
					await Task.WhenAll(filteredGezagsrelaties.Select(async gezagsrelatie => await _gezagsrelatieRepo.GetGezagsrelaties(gezagsrelatie.BsnMinderjarige)))
				).SelectMany(x => x!).Where(x => x != null);
                if (childGezagsrelaties.Any())
				{
					gezagsrelaties.AddRange(childGezagsrelaties);
				}
			}

			// To ensure that there are no duplicates after getting gezag from kinderen.
			gezagsrelaties = gezagsrelaties.GroupBy(x => new { x.SoortGezag, x.BsnMinderjarige, x.BsnMeerderjarige }).Select(x => x.First()).ToList();
		}

		if (gezagsrelaties.Any())
		{
			return  _gezagTransformer.TransformGezagsrelaties(gezagsrelaties, persoonBurgerservicenummer, partners, kinderen, ouders).ToList();
		}

		return new List<AbstractGezagsrelatie>();
	}

	private Task<List<AbstractGezagsrelatie>?> MapGezagMeerderjarige(string? persoonBurgerservicenummer, Waardetabel? persoonGemeenteVanInschrijving, List<GbaPartner>? partners, List<GbaPartner>? historischePartners, List<GbaKind>? kinderen, List<GbaOuder>? ouders, List<Gezagsrelatie> gezagsrelaties, string? gemeenteVanInschrijving)
	{
		if (persoonGemeenteVanInschrijving?.Code != "1999")
		{
			return MapGezagBase(persoonBurgerservicenummer, partners, historischePartners, kinderen, ouders, gezagsrelaties, gemeenteVanInschrijving);
		}

		return Task.FromResult<List<AbstractGezagsrelatie>?>(new List<AbstractGezagsrelatie>());
	}

	private Task<List<AbstractGezagsrelatie>?> MapGezagMinderjarige(string? persoonBurgerservicenummer, List<GbaPartner>? partners, List<GbaPartner>? historischePartners, List<GbaKind>? kinderen, List<GbaOuder>? ouders, List<Gezagsrelatie> gezagsrelaties, string? gemeenteVanInschrijving)
	{
		return MapGezagBase(persoonBurgerservicenummer, partners, historischePartners, kinderen, ouders, gezagsrelaties, gemeenteVanInschrijving);
	}

	private static int? CalculateAge(DatumOnvolledig geboorteDatum)
	{
		// If birth year is unknown then it is impossible to know if person is younger than 27.
		// Because of this Leerlingen requires us to add such a person anyway. Because of logic like that, person's age will be considered to be new born.
		if (geboorteDatum == null || (!geboorteDatum.Jaar.HasValue || geboorteDatum.Jaar.Value == 0))
		{
			return 0;
		}

		// If birth month or birth day is unknown, force these to be the last of that month/day.
		// That will just mean that they will be followed for a few more days or months.
		geboorteDatum.Maand ??= 12;
		geboorteDatum.Dag ??= DateTime.DaysInMonth(geboorteDatum.Jaar!.Value, geboorteDatum.Maand.Value);

		var birthdate = new DateTime(geboorteDatum.Jaar!.Value, geboorteDatum.Maand.Value, geboorteDatum.Dag.Value);

		var currentDate = DateTime.Now;
		int age = currentDate.Year - birthdate.Year;

		// Check if the birthday has occurred this year
		if (currentDate.Month < birthdate.Month ||
			(currentDate.Month == birthdate.Month && currentDate.Day < birthdate.Day))
		{
			age--;
		}

		return age;
	}

	/// <summary>
	/// Get personen via search params (child of PersonenQuery)
	/// </summary>
	/// <param name="model"></param>
	/// <returns>Response object with list of restricted person data</returns>
	private async Task<(List<T>? personen, List<long>? plIds)> GetPersonenBeperktBase<T>(PersonenQuery model, bool? inclusiefOverledenPersonen = false) where T : GbaPersoonBeperkt
	{
		// Validation
		_fieldsExpandFilterService.ValidateScope(typeof(T), _persoonBeperktFieldsSettings.GbaFieldsSettings, model.fields);

		// Get personen
		var fieldsToUseForAuthorisations = model.fields.ConvertAll(field => _persoonBeperktFieldsSettings.GbaFieldsSettings.ShortHandMappings.ContainsKey(field)
			? _persoonBeperktFieldsSettings.GbaFieldsSettings.ShortHandMappings[field]
			: field);
		(IEnumerable<(T persoon, long pl_id)>? personenPlIds, int afnemerCode) = await _getAndMapPersoonService.GetMapZoekPersonen<T>(model, fieldsToUseForAuthorisations, _protocolleringAuthorizationOptions.Value.UseAuthorizationChecks);

		// Filter response by fields
		if (model?.fields?.Any() == true && personenPlIds != null)
		{
			// Only opschortingBijhouding with code F must be ignored. If the value F is present then the entire PL must be ignored.
			personenPlIds = (await Task.WhenAll(
				personenPlIds
				.Where(x => x.persoon.OpschortingBijhouding == null
				|| x.persoon.OpschortingBijhouding?.Reden?.Code?.Equals("F") == false
				&& x.persoon.OpschortingBijhouding?.Reden?.Code?.Equals("O") == false
				|| x.persoon.OpschortingBijhouding?.Reden?.Code?.Equals("O") == true
							&& inclusiefOverledenPersonen == true)
				.Select(async x =>
			{
				if (x.persoon is GbaGezagPersoonBeperkt persoonGezagBeperkt
						&& model.fields.Any(field => field.Contains("gezag", StringComparison.CurrentCultureIgnoreCase) && !field.StartsWith("indicatieGezagMinderjarige"))
					&& !string.IsNullOrWhiteSpace(x.persoon.Burgerservicenummer))
				{
					var gezagsrelaties = (await _gezagsrelatieRepo.GetGezagsrelaties(persoonGezagBeperkt.Burgerservicenummer))?.ToList() ?? new List<Gezagsrelatie>();
					bool? underage = gezagsrelaties.Count > 0 ? gezagsrelaties.Any(gezagsrelatie => gezagsrelatie.BsnMinderjarige == persoonGezagBeperkt.Burgerservicenummer) : null;
					int? leeftijd = 0;
					if (persoonGezagBeperkt.Geboorte != null)
					{
						leeftijd = CalculateAge(new DatumOnvolledig(persoonGezagBeperkt.Geboorte.Datum));
					}

					persoonGezagBeperkt.Gezag = new List<AbstractGezagsrelatie>();
					if (leeftijd < 18 && (underage == true || !underage.HasValue))
					{
						persoonGezagBeperkt.Gezag = await MapGezagMinderjarige(x.persoon.Burgerservicenummer, x.persoon.Partners, x.persoon.HistorischePartners, x.persoon.Kinderen, x.persoon.Ouders, gezagsrelaties, model.gemeenteVanInschrijving);
					}
					else
					{
						persoonGezagBeperkt.Gezag = await MapGezagMeerderjarige(x.persoon.Burgerservicenummer, x.persoon.GemeenteVanInschrijving, x.persoon.Partners, x.persoon.HistorischePartners, x.persoon.Kinderen, x.persoon.Ouders, gezagsrelaties, model.gemeenteVanInschrijving);
					}
				}

				x.persoon.Rni = GbaPersonenApiHelperBase.ApplyRniLogic(model.fields, x.persoon.Rni, _persoonBeperktFieldsSettings.GbaFieldsSettings);
				if (x.persoon.Verblijfplaats != null)
				{
					_gbaPersonenBeperktApiHelper.AddVerblijfplaatsBeperktInOnderzoek(model.fields, x.persoon.Verblijfplaats);
				}

				x.persoon = ApplyGbaPersoonBeperktScope(x.persoon, model.fields);
				_gbaPersonenBeperktApiHelper.RemoveBeperktInOnderzoekFromPersonCategoryIfNoFieldsAreRequested(model.fields, x.persoon);

				return x;
			})
			))
			.ToList();

			if (_protocolleringAuthorizationOptions.Value.UseProtocollering)
			{
				await LogProtocolleringInDb(afnemerCode, personenPlIds?.Select(x => x.pl_id).ToList(),
								PersonenApiToRubriekCategoryHelper.ConvertModelParamsToRubrieken(model)
									.Where(x => !string.IsNullOrWhiteSpace(x))
									.OrderBy(rubriek => rubriek.Substring(0))
									.ToList(),
								PersonenApiToRubriekCategoryHelper.ConvertFieldsToRubriekCategory(fieldsToUseForAuthorisations, true)
									.ConvertAll(x => x.rubriek)
									.Where(x => !string.IsNullOrWhiteSpace(x))
									.OrderBy(rubriek => rubriek.Substring(0))
									.ToList());
			}
		}

		return (personenPlIds?.ToList()?.ConvertAll(gbaPersoon => gbaPersoon.persoon) ?? new List<T>(), personenPlIds?.Select(x => x.pl_id).ToList());
	}

	/// <summary>
	/// Get personen via search params (child of PersonenQuery)
	/// </summary>
	/// <param name="model"></param>
	/// <returns>Response object with list of restricted person data</returns>
	private async Task<(ZoekMetGeslachtsnaamEnGeboortedatumResponse personenResponse, List<long>? plIds)> GetPersonenBeperkt(ZoekMetGeslachtsnaamEnGeboortedatum model)
	{
		// Validation
		await ValidationHelper.ValidateGemeenteInschrijving(model.gemeenteVanInschrijving, _domeinTabellenRepo);
		ValidationHelper.ValidateWildcards(model);
		var personenPlIds = await GetPersonenBeperktBase<GbaPersoonBeperkt>(model, model.inclusiefOverledenPersonen);

		// Get personen + Filter response by fields
		return (new ZoekMetGeslachtsnaamEnGeboortedatumResponse { Personen = personenPlIds.personen }, personenPlIds.plIds);
	}

	/// <summary>
	/// Get personen via search params (child of PersonenQuery)
	/// </summary>
	/// <param name="model"></param>
	/// <returns>Response object with list of restricted person data</returns>
	private async Task<(ZoekMetNaamEnGemeenteVanInschrijvingResponse personenResponse, List<long>? plIds)> GetPersonenBeperkt(ZoekMetNaamEnGemeenteVanInschrijving model)
	{
		// Validation
		await ValidationHelper.ValidateGemeenteInschrijving(model.gemeenteVanInschrijving, _domeinTabellenRepo);
		ValidationHelper.ValidateWildcards(model);
		var personenPlIds = await GetPersonenBeperktBase<GbaPersoonBeperkt>(model, model.inclusiefOverledenPersonen);

		// Get personen + Filter response by fields
		return (new ZoekMetNaamEnGemeenteVanInschrijvingResponse { Personen = personenPlIds.personen }, personenPlIds.plIds);
	}

	/// <summary>
	/// Get personen via search params (child of PersonenQuery)
	/// </summary>
	/// <param name="model"></param>
	/// <returns>Response object with list of restricted person data</returns>
	private async Task<(ZoekMetNummeraanduidingIdentificatieResponse personenResponse, List<long>? plIds)> GetPersonenBeperkt(ZoekMetNummeraanduidingIdentificatie model)
	{
		var personenPlIds = await GetPersonenBeperktBase<GbaPersoonBeperkt>(model, model.inclusiefOverledenPersonen);

		// Get personen + Filter response by fields
		return (new ZoekMetNummeraanduidingIdentificatieResponse { Personen = personenPlIds.personen }, personenPlIds.plIds);
	}

	/// <summary>
	/// Get personen via search params (child of PersonenQuery)
	/// </summary>
	/// <param name="model"></param>
	/// <returns>Response object with list of restricted person data</returns>
	private async Task<(ZoekMetPostcodeEnHuisnummerResponse personenResponse, List<long>? plIds)> GetPersonenBeperkt(ZoekMetPostcodeEnHuisnummer model)
	{
		// Validation
		await ValidationHelper.ValidateGemeenteInschrijving(model.gemeenteVanInschrijving, _domeinTabellenRepo);
		var personenPlIds = await GetPersonenBeperktBase<GbaPersoonBeperkt>(model, model.inclusiefOverledenPersonen);

		// Get personen + Filter response by fields
		return (new ZoekMetPostcodeEnHuisnummerResponse { Personen = personenPlIds.personen }, personenPlIds.plIds);
	}

	/// <summary>
	/// Get personen via search params (child of PersonenQuery)
	/// </summary>
	/// <param name="model"></param>
	/// <returns>Response object with list of restricted person data</returns>
	private async Task<(ZoekMetStraatHuisnummerEnGemeenteVanInschrijvingResponse personenResponse, List<long>? plIds)> GetPersonenBeperkt(ZoekMetStraatHuisnummerEnGemeenteVanInschrijving model)
	{
		// Validation
		await ValidationHelper.ValidateGemeenteInschrijving(model.gemeenteVanInschrijving, _domeinTabellenRepo);
		ValidationHelper.ValidateWildcards(model);
		var personenPlIds = await GetPersonenBeperktBase<GbaPersoonBeperkt>(model, model.inclusiefOverledenPersonen);

		// Get personen + Filter response by fields
		return (new ZoekMetStraatHuisnummerEnGemeenteVanInschrijvingResponse { Personen = personenPlIds.personen }, personenPlIds.plIds);
	}

	private async Task<(ZoekMetAdresseerbaarObjectIdentificatieResponse personenResponse, List<long>? plIds)> GetPersonenBeperkt(ZoekMetAdresseerbaarObjectIdentificatie model)
	{
		var personenPlIds = await GetPersonenBeperktBase<GbaGezagPersoonBeperkt>(model, model.inclusiefOverledenPersonen);

		// Get personen + Filter response by fields
		return (new ZoekMetAdresseerbaarObjectIdentificatieResponse { Personen = personenPlIds.personen }, personenPlIds.plIds);
	}

	private T ApplyGbaPersoonBeperktScope<T>(T gbaPersoonBeperkt, List<string> fields) where T : GbaPersoonBeperkt
	{
		return _fieldsExpandFilterService.ApplyScope(gbaPersoonBeperkt, string.Join(",", fields), _persoonBeperktFieldsSettings.GbaFieldsSettings);
	}
}