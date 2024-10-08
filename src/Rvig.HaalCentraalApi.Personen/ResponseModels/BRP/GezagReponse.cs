using Rvig.HaalCentraalApi.Shared.Util;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;

namespace Rvig.HaalCentraalApi.Personen.ResponseModels.BRP
{
	[DataContract]
    public class GezagResponse
    {
		/// <summary>
		/// Gets or Sets Burgerservicenummer
		/// </summary>
		[DataMember(Name = "personen", EmitDefaultValue = false)]
		[JsonConverter(typeof(JsonArrayConverter))]
		public List<GbaPersoon> personen { get; set; } = null!;
	}
}
