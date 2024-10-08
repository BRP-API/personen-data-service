using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP
{
	[DataContract]
    public class Meerderjarige
    {
        /// <summary>
        /// Gets or Sets Burgerservicenummer
        /// </summary>
        [RegularExpression("^[0-9]{9}$")]
        [DataMember(Name = "burgerservicenummer", EmitDefaultValue = false)]
        public string? Burgerservicenummer { get; set; }
    }
}
