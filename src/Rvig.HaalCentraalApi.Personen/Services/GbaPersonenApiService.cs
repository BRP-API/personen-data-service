//using Microsoft.IdentityModel.Tokens;
////using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
//using Rvig.HaalCentraalApi.Personen.Fields;
//using Rvig.HaalCentraalApi.Personen.Helpers;
//using Rvig.HaalCentraalApi.Personen.Interfaces;
////using Rvig.HaalCentraalApi.Personen.RequestModels.BRP;
////using Rvig.HaalCentraalApi.Personen.ResponseModels.BRP;
//using Rvig.HaalCentraalApi.Personen.Generated;
//using Rvig.HaalCentraalApi.Shared.Exceptions;
//using Rvig.HaalCentraalApi.Shared.Interfaces;
//using Rvig.HaalCentraalApi.Shared.Services;
//using Namotion.Reflection;
//using Rvig.HaalCentraalApi.Personen.Mappers;
//using Microsoft.AspNetCore.Http;

//namespace Rvig.HaalCentraalApi.Personen.Services;
//public interface IGbaPersonenApiService
//{
//	Task<(PersonenQueryResponse personenResponse, List<long>? plIds)> GetPersonen(PersonenQuery model);
//}

//public class GbaPersonenApiService : BaseApiService, IGbaPersonenApiService
//{
//	protected IGetAndMapGbaPersonenService _getAndMapPersoonService;
//	private readonly IGezagService _gezagService;
//	private readonly GbaPersonenApiHelper _gbaPersonenApiHelper;
//	private readonly GbaPersonenBeperktApiHelper _gbaPersonenBeperktApiHelper;

//	private static PersonenFieldsSettings _persoonFieldsSettings => new();
//	private static PersonenBeperktFieldsSettings _persoonBeperktFieldsSettings => new();

//	public GbaPersonenApiService(
//        IGetAndMapGbaPersonenService getAndMapPersoonService, 
//		IDomeinTabellenRepo domeinTabellenRepo, 
//		IGezagService gezagService)
//		: base(domeinTabellenRepo)
//	{
//		_getAndMapPersoonService = getAndMapPersoonService;
//		_gezagService = gezagService;
//		_gbaPersonenApiHelper = new GbaPersonenApiHelper(_fieldsExpandFilterService, _persoonFieldsSettings);
//		_gbaPersonenBeperktApiHelper = new GbaPersonenBeperktApiHelper(_persoonBeperktFieldsSettings);
//	}

//	/// <summary>
//	/// Get BRP (GBA) 2.0.0 personen via child of PersonenQuery.
//	/// </summary>
//	/// <param name="model">Child of PersonenQuery.</param>
//	/// <returns>Child of PersonenQueryResponse</returns>
//	/// <exception cref="InvalidOperationException"></exception>
//	public async Task<(PersonenQueryResponse personenResponse, List<long>? plIds)> GetPersonen(PersonenQuery model)
//	{
//		return model switch
//		{
//			RaadpleegMetBurgerservicenummer raadpleegMetBurgerservicenummer => await GetPersonen(raadpleegMetBurgerservicenummer),
//			ZoekMetGeslachtsnaamEnGeboortedatum zoekMetGeslachtsnaamEnGeboortedatum => await GetPersonenBeperkt(zoekMetGeslachtsnaamEnGeboortedatum),
//			ZoekMetNaamEnGemeenteVanInschrijving zoekMetNaamEnGemeenteVanInschrijving => await GetPersonenBeperkt(zoekMetNaamEnGemeenteVanInschrijving),
//			ZoekMetNummeraanduidingIdentificatie zoekMetNummeraanduidingIdentificatie => await GetPersonenBeperkt(zoekMetNummeraanduidingIdentificatie),
//			ZoekMetPostcodeEnHuisnummer zoekMetPostcodeEnHuisnummer => await GetPersonenBeperkt(zoekMetPostcodeEnHuisnummer),
//			ZoekMetStraatHuisnummerEnGemeenteVanInschrijving zoekMetStraatHuisnummerEnGemeenteVanInschrijving => await GetPersonenBeperkt(zoekMetStraatHuisnummerEnGemeenteVanInschrijving),
//			ZoekMetAdresseerbaarObjectIdentificatie zoekMetAdresseerbaarObjectIdentificatie => await GetPersonenBeperkt(zoekMetAdresseerbaarObjectIdentificatie),
//			_ => throw new CustomInvalidOperationException($"Onbekend type query: {model}"),
//		};
//	}

//	/// <summary>
//	/// Get personen via bsn(s).
//	/// </summary>
//	/// <param name="model"></param>
//	/// <returns>Response object with list of complete person data</returns>
//	private async Task<(RaadpleegMetBurgerservicenummerResponse personenResponse, List<long>? plIds)> GetPersonen(RaadpleegMetBurgerservicenummer model)
//	{
//		// Validation
//		_fieldsExpandFilterService.ValidateScope(typeof(GbaPersoon), _persoonFieldsSettings.GbaFieldsSettings, model.Fields.ToList());

//		// Get personen
//		var fieldsToUseForAuthorisations = model.Fields.ToList().ConvertAll(field => _persoonFieldsSettings.GbaFieldsSettings.ShortHandMappings.ContainsKey(field)
//			? _persoonFieldsSettings.GbaFieldsSettings.ShortHandMappings[field]
//			: field);

//		List<(GbaPersoon persoon, long pl_id)> result = new();

//		(IEnumerable<(ApiModels.BRP.GbaPersoon persoon, long pl_id)>? personenPlIds, int _) = await _getAndMapPersoonService.GetPersonenMapByBsns(model.Burgerservicenummer, model.GemeenteVanInschrijving, fieldsToUseForAuthorisations);

//		_gbaPersonenApiHelper.HackLogicKinderenPartnersOuders(model.Fields.ToList(), personenPlIds?.ToList()?.ConvertAll(gbaPersoon => gbaPersoon.persoon));

//		// Extract bsns from personenPlIds
//		var bsns = personenPlIds?.Select(p => p.persoon.Burgerservicenummer).Where(bsn => !bsn.IsNullOrEmpty()).ToList();

//		// Add response fields
//		foreach (var x in personenPlIds!.Where(x => x.persoon != null))
//		{
//			x.persoon.Rni = GbaPersonenApiHelperBase.ApplyRniLogic(model.Fields.ToList(), x.persoon.Rni, _persoonFieldsSettings.GbaFieldsSettings);
//			if (x.persoon.Verblijfplaats != null)
//			{
//				GbaPersonenApiHelper.AddVerblijfplaatsInOnderzoek(model.Fields.ToList(), x.persoon.Verblijfplaats);
//			}
//			if (x.persoon.Immigratie != null)
//			{
//				GbaPersonenApiHelper.AddLandVanwaarIngeschreven(model.Fields.ToList(), x.persoon.Immigratie);
//			}

//			model.Fields = GbaPersonenApiHelper.OpschortingBijhoudingLogicRaadplegen(model.Fields.ToList(), x.persoon.OpschortingBijhouding);

//			ApiModels.BRP.GbaPersoon target = _fieldsExpandFilterService.ApplyScope(x.persoon, string.Join(",", model.Fields), _persoonFieldsSettings.GbaFieldsSettings);
//			_gbaPersonenApiHelper.RemoveInOnderzoekFromPersonCategoryIfNoFieldsAreRequested(model.Fields.ToList(), target);
//			_gbaPersonenApiHelper.RemoveInOnderzoekFromGezagsverhoudingCategoryIfNoFieldsAreRequested(model.Fields.ToList(), target);

//			// Map brpResults to Generated.RaadpleegMetBurgerservicenummerResponse
//			result.Add((GbaPersoonMapper.Map(x.persoon), x.pl_id));
//		}

//		var gezag = await _gezagService.GetGezagIfRequested(model.Fields.ToList(), bsns!);

//        // gezag is a list of personen (with field Burgerservicenummer)
//        // result is a tuple list (persoon, pl_id)
//		// if gezag Burgerservicenummer matches (persoon.Burgerservicenummer), map gezag to persoon.Gezag

//		foreach (var (persoon, _) in result)
//		{
//			if (persoon.Burgerservicenummer == null) continue;

//			foreach (var gezagPersoon in gezag)
//            {
//                if (persoon.Burgerservicenummer == gezagPersoon.Burgerservicenummer)
//                {
//                    persoon.Gezag ??= new();
//                    persoon.Gezag.Add(GbaGezagsrelatieMapper.Map(gezagPersoon));
//                }
//            }
//        }

//        return (
//			new() { Personen = result.Select(persoon => persoon.persoon).ToList() },
//			result.Select(x => x.pl_id).ToList()
//		);
//	}

//    /// <summary>
//    /// Get personen via search params (child of PersonenQuery)
//    /// </summary>
//    /// <param name="model"></param>
//    /// <returns>Response object with list of restricted person data</returns>
//    private async Task<(List<T>? personen, List<long>? plIds)> GetPersonenBeperktBase<T>(PersonenQuery model, bool? inclusiefOverledenPersonen = false) where T : GbaPersoonBeperkt
//	{
//		// Validation
//		_fieldsExpandFilterService.ValidateScope(typeof(T), _persoonBeperktFieldsSettings.GbaFieldsSettings, model.Fields.ToList());

//		// Get personen
//		var fieldsToUseForAuthorisations = model.Fields.ToList().ConvertAll(field => _persoonBeperktFieldsSettings.GbaFieldsSettings.ShortHandMappings.ContainsKey(field)
//			? _persoonBeperktFieldsSettings.GbaFieldsSettings.ShortHandMappings[field]
//			: field);

//		(IEnumerable<(T persoon, long pl_id)>? personenPlIds, int _) = await _getAndMapPersoonService.GetMapZoekPersonen<T>(model, fieldsToUseForAuthorisations);
//        List<(T persoon, long pl_id)> result = new List<(T persoon, long pl_id)>();

//        // Filter response by fields
//        if (model?.Fields?.Any() == true && personenPlIds != null)
//        {
//            // OpschortingBijhouding with code F must be ignored. If the value F is present then the entire PL must be ignored.
//            // and opschortingBijhouding with code O must be ignored if the result should not included deceased personen
//            personenPlIds = personenPlIds.Where(x => x.persoon.OpschortingBijhouding == null
//				|| x.persoon.OpschortingBijhouding?.Reden?.Code?.Equals("F") == false
//				&& x.persoon.OpschortingBijhouding?.Reden?.Code?.Equals("O") == false
//				|| x.persoon.OpschortingBijhouding?.Reden?.Code?.Equals("O") == true
//							&& inclusiefOverledenPersonen == true);

//            var bsns = personenPlIds.Select(p => p.persoon.Burgerservicenummer).Where(bsn => !bsn.IsNullOrEmpty()).ToList();

//            var gezag = await _gezagService.GetGezagIfRequested(model.Fields.ToList(), bsns);
//            var gezagPersonen = await _gezagService.GetGezagPersonenIfRequested(model.fields, gezag);

//            foreach (var x in personenPlIds.Where(x => x.persoon != null))
//			{
//				if (x.persoon is IPersoonMetGezag)
//				{
//					_gezagService.VerrijkPersonenMetGezagIfRequested(model.fields, gezag, gezagPersonen, ((IPersoonMetGezag persoon, long pl_id))x);
//				}

//				x.persoon.Rni = GbaPersonenApiHelperBase.ApplyRniLogic(model.fields, x.persoon.Rni, _persoonBeperktFieldsSettings.GbaFieldsSettings);
//				if (x.persoon.Verblijfplaats != null)
//				{
//					_gbaPersonenBeperktApiHelper.AddVerblijfplaatsBeperktInOnderzoek(model.fields, x.persoon.Verblijfplaats);
//				}


//				T target = ApplyGbaPersoonBeperktScope(x.persoon, model.fields);
//				_gbaPersonenBeperktApiHelper.RemoveBeperktInOnderzoekFromPersonCategoryIfNoFieldsAreRequested(model.fields, target);
//				result.Add((target, x.pl_id));
//			}
//		}

//		return (result.ConvertAll(gbaPersoon => gbaPersoon.persoon) ?? new List<T>(), result.Select(x => x.pl_id).ToList());
//	}

//	/// <summary>
//	/// Get personen via search params (child of PersonenQuery)
//	/// </summary>
//	/// <param name="model"></param>
//	/// <returns>Response object with list of restricted person data</returns>
//	private async Task<(ZoekMetGeslachtsnaamEnGeboortedatumResponse personenResponse, List<long>? plIds)> GetPersonenBeperkt(ZoekMetGeslachtsnaamEnGeboortedatum model)
//	{
//		// Validation
//		var personenPlIds = await GetPersonenBeperktBase<GbaPersoonBeperkt>(model, model.inclusiefOverledenPersonen);

//		// Get personen + Filter response by fields
//		return (new ZoekMetGeslachtsnaamEnGeboortedatumResponse { Personen = personenPlIds.personen }, personenPlIds.plIds);
//	}

//	/// <summary>
//	/// Get personen via search params (child of PersonenQuery)
//	/// </summary>
//	/// <param name="model"></param>
//	/// <returns>Response object with list of restricted person data</returns>
//	private async Task<(ZoekMetNaamEnGemeenteVanInschrijvingResponse personenResponse, List<long>? plIds)> GetPersonenBeperkt(ZoekMetNaamEnGemeenteVanInschrijving model)
//	{
//		// Validation
//		var personenPlIds = await GetPersonenBeperktBase<GbaPersoonBeperkt>(model, model.inclusiefOverledenPersonen);

//		// Get personen + Filter response by fields
//		return (new ZoekMetNaamEnGemeenteVanInschrijvingResponse { Personen = personenPlIds.personen }, personenPlIds.plIds);
//	}

//	/// <summary>
//	/// Get personen via search params (child of PersonenQuery)
//	/// </summary>
//	/// <param name="model"></param>
//	/// <returns>Response object with list of restricted person data</returns>
//	private async Task<(ZoekMetNummeraanduidingIdentificatieResponse personenResponse, List<long>? plIds)> GetPersonenBeperkt(ZoekMetNummeraanduidingIdentificatie model)
//	{
//		var personenPlIds = await GetPersonenBeperktBase<GbaPersoonBeperkt>(model, model.inclusiefOverledenPersonen);

//		// Get personen + Filter response by fields
//		return (new ZoekMetNummeraanduidingIdentificatieResponse { Personen = personenPlIds.personen }, personenPlIds.plIds);
//	}

//	/// <summary>
//	/// Get personen via search params (child of PersonenQuery)
//	/// </summary>
//	/// <param name="model"></param>
//	/// <returns>Response object with list of restricted person data</returns>
//	private async Task<(ZoekMetPostcodeEnHuisnummerResponse personenResponse, List<long>? plIds)> GetPersonenBeperkt(ZoekMetPostcodeEnHuisnummer model)
//	{
//		// Validation
//		var personenPlIds = await GetPersonenBeperktBase<GbaPersoonBeperkt>(model, model.inclusiefOverledenPersonen);

//		// Get personen + Filter response by fields
//		return (new ZoekMetPostcodeEnHuisnummerResponse { Personen = personenPlIds.personen }, personenPlIds.plIds);
//	}

//	/// <summary>
//	/// Get personen via search params (child of PersonenQuery)
//	/// </summary>
//	/// <param name="model"></param>
//	/// <returns>Response object with list of restricted person data</returns>
//	private async Task<(ZoekMetStraatHuisnummerEnGemeenteVanInschrijvingResponse personenResponse, List<long>? plIds)> GetPersonenBeperkt(ZoekMetStraatHuisnummerEnGemeenteVanInschrijving model)
//	{
//		// Validation
//		var personenPlIds = await GetPersonenBeperktBase<GbaPersoonBeperkt>(model, model.inclusiefOverledenPersonen);

//		// Get personen + Filter response by fields
//		return (new ZoekMetStraatHuisnummerEnGemeenteVanInschrijvingResponse { Personen = personenPlIds.personen }, personenPlIds.plIds);
//	}

//	private async Task<(ZoekMetAdresseerbaarObjectIdentificatieResponse personenResponse, List<long>? plIds)> GetPersonenBeperkt(ZoekMetAdresseerbaarObjectIdentificatie model)
//	{
//		var personenPlIds = await GetPersonenBeperktBase<GbaGezagPersoonBeperkt>(model, model.inclusiefOverledenPersonen);

//		// Get personen + Filter response by fields
//		return (new ZoekMetAdresseerbaarObjectIdentificatieResponse { Personen = personenPlIds.personen }, personenPlIds.plIds);
//	}

//	private T ApplyGbaPersoonBeperktScope<T>(T gbaPersoonBeperkt, List<string> fields) where T : GbaPersoonBeperkt
//	{
//		return _fieldsExpandFilterService.ApplyScope(gbaPersoonBeperkt, string.Join(",", fields), _persoonBeperktFieldsSettings.GbaFieldsSettings);
//	}
//}