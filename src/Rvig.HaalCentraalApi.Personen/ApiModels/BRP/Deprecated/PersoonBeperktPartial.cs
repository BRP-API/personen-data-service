using Newtonsoft.Json;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated;

public partial class GbaPersoonBeperkt
{
    [JsonIgnore]
    public List<GbaPartner>? HistorischePartners { get; set; }

    [JsonIgnore]
    public List<GbaPartner>? Partners { get; set; }

    [JsonIgnore]
    public List<GbaKind>? Kinderen { get; set; }

    [JsonIgnore]
    public List<GbaOuder>? Ouders { get; set; }
}
