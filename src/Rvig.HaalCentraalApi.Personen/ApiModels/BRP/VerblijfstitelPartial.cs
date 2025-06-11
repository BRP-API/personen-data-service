using Newtonsoft.Json;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP;

public partial class GbaVerblijfstitel
{
    [JsonIgnore]
    public string? _datumOpneming { get; set; }
}
