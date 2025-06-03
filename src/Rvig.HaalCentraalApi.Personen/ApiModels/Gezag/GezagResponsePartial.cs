using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.Gezag
{
    partial class GezagResponse : IGezagResponseWithPersonen
    {
        IEnumerable<object> IGezagResponseWithPersonen.Personen => Personen.Cast<object>().ToList();
    }
}
