using Rvig.Base.App;
using Rvig.Data.Personen.Mappers;
using Rvig.Data.Personen.Repositories;
using Rvig.Data.Personen.Services;
using Rvig.HaalCentraalApi.Personen.Interfaces;
using System.Collections.Generic;
using System;
using Rvig.HaalCentraalApi.Personen.Services;
using Rvig.HaalCentraalApi.Personen.Validation.RequestModelValidators;
using Rvig.Data.Base.Gezag.Repositories;
using Microsoft.AspNetCore.Builder;

var servicesDictionary = new Dictionary<Type, Type>
{
	// BRP Data
	{ typeof(IRvigPersoonRepo), typeof(RvigPersoonRepo) },
	{ typeof(IRvigPersoonBeperktRepo), typeof(RvigPersoonBeperktRepo) },
	{ typeof(IRvIGDataPersonenMapper), typeof(RvIGDataPersonenMapper) },
	{ typeof(IGetAndMapGbaPersonenService), typeof(GetAndMapGbaPersonenService) },

	// BRP API
	{ typeof(IGbaPersonenApiService), typeof(GbaPersonenApiService) },
	{ typeof(IGezagTransformer), typeof(GezagTransformer) },

	// Gezag Data
	{ typeof(IRepoGezagsrelatie), typeof(RepoGezagsrelatie) }
};

var validatorList = new List<Type>
{
	typeof(RaadpleegMetBurgerservicenummerValidator),
	typeof(ZoekMetGeslachtsnaamEnGeboortedatumValidator),
	typeof(ZoekMetNaamEnGemeenteVanInschrijvingValidator),
	typeof(ZoekMetNummeraanduidingIdentificatieValidator),
	typeof(ZoekMetPostcodeEnHuisnummerValidator),
	typeof(ZoekMetStraatHuisnummerEnGemeenteVanInschrijvingValidator)
};

// This is used to give configurable options to deactive the authorization layer. This was determined by the Haal Centraal crew and the RvIG to be required.
// The reason for this requirement has to do with Kubernetes and a multi pod setup where another pod is responsible for authorizations therefore making this a sessionless API.
static bool UseAuthorizationLayer(WebApplicationBuilder builder)
{
	// We no longer perform authorization checks in this app as this is taken care of by another application and therefore no longer the responsibility of this app.
	return false;
}
RvigBaseApp.Init(servicesDictionary, validatorList, UseAuthorizationLayer, "BRP Personen API");