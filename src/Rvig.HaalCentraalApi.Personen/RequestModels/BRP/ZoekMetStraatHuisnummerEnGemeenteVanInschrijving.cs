using System.Runtime.Serialization;
using Newtonsoft.Json;
using Rvig.HaalCentraalApi.Shared.Util;

namespace Rvig.HaalCentraalApi.Personen.RequestModels.BRP
{
	[DataContract]
    public class ZoekMetStraatHuisnummerEnGemeenteVanInschrijving : PersonenQuery
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
        /// Een nummer dat door de gemeente aan een adresseerbaar object is gegeven.
        /// </summary>
        /// <value>Een nummer dat door de gemeente aan een adresseerbaar object is gegeven. </value>
        [DataMember(Name = "huisnummer2", EmitDefaultValue = false)]
		[JsonConverter(typeof(StringToNullableIntegerJsonConverter))]
		public int? huisnummer2 { get; set; }

        /// <summary>
        /// Een toevoeging aan een huisnummer of een combinatie van huisnummer en huisletter die door de gemeente aan een adresseerbaar object is gegeven.
        /// </summary>
        /// <value>Een toevoeging aan een huisnummer of een combinatie van huisnummer en huisletter die door de gemeente aan een adresseerbaar object is gegeven. </value>
        [DataMember(Name = "huisnummertoevoeging", EmitDefaultValue = false)]
        public string? huisnummertoevoeging { get; set; }

        /// <summary>
        /// Een naam die door de gemeente aan een openbare ruimte is gegeven. **Gebruik van de wildcard is toegestaan bij invoer van ten minste 7 letters.** **Zoeken met tekstvelden is case-insensitive.**
        /// </summary>
        /// <value>Een naam die door de gemeente aan een openbare ruimte is gegeven. **Gebruik van de wildcard is toegestaan bij invoer van ten minste 7 letters.** **Zoeken met tekstvelden is case-insensitive.** </value>
        [DataMember(Name = "straat", EmitDefaultValue = false)]
        public string straat { get; set; } = "";

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
        public int maxItems { get; } = 30;
    }
}
