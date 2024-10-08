using System.Runtime.Serialization;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP
{
	[DataContract]
    public class TweehoofdigOuderlijkGezag : AbstractGezagsrelatie
    {
        /// <summary>
        /// Gets or Sets Ouders
        /// </summary>
        [DataMember(Name = "ouders", EmitDefaultValue = false)]
        public List<GezagOuder>? Ouders { get; set; }

        /// <summary>
        /// Gets or Sets Minderjarige
        /// </summary>
        [DataMember(Name = "minderjarige", EmitDefaultValue = false)]
        public Minderjarige? Minderjarige { get; set; }
    }
}
