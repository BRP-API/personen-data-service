using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP
{
	[DataContract]
    public class TijdelijkGeenGezag : AbstractGezagsrelatie
    {
        /// <summary>
        /// Toelichting bij tijdelijk geen gezag.
        /// </summary>
        /// <value>Toelichting bij tijdelijk geen gezag.</value>
        [MaxLength(400)]
        [DataMember(Name = "toelichting", EmitDefaultValue = false)]
        public string? Toelichting { get; set; }
        /// <summary>
        /// Minderjarige bij tijdelijk geen gezag.
        /// </summary>
        /// <value>Minderjarige bij tijdelijk geen gezag.</value>
        [DataMember(Name = "minderjarige", EmitDefaultValue = false)]
        public Minderjarige? Minderjarige { get; set; }
    }
}
