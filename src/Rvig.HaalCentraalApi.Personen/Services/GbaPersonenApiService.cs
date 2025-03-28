﻿using Microsoft.IdentityModel.Tokens;
using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Personen.Fields;
using Rvig.HaalCentraalApi.Personen.Helpers;
using Rvig.HaalCentraalApi.Personen.Interfaces;
using Rvig.HaalCentraalApi.Personen.RequestModels.BRP;
using Rvig.HaalCentraalApi.Personen.ResponseModels.BRP;
using Rvig.HaalCentraalApi.Shared.Exceptions;
using Rvig.HaalCentraalApi.Shared.Interfaces;
using Rvig.HaalCentraalApi.Shared.Services;

namespace Rvig.HaalCentraalApi.Personen.Services;
public interface IGbaPersonenApiService
{
	Task<(PersonenQueryResponse personenResponse, List<long>? plIds)> GetPersonen(PersonenQuery model);
}

public class GbaPersonenApiService : BaseApiService, IGbaPersonenApiService
{
	protected IGetAndMapGbaPersonenService _getAndMapPersoonService;
	private readonly IGezagService _gezagService;
	private readonly GbaPersonenApiHelper _gbaPersonenApiHelper;
	private readonly GbaPersonenBeperktApiHelper _gbaPersonenBeperktApiHelper;

	private static PersonenFieldsSettings _persoonFieldsSettings => new();
	private static PersonenBeperktFieldsSettings _persoonBeperktFieldsSettings => new();

	public GbaPersonenApiService(
        IGetAndMapGbaPersonenService getAndMapPersoonService, 
		IDomeinTabellenRepo domeinTabellenRepo, 
		IGezagService gezagService)
		: base(domeinTabellenRepo)
	{
		_getAndMapPersoonService = getAndMapPersoonService;
		_gezagService = gezagService;
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
		_fieldsExpandFilterService.ValidateScope(typeof(GbaPersoon), _persoonFieldsSettings.GbaFieldsSettings, model.fields);

		// Get personen
		var fieldsToUseForAuthorisations = model.fields.ConvertAll(field => _persoonFieldsSettings.GbaFieldsSettings.ShortHandMappings.ContainsKey(field)
			? _persoonFieldsSettings.GbaFieldsSettings.ShortHandMappings[field]
			: field);

        List<(GbaPersoon persoon, long pl_id)> result = new List<(GbaPersoon persoon, long pl_id)>();
        (IEnumerable<(GbaPersoon persoon, long pl_id)>? personenPlIds, int afnemerCode) = await _getAndMapPersoonService.GetPersonenMapByBsns(model.burgerservicenummer, model.gemeenteVanInschrijving, fieldsToUseForAuthorisations);

		// Filter response by fields
		if (model?.fields?.Any() == true && personenPlIds != null)
		{
			_gbaPersonenApiHelper.HackLogicKinderenPartnersOuders(model.fields, personenPlIds?.ToList()?.ConvertAll(gbaPersoon => gbaPersoon.persoon));

			var bsns = personenPlIds!.Select(p => p.persoon.Burgerservicenummer).Where(bsn => !bsn.IsNullOrEmpty()).ToList();

			var gezag = await _gezagService.GetGezagIfRequested(model.fields, bsns);

            var gezagPersonen = await _gezagService.GetGezagPersonenIfRequested(model.fields, gezag);

			foreach (var x in personenPlIds!.Where(x => x.persoon != null))
            {
				_gezagService.VerrijkPersonenMetGezagIfRequested(model.fields, gezag, gezagPersonen, x);

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
		}

		return (new RaadpleegMetBurgerservicenummerResponse { Personen = result.ConvertAll(gbaPersoon => gbaPersoon.persoon) ?? new List<GbaPersoon>() }, result.Select(x => x.pl_id).ToList());
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

		(IEnumerable<(T persoon, long pl_id)>? personenPlIds, int afnemerCode) = await _getAndMapPersoonService.GetMapZoekPersonen<T>(model, fieldsToUseForAuthorisations);
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

            var gezag = await _gezagService.GetGezagIfRequested(model.fields, bsns);
            var gezagPersonen = await _gezagService.GetGezagPersonenIfRequested(model.fields, gezag);

            foreach (var x in personenPlIds.Where(x => x.persoon != null))
			{
				if (x.persoon is IPersoonMetGezag)
				{
					_gezagService.VerrijkPersonenMetGezagIfRequested(model.fields, gezag, gezagPersonen, ((IPersoonMetGezag persoon, long pl_id))x);
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