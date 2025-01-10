using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Personen.ApiModels.Gezag;
using Rvig.HaalCentraalApi.Personen.Fields;
using Rvig.HaalCentraalApi.Personen.Helpers;
using Rvig.HaalCentraalApi.Personen.Interfaces;
using Rvig.HaalCentraalApi.Personen.Repositories;
using Rvig.HaalCentraalApi.Personen.RequestModels.BRP;
using Rvig.HaalCentraalApi.Personen.ResponseModels.BRP;
using Rvig.HaalCentraalApi.Personen.Validation;
using Rvig.HaalCentraalApi.Shared.Exceptions;
using Rvig.HaalCentraalApi.Shared.Fields;
using Rvig.HaalCentraalApi.Shared.Helpers;
using Rvig.HaalCentraalApi.Shared.Interfaces;
using Rvig.HaalCentraalApi.Shared.Options;
using Rvig.HaalCentraalApi.Shared.Services;
using Rvig.HaalCentraalApi.Shared.Util;
using System.Linq;
using AbstractGezagsrelatie = Rvig.HaalCentraalApi.Personen.ApiModels.BRP.AbstractGezagsrelatie;

namespace Rvig.HaalCentraalApi.Personen.Services;
public interface IGbaPersonenApiService
{
	Task<(PersonenQueryResponse personenResponse, List<long>? plIds)> GetPersonen(PersonenQuery model);
}

public class GbaPersonenApiService : BaseApiService, IGbaPersonenApiService
{
	protected IGetAndMapGbaPersonenService _getAndMapPersoonService;
	private readonly IRepoGezagsrelatie _gezagsrelatieRepo;
	private readonly GbaPersonenApiHelper _gbaPersonenApiHelper;
	private readonly GbaPersonenBeperktApiHelper _gbaPersonenBeperktApiHelper;

	// We didn't want to split personen and beperkt because they use the same operation. Too much of a hassle.
	// Because of this we ignored this abstract class implementation in favor for two seperate fields to make things easier.
	protected override FieldsSettings _fieldsSettings => throw new CustomNotImplementedException();

	private static PersonenFieldsSettings _persoonFieldsSettings => new();
	private static PersonenBeperktFieldsSettings _persoonBeperktFieldsSettings => new();

	public GbaPersonenApiService(IGetAndMapGbaPersonenService getAndMapPersoonService, IDomeinTabellenRepo domeinTabellenRepo, IProtocolleringService protocolleringService
		, ILoggingHelper loggingHelper, IRepoGezagsrelatie gezagsrelatieRepo, IOptions<ProtocolleringAuthorizationOptions> protocolleringAuthorizationOptions)
		: base(domeinTabellenRepo, protocolleringService, loggingHelper, protocolleringAuthorizationOptions)
	{
		_getAndMapPersoonService = getAndMapPersoonService;
		_gezagsrelatieRepo = gezagsrelatieRepo;
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

	private static bool GezagIsRequested(List<string> fields) =>
		fields.Any(field => field.Contains("gezag", StringComparison.CurrentCultureIgnoreCase) &&
						  	!field.StartsWith("indicatieGezagMinderjarige"));

	private async Task<List<Persoon>> GetGezagIfRequested(List<string> fields, List<string?> bsns){
		if(GezagIsRequested(fields))
		{
			GezagResponse response = (await _gezagsrelatieRepo.GetGezag(bsns!)) ?? new GezagResponse();
			return response.Personen.ToList();
		}
		return new List<Persoon>();
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

        List<(GbaPersoon persoon, long pl_id)> result = new List<(GbaPersoon persoon, long pl_id)>();
        (IEnumerable<(GbaPersoon persoon, long pl_id)>? personenPlIds, int afnemerCode) = await _getAndMapPersoonService.GetPersonenMapByBsns(model.burgerservicenummer, model.gemeenteVanInschrijving, fieldsToUseForAuthorisations, _protocolleringAuthorizationOptions.Value.UseAuthorizationChecks);

		// Filter response by fields
		if (model?.fields?.Any() == true && personenPlIds != null)
		{
			_gbaPersonenApiHelper.HackLogicKinderenPartnersOuders(model.fields, personenPlIds?.ToList()?.ConvertAll(gbaPersoon => gbaPersoon.persoon));

			var bsns = personenPlIds!.Select(p => p.persoon.Burgerservicenummer).Where(bsn => !bsn.IsNullOrEmpty()).ToList();

			IEnumerable<Persoon> persoonGezagsrelaties = await GetGezagIfRequested(model.fields, bsns);

			List<ApiModels.Gezag.AbstractGezagsrelatie> gezagsrelaties;
			List<string> gezagBsns;
			List<GbaPersoon> gezagPersonen = new();

            if (GezagIsRequested(model.fields))
			{
                gezagsrelaties = persoonGezagsrelaties.Where(p => p.Gezag != null).SelectMany(p => p.Gezag).ToList();
				gezagBsns = GetGezagBsns(gezagsrelaties);
				gezagPersonen = await GetGezagPersonen(gezagBsns);	
			}

			foreach (var x in personenPlIds!.Where(x => x.persoon != null))
			{
				if(GezagIsRequested(model.fields))
				{
					var persoonGezagsrelatie = persoonGezagsrelaties
						.Where(pgr => pgr.Burgerservicenummer == x.persoon.Burgerservicenummer);

					if (persoonGezagsrelatie.Any())
					{
						x.persoon.Gezag = new List<AbstractGezagsrelatie>();
					}
					foreach(var pg in persoonGezagsrelatie)
					{
                        var gezagResponse = new GezagResponse { Personen = new List<Persoon>() { pg } };
                        var gezag = GezagsrelatieMapper.Map(gezagResponse, gezagPersonen);

                        x.persoon.Gezag?.AddRange(gezag);
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

				GbaPersoon target = _fieldsExpandFilterService.ApplyScope(x.persoon, string.Join(",", model.fields), _persoonFieldsSettings.GbaFieldsSettings);
				_gbaPersonenApiHelper.RemoveInOnderzoekFromPersonCategoryIfNoFieldsAreRequested(model.fields, target);
				_gbaPersonenApiHelper.RemoveInOnderzoekFromGezagsverhoudingCategoryIfNoFieldsAreRequested(model.fields, target);
				result.Add((target, x.pl_id));
			}

			if (_protocolleringAuthorizationOptions.Value.UseProtocollering)
			{
				await LogProtocolleringInDb(afnemerCode, result.Select(x => x.pl_id).ToList(),
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

		return (new RaadpleegMetBurgerservicenummerResponse { Personen = result.ConvertAll(gbaPersoon => gbaPersoon.persoon) ?? new List<GbaPersoon>() }, result.Select(x => x.pl_id).ToList());
	}

    private async Task<List<GbaPersoon>> GetGezagPersonen(List<string> gezagBsns)
    {
        (IEnumerable<(GbaPersoon persoon, long pl_id)>? personenData, int _) = await _getAndMapPersoonService.GetPersonenMapByBsns(gezagBsns, null, new List<string> { "naam", "geslacht", "geboorte.datum" }, _protocolleringAuthorizationOptions.Value.UseAuthorizationChecks);

        List<GbaPersoon> gezagPersonen = new();
        if (personenData != null)
        {
            gezagPersonen = personenData.Select(x => x.persoon).ToList();
        }

		return gezagPersonen;
    }

    private static List<string> GetGezagBsns(List<ApiModels.Gezag.AbstractGezagsrelatie> gezagsrelaties)
    {
        var gezagBsns = new List<string>();

        foreach (var x in gezagsrelaties)
        {
            if (x is ApiModels.Gezag.TweehoofdigOuderlijkGezag y)
            {
                gezagBsns.AddRange(from z in y.Ouders
                                   select z.Burgerservicenummer);
                gezagBsns.Add(y.Minderjarige.Burgerservicenummer);
            }
            if (x is ApiModels.Gezag.EenhoofdigOuderlijkGezag y2)
            {
                gezagBsns.Add(y2.Ouder.Burgerservicenummer);
            }
            if (x is ApiModels.Gezag.Voogdij v)
            {
                gezagBsns.AddRange(from z in v.Derden
                                   select z.Burgerservicenummer);
                gezagBsns.Add(v.Minderjarige.Burgerservicenummer);
            }
            if (x is ApiModels.Gezag.GezamenlijkGezag gz)
            {
                gezagBsns.Add(gz.Derde.Burgerservicenummer);
                gezagBsns.Add(gz.Ouder.Burgerservicenummer);
                gezagBsns.Add(gz.Minderjarige.Burgerservicenummer);
            }
            if (x is ApiModels.Gezag.GezagNietTeBepalen gn)
            {
                gezagBsns.Add(gn.Minderjarige.Burgerservicenummer);
            }
            if (x is ApiModels.Gezag.TijdelijkGeenGezag tg)
            {
                gezagBsns.Add(tg.Minderjarige.Burgerservicenummer);
            }
        }

        return gezagBsns.Distinct().ToList();
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
        List<(T persoon, long pl_id)> result = new List<(T persoon, long pl_id)>();

        // Filter response by fields
        if (model?.fields?.Any() == true && personenPlIds != null)
        {
            // OpschortingBijhouding with code F must be ignored. If the value F is present then the entire PL must be ignored.
            // and opschortingBijhouding with code O must be ignored if the result should not included deceased personen
            personenPlIds = personenPlIds.Where(x => x.persoon.OpschortingBijhouding == null
				|| x.persoon.OpschortingBijhouding?.Reden?.Code?.Equals("F") == false
				&& x.persoon.OpschortingBijhouding?.Reden?.Code?.Equals("O") == false
				|| x.persoon.OpschortingBijhouding?.Reden?.Code?.Equals("O") == true
							&& inclusiefOverledenPersonen == true);

            var bsns = personenPlIds.Select(p => p.persoon.Burgerservicenummer).Where(bsn => !bsn.IsNullOrEmpty()).ToList();

            IEnumerable<Persoon> persoonGezagsrelaties = personenPlIds.FirstOrDefault().persoon is GbaGezagPersoonBeperkt
				? await GetGezagIfRequested(model.fields, bsns)
				: new List<Persoon>();

            List<ApiModels.Gezag.AbstractGezagsrelatie> gezagsrelaties;
            List<string> gezagBsns;
            List<GbaPersoon> gezagPersonen = new();

            if (GezagIsRequested(model.fields))
            {
                gezagsrelaties = persoonGezagsrelaties.Where(p => p.Gezag != null).SelectMany(p => p.Gezag).ToList();
                gezagBsns = GetGezagBsns(gezagsrelaties);
                gezagPersonen = await GetGezagPersonen(gezagBsns);
            }

            foreach (var x in personenPlIds.Where(x => x.persoon != null))
			{
				if (x.persoon is GbaGezagPersoonBeperkt persoonBeperkt &&
					GezagIsRequested(model.fields) &&
					!string.IsNullOrWhiteSpace(x.persoon.Burgerservicenummer))
				{
					var persoonGezagsrelatie = persoonGezagsrelaties
						.Where(pgr => pgr.Burgerservicenummer == x.persoon.Burgerservicenummer);

                    if (persoonGezagsrelatie.Any())
                    {
                        persoonBeperkt.Gezag = new List<AbstractGezagsrelatie>();
                    }
                    foreach (var pg in persoonGezagsrelatie)
                    {
						var gezagResponse = new GezagResponse { Personen = new List<Persoon>() { pg } };
                        var gezag = GezagsrelatieMapper.Map(gezagResponse, gezagPersonen);

                        persoonBeperkt.Gezag?.AddRange(gezag);
                    }
                }

				x.persoon.Rni = GbaPersonenApiHelperBase.ApplyRniLogic(model.fields, x.persoon.Rni, _persoonBeperktFieldsSettings.GbaFieldsSettings);
				if (x.persoon.Verblijfplaats != null)
				{
					_gbaPersonenBeperktApiHelper.AddVerblijfplaatsBeperktInOnderzoek(model.fields, x.persoon.Verblijfplaats);
				}


				T target = ApplyGbaPersoonBeperktScope(x.persoon, model.fields);
				_gbaPersonenBeperktApiHelper.RemoveBeperktInOnderzoekFromPersonCategoryIfNoFieldsAreRequested(model.fields, target);
				result.Add((target, x.pl_id));
			}

			if (_protocolleringAuthorizationOptions.Value.UseProtocollering)
			{
				await LogProtocolleringInDb(afnemerCode, result.Select(x => x.pl_id).ToList(),
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

		return (result.ConvertAll(gbaPersoon => gbaPersoon.persoon) ?? new List<T>(), result.Select(x => x.pl_id).ToList());
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