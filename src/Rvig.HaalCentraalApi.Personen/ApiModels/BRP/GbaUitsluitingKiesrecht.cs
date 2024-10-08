using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP
{
    [DataContract]
    public class GbaUitsluitingKiesrecht
    {
        /// <summary>
        /// Gets or Sets UitgeslotenVanKiesrecht
        /// </summary>
        [DataMember(Name = "uitgeslotenVanKiesrecht", EmitDefaultValue = false)]
        public bool? UitgeslotenVanKiesrecht { get; set; }

        /// <summary>
        /// Gets or Sets Einddatum
        /// </summary>
        [RegularExpression("^[0-9]{8}$")]
        [DataMember(Name = "einddatum", EmitDefaultValue = false)]
        public string? Einddatum { get; set; }
    }
}
