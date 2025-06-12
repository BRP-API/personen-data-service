using Newtonsoft.Json;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Common;

public partial class GbaNationaliteit
{
    [JsonIgnore]
    public string? _datumOpneming { get; set; }
}
