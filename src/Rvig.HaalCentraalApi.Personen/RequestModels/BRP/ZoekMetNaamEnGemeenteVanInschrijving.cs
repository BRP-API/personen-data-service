using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Rvig.HaalCentraalApi.Shared.Util;
using Rvig.HaalCentraalApi.Shared.Validation;

namespace Rvig.HaalCentraalApi.Personen.RequestModels.BRP
{
    [DataContract]
    public class ZoekMetNaamEnGemeenteVanInschrijving : PersonenQuery
    {
		/// <summary>
		/// Als je ook overleden personen in het antwoord wilt, geef dan de parameter inclusiefOverledenPersonen op met waarde True.  Zie [functionele specificaties](https://github.com/VNG-Realisatie/Haal-Centraal-BRP-bevragen/blob/v1.3.0/features/overleden_personen.feature)
		/// </summary>
		/// <value>Als je ook overleden personen in het antwoord wilt, geef dan de parameter inclusiefOverledenPersonen op met waarde True.  Zie [functionele specificaties](https://github.com/VNG-Realisatie/Haal-Centraal-BRP-bevragen/blob/v1.3.0/features/overleden_personen.feature) </value>
		[DataMember(Name = "inclusiefOverledenPersonen", EmitDefaultValue = true)]
		[JsonConverter(typeof(BooleanJsonConverter))]
		public bool inclusiefOverledenPersonen { get; set; }

		/// <summary>
		/// Geeft aan dat de persoon een man of een vrouw is, of dat het geslacht (nog) onbekend is.
		/// </summary>
		/// <value>Geeft aan dat de persoon een man of een vrouw is, of dat het geslacht (nog) onbekend is. </value>
        [DataMember(Name = "geslacht", EmitDefaultValue = false)]
        public string? geslacht { get; set; }

        /// <summary>
        /// De (geslachts)naam waarvan de eventueel aanwezige voorvoegsels zijn afgesplitst. **Gebruik van de wildcard is toegestaan bij invoer van ten minste 3 letters.** **Zoeken met tekstvelden is case-insensitive.**
        /// </summary>
        /// <value>De (geslachts)naam waarvan de eventueel aanwezige voorvoegsels zijn afgesplitst. **Gebruik van de wildcard is toegestaan bij invoer van ten minste 3 letters.** **Zoeken met tekstvelden is case-insensitive.** </value>
        [DataMember(Name = "geslachtsnaam", EmitDefaultValue = false)]
        public string geslachtsnaam { get; set; } = "";

        /// <summary>
        /// Deel van de geslachtsnaam dat vooraf gaat aan de rest van de geslachtsnaam. **Zoeken met tekstvelden is case-insensitive.**
        /// </summary>
        /// <value>Deel van de geslachtsnaam dat vooraf gaat aan de rest van de geslachtsnaam. **Zoeken met tekstvelden is case-insensitive.** </value>
        [DataMember(Name = "voorvoegsel", EmitDefaultValue = false)]
        public string? voorvoegsel { get; set; }

        /// <summary>
        /// De verzameling namen die, gescheiden door spaties, aan de geslachtsnaam voorafgaat. **Gebruik van de wildcard is toegestaan bij invoer van ten minste 3 letters.** **Zoeken met tekstvelden is case-insensitive.**
        /// </summary>
        /// <value>De verzameling namen die, gescheiden door spaties, aan de geslachtsnaam voorafgaat. **Gebruik van de wildcard is toegestaan bij invoer van ten minste 3 letters.** **Zoeken met tekstvelden is case-insensitive.** </value>
        [DataMember(Name = "voornamen", EmitDefaultValue = false)]
        public string voornamen { get; set; } = "";

        /// <summary>
        /// Een code die aangeeft in welke gemeente de persoon woont, of de laatste gemeente waar de persoon heeft gewoond, of de gemeente waar de persoon voor het eerst is ingeschreven.
        /// </summary>
        /// <value>Een code die aangeeft in welke gemeente de persoon woont, of de laatste gemeente waar de persoon heeft gewoond, of de gemeente waar de persoon voor het eerst is ingeschreven. </value>
        [DataMember(Name = "gemeenteVanInschrijving", EmitDefaultValue = false)]
        public override string? gemeenteVanInschrijving { get; set; }

        /// <summary>
        /// Het maximaal aantal resultaten in de response. Wanneer het aantal objecten boven deze waarde komt wordt een foutmelding gegeven.
        /// TODO: Dit moet configurabel worden gemaakt met issue: https://github.com/BRP-API/personen-data-service/issues/42
        /// </summary>
        /// <value>De waarde van het maximaal aantal resultaten</value>
        //[DataMember(Name = "maxItems", EmitDefaultValue = false)]
        public int maxItems { get; } = 10;
    }
}
