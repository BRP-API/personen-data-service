using Rvig.HaalCentraalApi.Shared.ApiModels.PersonenHistorieBase;
using Rvig.HaalCentraalApi.Shared.ApiModels.Universal;
using System.Runtime.Serialization;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP
{
	[DataContract]
    public class GbaNaamPersoon : GbaNaamBasis
    {
        /// <summary>
        /// Gets or Sets AanduidingNaamgebruik
        /// </summary>
        [DataMember(Name = "aanduidingNaamgebruik", EmitDefaultValue = false)]
        public Waardetabel? AanduidingNaamgebruik { get; set; }
    }
}
