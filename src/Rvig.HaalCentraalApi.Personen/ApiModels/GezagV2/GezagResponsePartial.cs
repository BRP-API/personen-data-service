using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.GezagV2
{
    partial class GezagResponse : IGezagResponseWithPersonen
    {
        IEnumerable<object> IGezagResponseWithPersonen.Personen => Personen.Cast<object>().ToList();
    }
}
