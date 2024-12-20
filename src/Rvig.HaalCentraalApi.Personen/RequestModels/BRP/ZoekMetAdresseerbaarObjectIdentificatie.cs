using Newtonsoft.Json;
using Rvig.HaalCentraalApi.Shared.Util;
using System.Runtime.Serialization;

namespace Rvig.HaalCentraalApi.Personen.RequestModels.BRP
{
	[DataContract]
    public class ZoekMetAdresseerbaarObjectIdentificatie : PersonenQuery
	{
		/// <summary>
		/// Als je ook overleden personen in het antwoord wilt, geef dan de parameter inclusiefOverledenPersonen op met waarde True.  Zie [functionele specificaties](https://github.com/VNG-Realisatie/Haal-Centraal-BRP-bevragen/blob/v1.3.0/features/overleden_personen.feature)
		/// </summary>
		/// <value>Als je ook overleden personen in het antwoord wilt, geef dan de parameter inclusiefOverledenPersonen op met waarde True.  Zie [functionele specificaties](https://github.com/VNG-Realisatie/Haal-Centraal-BRP-bevragen/blob/v1.3.0/features/overleden_personen.feature) </value>
		[DataMember(Name = "inclusiefOverledenPersonen", EmitDefaultValue = true)]
		[JsonConverter(typeof(BooleanJsonConverter))]
		public bool inclusiefOverledenPersonen { get; set; }

		/// <summary>
		/// De verblijfplaats van de persoon kan een ligplaats, een standplaats of een verblijfsobject zijn.
		/// </summary>
		/// <value>De verblijfplaats van de persoon kan een ligplaats, een standplaats of een verblijfsobject zijn. </value>
		[DataMember(Name = "adresseerbaarObjectIdentificatie", EmitDefaultValue = false)]
        public string adresseerbaarObjectIdentificatie { get; set; } = "";
        /// <summary>
        /// Het maximaal aantal resultaten in de response. Wanneer het aantal objecten boven deze waarde komt wordt een foutmelding gegeven.
        /// TODO: Dit moet configurabel worden gemaakt met issue: https://github.com/BRP-API/personen-data-service/issues/42
        /// </summary>
        /// <value>De waarde van het maximaal aantal resultaten</value>
        //[DataMember(Name = "maxItems", EmitDefaultValue = false)]
        public int maxItems { get; } = 30;
    }
}
