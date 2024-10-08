using Rvig.HaalCentraalApi.Shared.ApiModels.Universal;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP
{
    /// <summary>
    /// * **aanduiding** - Geeft aan of persoon een oproep moet ontvangen voor verkiezingen voor het Europees parlement. Wordt gevuld op basis van de waarden die voorkomen in de tabel &#39;Europees_Kiesrecht&#39; uit de Haal-Centraal-BRP-tabellen-bevragen API. 
    /// </summary>
    [DataContract]
    public class GbaEuropeesKiesrecht
    {
        /// <summary>
        /// Gets or Sets Aanduiding
        /// </summary>
        [DataMember(Name = "aanduiding", EmitDefaultValue = false)]
        public Waardetabel? Aanduiding { get; set; }

        /// <summary>
        /// Gets or Sets EinddatumUitsluiting
        /// </summary>
        [RegularExpression("^[0-9]{8}$")]
        [DataMember(Name = "einddatumUitsluiting", EmitDefaultValue = false)]
        public string? EinddatumUitsluiting { get; set; }
    }
}
