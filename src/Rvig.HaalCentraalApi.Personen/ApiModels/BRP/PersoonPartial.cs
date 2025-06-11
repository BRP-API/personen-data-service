using Newtonsoft.Json;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP;

public partial class GbaPersoon
{
    [JsonIgnore]
    public List<GbaPartner>? HistorischePartners { get; set; }
}
