using System.Runtime.Serialization;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP
{
	[DataContract]
    public class GezamenlijkGezag : AbstractGezagsrelatie
    {
        /// <summary>
        /// Gets or Sets Ouder
        /// </summary>
        [DataMember(Name = "ouder", EmitDefaultValue = false)]
        public GezagOuder? Ouder { get; set; }

        /// <summary>
        /// Gets or Sets Derde
        /// </summary>
        [DataMember(Name = "derde", EmitDefaultValue = false)]
        public Meerderjarige? Derde { get; set; }

        /// <summary>
        /// Gets or Sets Minderjarige
        /// </summary>
        [DataMember(Name = "minderjarige", EmitDefaultValue = false)]
        public Minderjarige? Minderjarige { get; set; }
    }
}
