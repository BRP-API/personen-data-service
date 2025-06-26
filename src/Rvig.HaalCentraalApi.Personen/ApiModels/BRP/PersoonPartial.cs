using Newtonsoft.Json;
using Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Common;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP;

public partial class GbaPersoon
{
    [JsonIgnore]
    public List<GbaPartner>? HistorischePartners { get; set; }
}
