using Rvig.Base.App;
using Rvig.Data.Personen.Mappers;
using Rvig.Data.Personen.Repositories;
using Rvig.Data.Personen.Services;
using Rvig.HaalCentraalApi.Personen.Interfaces;
using Rvig.HaalCentraalApi.Personen.Services;
using Rvig.HaalCentraalApi.Personen.Repositories;
using System.Collections.Generic;
using System;

var servicesDictionary = new Dictionary<Type, Type>
{
	// BRP Data
	{ typeof(IRvigPersoonRepo), typeof(RvigPersoonRepo) },
	{ typeof(IRvigPersoonBeperktRepo), typeof(RvigPersoonBeperktRepo) },
	{ typeof(IRvIGDataPersonenMapper), typeof(RvIGDataPersonenMapper) },
	{ typeof(IGetAndMapGbaPersonenService), typeof(GetAndMapGbaPersonenService) },
	{ typeof(IGezagPersonenService), typeof(GezagPersonenService) },
	
	// BRP API
	{ typeof(IGbaPersonenApiService), typeof(GbaPersonenApiService) },
	{ typeof(IGezagService), typeof(GezagServiceDeprecated) },
	{ typeof(GezagService), typeof(GezagService) }, // actual gezag service without interface

	// Gezag Data
	{ typeof(IRepoGezagsrelatie), typeof(RepoGezagsrelatie) }
};

var validatorList = new List<Type>
{
};

RvigBaseApp.Init(servicesDictionary, validatorList, "BRP Personen API");