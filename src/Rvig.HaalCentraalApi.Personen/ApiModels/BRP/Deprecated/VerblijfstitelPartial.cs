using Newtonsoft.Json;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated;

public partial class GbaVerblijfstitel
{
    [JsonIgnore]
    public string? _datumOpneming { get; set; }
}
