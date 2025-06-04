using Newtonsoft.Json;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP;

public partial class GbaPersoon : IPersoonMetGezag
{
    [JsonIgnore]
    public List<GbaPartner>? HistorischePartners { get; set; }
}
