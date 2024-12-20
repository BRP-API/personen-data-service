using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP
{
	[DataContract]
    public class GezagNietTeBepalen : AbstractGezagsrelatie
    {
        /// <summary>
		/// Toelichting bij gezag niet te bepalen.
		/// </summary>
		/// <value>Toelichting bij gezag niet te bepalen.</value>
		[MaxLength(400)]
        [DataMember(Name = "toelichting", EmitDefaultValue = false)]
        public string? Toelichting { get; set; }

        /// <summary>
        /// Minderjarige bij gezag niet te bepalen.
        /// </summary>
        /// <value>Minderjarige bij gezag niet te bepalen.</value>
        [DataMember(Name = "minderjarige", EmitDefaultValue = false)]
        public Minderjarige? Minderjarige { get; set; }
    }
}
