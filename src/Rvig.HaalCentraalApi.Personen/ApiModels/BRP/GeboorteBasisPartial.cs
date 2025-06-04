using Newtonsoft.Json;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP;

public partial class GeboorteBasis
{
    [JsonIgnore]
    public int? DatumJaar => !string.IsNullOrWhiteSpace(Datum) && !Datum.Equals("00000000") && Datum.Length >= 4 ? int.Parse(Datum.Substring(0, 4)) : null;

    [JsonIgnore]
    public int? DatumMaand => !string.IsNullOrWhiteSpace(Datum) && !Datum.Equals("00000000") && Datum.Length >= 6 ? int.Parse(Datum.Substring(4, 2)) : null;

    [JsonIgnore]
    public int? DatumDag => !string.IsNullOrWhiteSpace(Datum) && !Datum.Equals("00000000") && Datum.Length == 8 ? int.Parse(Datum.Substring(6, 2)) : null;
}
