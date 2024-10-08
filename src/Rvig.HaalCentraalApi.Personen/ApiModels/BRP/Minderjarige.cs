using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP
{
	[DataContract]
    public class Minderjarige
    {
        /// <summary>
        /// Gets or Sets Burgerservicenummer
        /// </summary>
        [Required]
        [RegularExpression("^[0-9]{9}$")]
        [DataMember(Name = "burgerservicenummer", EmitDefaultValue = false)]
        public string? Burgerservicenummer { get; set; }
    }
}
