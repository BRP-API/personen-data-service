using Microsoft.IdentityModel.Tokens;
using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Common;
using Rvig.HaalCentraalApi.Personen.Fields;
using Rvig.HaalCentraalApi.Personen.Helpers;
using Rvig.HaalCentraalApi.Personen.Interfaces;
using Rvig.HaalCentraalApi.Shared.Exceptions;
using Rvig.HaalCentraalApi.Shared.Interfaces;
using Rvig.HaalCentraalApi.Shared.Services;

namespace Rvig.HaalCentraalApi.Personen.Services;
public interface IGbaPersonenApiService
{
	Task<(PersonenQueryResponse personenResponse, List<long>? plIds, List<string>? bsns)> GetPersonen(PersonenQuery model);
}

public class GbaPersonenApiService : BaseApiService, IGbaPersonenApiService
{
	protected IGetAndMapGbaPersonenService _getAndMapPersoonService;
	private readonly GbaPersonenApiHelper _gbaPersonenApiHelper;
	private readonly GbaPersonenBeperktApiHelper _gbaPersonenBeperktApiHelper;

	private static PersonenFieldsSettings _persoonFieldsSettings => new();
	private static PersonenBeperktFieldsSettings _persoonBeperktFieldsSettings => new();

    public GbaPersonenApiService(
        IGetAndMapGbaPersonenService getAndMapPersoonService, 
		IDomeinTabellenRepo domeinTabellenRepo)
		: base(domeinTabellenRepo)
	{
		_getAndMapPersoonService = getAndMapPersoonService;
		_gbaPersonenApiHelper = new GbaPersonenApiHelper(_fieldsExpandFilterService, _persoonFieldsSettings);
		_gbaPersonenBeperktApiHelper = new GbaPersonenBeperktApiHelper(_persoonBeperktFieldsSettings);
	}

	/// <summary>
	/// Get BRP (GBA) 2.0.0 personen via child of PersonenQuery.
	/// </summary>
	/// <param name="model">Child of PersonenQuery.</param>
	/// <returns>Child of PersonenQueryResponse</returns>
	/// <exception cref="InvalidOperationException"></exception>
	public async Task<(PersonenQueryResponse personenResponse, List<long>? plIds, List<string>? bsns)> GetPersonen(PersonenQuery model)
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
	private async Task<(RaadpleegMetBurgerservicenummerResponse personenResponse, List<long>? plIds, List<string> bsns)> GetPersonen(RaadpleegMetBurgerservicenummer model)
	{
		// Validation
		_fieldsExpandFilterService.ValidateScope(typeof(GbaPersoon), _persoonFieldsSettings.GbaFieldsSettings, model.Fields);

		// Get personen
		var fieldsToUseForAuthorisations = model.Fields.ConvertAll(field => _persoonFieldsSettings.GbaFieldsSettings.ShortHandMappings.ContainsKey(field)
			? _persoonFieldsSettings.GbaFieldsSettings.ShortHandMappings[field]
			: field);

        List<(GbaPersoon persoon, long pl_id)> result = new List<(GbaPersoon persoon, long pl_id)>();
        (IEnumerable<(GbaPersoon persoon, long pl_id)>? personenPlIds, int afnemerCode) = await _getAndMapPersoonService.GetPersonenMapByBsns(model.Burgerservicenummer, model.GemeenteVanInschrijving, fieldsToUseForAuthorisations);

		List<string> bsns = new();

		// Filter response by fields
		if (model?.Fields?.Any() == true && personenPlIds != null)
		{
			_gbaPersonenApiHelper.HackLogicKinderenPartnersOuders(model.Fields, personenPlIds?.ToList()?.ConvertAll(gbaPersoon => gbaPersoon.persoon));

			//var bsns = personenPlIds!.Select(p => p.persoon.Burgerservicenummer).Where(bsn => !bsn.IsNullOrEmpty()).ToList();
			//var gezag = await _gezagService.GetGezagIfRequested(model.Fields, bsns);
            //var gezagPersonen = await _gezagService.GetGezagPersonenIfRequested(model.Fields, gezag);

			foreach (var x in personenPlIds!.Where(x => x.persoon != null))
            {
                bsns.Add(x.persoon.Burgerservicenummer);
                //_gezagService.VerrijkPersonenMetGezagIfRequested(model.Fields, gezag, gezagPersonen, x);

                x.persoon.Rni = GbaPersonenApiHelperBase.ApplyRniLogic(model.Fields, x.persoon.Rni, _persoonFieldsSettings.GbaFieldsSettings);
                if (x.persoon.Verblijfplaats != null)
                {
                    GbaPersonenApiHelper.AddVerblijfplaatsInOnderzoek(model.Fields, x.persoon.Verblijfplaats);
                }
                if (x.persoon.Immigratie != null)
                {
                    GbaPersonenApiHelper.AddLandVanwaarIngeschreven(model.Fields, x.persoon.Immigratie);
                }

                model.Fields = GbaPersonenApiHelper.OpschortingBijhoudingLogicRaadplegen(model.Fields, x.persoon.OpschortingBijhouding);
				
				if (!model.Fields.Contains("burgerservicenummer") && GezagHelper.GezagIsRequested(model.Fields))
                {
					model.Fields.Add("burgerservicenummer");
                }

                GbaPersoon target = _fieldsExpandFilterService.ApplyScope(x.persoon, string.Join(",", model.Fields), _persoonFieldsSettings.GbaFieldsSettings);
                _gbaPersonenApiHelper.RemoveInOnderzoekFromPersonCategoryIfNoFieldsAreRequested(model.Fields, target);
                _gbaPersonenApiHelper.RemoveInOnderzoekFromGezagsverhoudingCategoryIfNoFieldsAreRequested(model.Fields, target);
                result.Add((target, x.pl_id));
            }
		}

		var response = new RaadpleegMetBurgerservicenummerResponse { Personen = result.ConvertAll(gbaPersoon => gbaPersoon.persoon) };
		var plIds = result.Select(x => x.pl_id).ToList();

		return (response, plIds, bsns); 
	}

    /// <summary>
    /// Get personen via search params (child of PersonenQuery)
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Response object with list of restricted person data</returns>
    private async Task<(List<T>? personen, List<long>? plIds, List<string>? bsns)> GetPersonenBeperktBase<T>(PersonenQuery model, bool? inclusiefOverledenPersonen = false) where T : GbaPersoonBeperkt
	{
		// Validation
		_fieldsExpandFilterService.ValidateScope(typeof(T), _persoonBeperktFieldsSettings.GbaFieldsSettings, model.Fields);

		// Get personen
		var fieldsToUseForAuthorisations = model.Fields.ConvertAll(field => _persoonBeperktFieldsSettings.GbaFieldsSettings.ShortHandMappings.ContainsKey(field)
			? _persoonBeperktFieldsSettings.GbaFieldsSettings.ShortHandMappings[field]
			: field);

		(IEnumerable<(T persoon, long pl_id)>? personenPlIds, int afnemerCode) = await _getAndMapPersoonService.GetMapZoekPersonen<T>(model, fieldsToUseForAuthorisations);
        List<(T persoon, long pl_id)> result = new List<(T persoon, long pl_id)>();
        List<string> bsns = new();

        // Filter response by fields
        if (model?.Fields?.Any() == true && personenPlIds != null)
        {
            // OpschortingBijhouding with code F must be ignored. If the value F is present then the entire PL must be ignored.
            // and opschortingBijhouding with code O must be ignored if the result should not included deceased personen
            personenPlIds = personenPlIds.Where(x => x.persoon.OpschortingBijhouding == null
				|| x.persoon.OpschortingBijhouding?.Reden?.Code?.Equals("F") == false
				&& x.persoon.OpschortingBijhouding?.Reden?.Code?.Equals("O") == false
				|| x.persoon.OpschortingBijhouding?.Reden?.Code?.Equals("O") == true
							&& inclusiefOverledenPersonen == true);

            //var bsns = personenPlIds.Select(p => p.persoon.Burgerservicenummer).Where(bsn => !bsn.IsNullOrEmpty()).ToList();
            //var gezag = await _gezagService.GetGezagIfRequested(model.Fields, bsns);
            //var gezagPersonen = await _gezagService.GetGezagPersonenIfRequested(model.Fields, gezag);

            foreach (var x in personenPlIds.Where(x => x.persoon != null))
			{
                //if (x.persoon is IPersoonMetGezag)
                //{
                //	//_gezagService.VerrijkPersonenMetGezagIfRequested(model.Fields, gezag, gezagPersonen, ((IPersoonMetGezag persoon, long pl_id))x);
                //}
                bsns.Add(x.persoon.Burgerservicenummer);

                if (!model.Fields.Contains("burgerservicenummer") && GezagHelper.GezagIsRequested(model.Fields))
                {
                    model.Fields.Add("burgerservicenummer");
                }

                x.persoon.Rni = GbaPersonenApiHelperBase.ApplyRniLogic(model.Fields, x.persoon.Rni, _persoonBeperktFieldsSettings.GbaFieldsSettings);
				if (x.persoon.Verblijfplaats != null)
				{
					_gbaPersonenBeperktApiHelper.AddVerblijfplaatsBeperktInOnderzoek(model.Fields, x.persoon.Verblijfplaats);
				}

				T target = ApplyGbaPersoonBeperktScope(x.persoon, model.Fields);
				_gbaPersonenBeperktApiHelper.RemoveBeperktInOnderzoekFromPersonCategoryIfNoFieldsAreRequested(model.Fields, target);
				result.Add((target, x.pl_id));
			}
		}

		var response = result.ConvertAll(gbaPersoon => gbaPersoon.persoon);
        var plIds = result.Select(x => x.pl_id).ToList();

        return (response, plIds, bsns);
    }

	/// <summary>
	/// Get personen via search params (child of PersonenQuery)
	/// </summary>
	/// <param name="model"></param>
	/// <returns>Response object with list of restricted person data</returns>
	private async Task<(ZoekMetGeslachtsnaamEnGeboortedatumResponse personenResponse, List<long>? plIds, List<string>? bsns)> GetPersonenBeperkt(ZoekMetGeslachtsnaamEnGeboortedatum model)
	{
		// Validation
		var personenPlIds = await GetPersonenBeperktBase<GbaPersoonBeperkt>(model, model.InclusiefOverledenPersonen);

		// Get personen + Filter response by fields
		return (new ZoekMetGeslachtsnaamEnGeboortedatumResponse { Personen = personenPlIds.personen }, personenPlIds.plIds, personenPlIds.bsns);
	}

	/// <summary>
	/// Get personen via search params (child of PersonenQuery)
	/// </summary>
	/// <param name="model"></param>
	/// <returns>Response object with list of restricted person data</returns>
	private async Task<(ZoekMetNaamEnGemeenteVanInschrijvingResponse personenResponse, List<long>? plIds, List<string>? bsns)> GetPersonenBeperkt(ZoekMetNaamEnGemeenteVanInschrijving model)
	{
		// Validation
		var personenPlIds = await GetPersonenBeperktBase<GbaPersoonBeperkt>(model, model.InclusiefOverledenPersonen);

		// Get personen + Filter response by fields
		return (new ZoekMetNaamEnGemeenteVanInschrijvingResponse { Personen = personenPlIds.personen }, personenPlIds.plIds, personenPlIds.bsns);
	}

	/// <summary>
	/// Get personen via search params (child of PersonenQuery)
	/// </summary>
	/// <param name="model"></param>
	/// <returns>Response object with list of restricted person data</returns>
	private async Task<(ZoekMetNummeraanduidingIdentificatieResponse personenResponse, List<long>? plIds, List<string>? bsns)> GetPersonenBeperkt(ZoekMetNummeraanduidingIdentificatie model)
	{
		var personenPlIds = await GetPersonenBeperktBase<GbaPersoonBeperkt>(model, model.InclusiefOverledenPersonen);

		// Get personen + Filter response by fields
		return (new ZoekMetNummeraanduidingIdentificatieResponse { Personen = personenPlIds.personen }, personenPlIds.plIds, personenPlIds.bsns);
	}

	/// <summary>
	/// Get personen via search params (child of PersonenQuery)
	/// </summary>
	/// <param name="model"></param>
	/// <returns>Response object with list of restricted person data</returns>
	private async Task<(ZoekMetPostcodeEnHuisnummerResponse personenResponse, List<long>? plIds, List<string>? bsns)> GetPersonenBeperkt(ZoekMetPostcodeEnHuisnummer model)
	{
		// Validation
		var personenPlIds = await GetPersonenBeperktBase<GbaPersoonBeperkt>(model, model.InclusiefOverledenPersonen);

		// Get personen + Filter response by fields
		return (new ZoekMetPostcodeEnHuisnummerResponse { Personen = personenPlIds.personen }, personenPlIds.plIds, personenPlIds.bsns);
	}

	/// <summary>
	/// Get personen via search params (child of PersonenQuery)
	/// </summary>
	/// <param name="model"></param>
	/// <returns>Response object with list of restricted person data</returns>
	private async Task<(ZoekMetStraatHuisnummerEnGemeenteVanInschrijvingResponse personenResponse, List<long>? plIds, List<string>? bsns)> GetPersonenBeperkt(ZoekMetStraatHuisnummerEnGemeenteVanInschrijving model)
	{
		// Validation
		var personenPlIds = await GetPersonenBeperktBase<GbaPersoonBeperkt>(model, model.InclusiefOverledenPersonen);

		// Get personen + Filter response by fields
		return (new ZoekMetStraatHuisnummerEnGemeenteVanInschrijvingResponse { Personen = personenPlIds.personen }, personenPlIds.plIds, personenPlIds.bsns);
	}

	private async Task<(ZoekMetAdresseerbaarObjectIdentificatieResponse personenResponse, List<long>? plIds, List<string>? bsns)> GetPersonenBeperkt(ZoekMetAdresseerbaarObjectIdentificatie model)
	{
		var personenPlIds = await GetPersonenBeperktBase<GbaGezagPersoonBeperkt>(model, model.InclusiefOverledenPersonen);

		// Get personen + Filter response by fields
		return (new ZoekMetAdresseerbaarObjectIdentificatieResponse { Personen = personenPlIds.personen }, personenPlIds.plIds, personenPlIds.bsns);
	}

	private T ApplyGbaPersoonBeperktScope<T>(T gbaPersoonBeperkt, List<string> fields) where T : GbaPersoonBeperkt
	{
		return _fieldsExpandFilterService.ApplyScope(gbaPersoonBeperkt, string.Join(",", fields), _persoonBeperktFieldsSettings.GbaFieldsSettings);
	}
}