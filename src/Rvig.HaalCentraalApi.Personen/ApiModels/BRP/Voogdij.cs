using System.Runtime.Serialization;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP
{
	[DataContract]
    public class Voogdij : AbstractGezagsrelatie
    {
        /// <summary>
        /// Gets or Sets Derden
        /// </summary>
        [DataMember(Name = "derden", EmitDefaultValue = false)]
        public List<Meerderjarige>? Derden { get; set; }

        /// <summary>
        /// Gets or Sets Minderjarige
        /// </summary>
        [DataMember(Name = "minderjarige", EmitDefaultValue = false)]
        public Minderjarige? Minderjarige { get; set; }
    }
}
