using Rvig.HaalCentraalApi.Shared.ApiModels.Universal;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP
{
    /// <summary>
    /// Gegevens over het het immigratie van een persoon.   * **landVanWaarIngeschreven** : het land waar de persoon woonde voor (her)vestiging in Nederland. 
    /// </summary>
    [DataContract]
    public class GbaImmigratie
    {
        /// <summary>
        /// Gets or Sets DatumVestigingInNederland
        /// </summary>
        [RegularExpression("^[0-9]{8}$")]
        [DataMember(Name = "datumVestigingInNederland", EmitDefaultValue = false)]
        public string? DatumVestigingInNederland { get; set; }

        /// <summary>
        /// Gets or Sets LandVanwaarIngeschreven
        /// </summary>
        [DataMember(Name = "landVanwaarIngeschreven", EmitDefaultValue = false)]
        public Waardetabel? LandVanwaarIngeschreven { get; set; }
    }
}
