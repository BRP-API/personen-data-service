using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using System.Runtime.Serialization;

namespace Rvig.HaalCentraalApi.Personen.ResponseModels.BRP
{
	[DataContract]
    public class ZoekMetNummeraanduidingIdentificatieResponse : PersonenQueryResponse
    {
        /// <summary>
        /// Gets or Sets Personen
        /// </summary>
        [DataMember(Name = "personen", EmitDefaultValue = false)]
        public List<GbaPersoonBeperkt>? Personen { get; set; }
    }
}
