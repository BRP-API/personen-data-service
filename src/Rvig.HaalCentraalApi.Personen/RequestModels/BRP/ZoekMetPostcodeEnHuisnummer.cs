using System.Runtime.Serialization;
using Newtonsoft.Json;
using Rvig.HaalCentraalApi.Shared.Util;

namespace Rvig.HaalCentraalApi.Personen.RequestModels.BRP
{
	[DataContract]
	public class ZoekMetPostcodeEnHuisnummer : PersonenQuery
    {
		/// <summary>
		/// Als je ook overleden personen in het antwoord wilt, geef dan de parameter inclusiefOverledenPersonen op met waarde True.  Zie [functionele specificaties](https://github.com/VNG-Realisatie/Haal-Centraal-BRP-bevragen/blob/v1.3.0/features/overleden_personen.feature)
		/// </summary>
		/// <value>Als je ook overleden personen in het antwoord wilt, geef dan de parameter inclusiefOverledenPersonen op met waarde True.  Zie [functionele specificaties](https://github.com/VNG-Realisatie/Haal-Centraal-BRP-bevragen/blob/v1.3.0/features/overleden_personen.feature) </value>
		[DataMember(Name = "inclusiefOverledenPersonen", EmitDefaultValue = true)]
		[JsonConverter(typeof(BooleanJsonConverter))]
		public bool inclusiefOverledenPersonen { get; set; }

		/// <summary>
		/// Een toevoeging aan een huisnummer in de vorm van een letter die door de gemeente aan een adresseerbaar object is gegeven.
		/// </summary>
		/// <value>Een toevoeging aan een huisnummer in de vorm van een letter die door de gemeente aan een adresseerbaar object is gegeven. </value>
        [DataMember(Name = "huisletter", EmitDefaultValue = false)]
        public string? huisletter { get; set; }

		/// <summary>
		/// Een nummer dat door de gemeente aan een adresseerbaar object is gegeven.
		/// </summary>
		/// <value>Een nummer dat door de gemeente aan een adresseerbaar object is gegeven. </value>
		[DataMember(Name = "huisnummer", EmitDefaultValue = false)]
		[JsonConverter(typeof(StringToNullableIntegerJsonConverter))]
		public int? huisnummer { get; set; }

        /// <summary>
        /// Een toevoeging aan een huisnummer of een combinatie van huisnummer en huisletter die door de gemeente aan een adresseerbaar object is gegeven.
        /// </summary>
        /// <value>Een toevoeging aan een huisnummer of een combinatie van huisnummer en huisletter die door de gemeente aan een adresseerbaar object is gegeven. </value>
        [DataMember(Name = "huisnummertoevoeging", EmitDefaultValue = false)]
        public string? huisnummertoevoeging { get; set; }

        /// <summary>
        /// De door PostNL vastgestelde code die bij een bepaalde combinatie van een straatnaam en een huisnummer hoort.
        /// </summary>
        /// <value>De door PostNL vastgestelde code die bij een bepaalde combinatie van een straatnaam en een huisnummer hoort. </value>
        [DataMember(Name = "postcode", EmitDefaultValue = false)]
        public string postcode { get; set; } = "";

		/// <summary>
		/// Je kunt alleen zoeken met een volledig geboortedatum. Zie [functionele specificaties](https://github.com/VNG-Realisatie/Haal-Centraal-BRP-bevragen/blob/v1.3.0/features/parametervalidatie.feature)
		/// </summary>
		/// <value>Je kunt alleen zoeken met een volledig geboortedatum. Zie [functionele specificaties](https://github.com/VNG-Realisatie/Haal-Centraal-BRP-bevragen/blob/v1.3.0/features/parametervalidatie.feature) </value>
		[DataMember(Name = "geboortedatum", EmitDefaultValue = false)]
		public string? geboortedatum { get; set; }
        /// <summary>
        /// Het maximaal aantal resultaten in de response. Wanneer het aantal objecten boven deze waarde komt wordt een foutmelding gegeven.
        /// TODO: Dit moet configurabel worden gemaakt met issue: https://github.com/BRP-API/personen-data-service/issues/42
        /// </summary>
        /// <value>De waarde van het maximaal aantal resultaten</value>
        //[DataMember(Name = "maxItems", EmitDefaultValue = false)]
        public int maxItems { get; } = 30;

        /// <summary>
        /// De (geslachts)naam waarvan de eventueel aanwezige voorvoegsels zijn afgesplitst. **Gebruik van de wildcard is toegestaan bij invoer van ten minste 3 letters.** **Zoeken met tekstvelden is case-insensitive.**
        /// </summary>
        /// <value>De (geslachts)naam waarvan de eventueel aanwezige voorvoegsels zijn afgesplitst. **Gebruik van de wildcard is toegestaan bij invoer van ten minste 3 letters.** **Zoeken met tekstvelden is case-insensitive.** </value>
        [DataMember(Name = "geslachtsnaam", EmitDefaultValue = false)]
        public string geslachtsnaam { get; set; } = "";
    }
}
