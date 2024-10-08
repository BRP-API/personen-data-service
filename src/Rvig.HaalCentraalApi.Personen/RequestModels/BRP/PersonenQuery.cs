using System.Runtime.Serialization;
using Newtonsoft.Json;
using NJsonSchema.Converters;
using Rvig.HaalCentraalApi.Personen.Util;
using Rvig.HaalCentraalApi.Shared.Util;

namespace Rvig.HaalCentraalApi.Personen.RequestModels.BRP
{
	[DataContract]
    [JsonConverter(typeof(PersonenQueryJsonInheritanceConverter), "type")]
	[JsonInheritance("ZoekMetGeslachtsnaamEnGeboortedatum", typeof(ZoekMetGeslachtsnaamEnGeboortedatum))]
    [JsonInheritance("ZoekMetNaamEnGemeenteVanInschrijving", typeof(ZoekMetNaamEnGemeenteVanInschrijving))]
    [JsonInheritance("RaadpleegMetBurgerservicenummer", typeof(RaadpleegMetBurgerservicenummer))]
    [JsonInheritance("ZoekMetPostcodeEnHuisnummer", typeof(ZoekMetPostcodeEnHuisnummer))]
    [JsonInheritance("ZoekMetStraatHuisnummerEnGemeenteVanInschrijving", typeof(ZoekMetStraatHuisnummerEnGemeenteVanInschrijving))]
    [JsonInheritance("ZoekMetNummeraanduidingIdentificatie", typeof(ZoekMetNummeraanduidingIdentificatie))]
    [JsonInheritance("ZoekMetAdresseerbaarObjectIdentificatie", typeof(ZoekMetAdresseerbaarObjectIdentificatie))]
    public class PersonenQuery
    {
        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string? type { get; set; }

        /// <summary>
        /// Hiermee kun je de inhoud van de resource naar behoefte aanpassen door een lijst van paden die verwijzen naar de gewenste velden op te nemen ([zie functionele specificaties &#39;fields&#39; properties](https://raw.githubusercontent.com/VNG-Realisatie/Haal-Centraal-BRP-bevragen/develop/features/fields.feature)).  De te gebruiken paden zijn beschreven in [fields-Persoon.csv](https://raw.githubusercontent.com/VNG-Realisatie/Haal-Centraal-BRP-bevragen/develop/features/fields-Persoon.csv) (voor gebruik fields bij raadplegen) en [fields-PersoonBeperkt.csv](https://github.com/VNG-Realisatie/Haal-Centraal-BRP-bevragen/blob/develop/features/fields-PersoonBeperkt.csv) (voor gebruik fields bij zoeken) waarbij in de eerste kolom het fields-pad staat en in de tweede kolom het volledige pad naar het gewenste veld.
        /// </summary>
        /// <value>Hiermee kun je de inhoud van de resource naar behoefte aanpassen door een lijst van paden die verwijzen naar de gewenste velden op te nemen ([zie functionele specificaties &#39;fields&#39; properties](https://raw.githubusercontent.com/VNG-Realisatie/Haal-Centraal-BRP-bevragen/develop/features/fields.feature)).  De te gebruiken paden zijn beschreven in [fields-Persoon.csv](https://raw.githubusercontent.com/VNG-Realisatie/Haal-Centraal-BRP-bevragen/develop/features/fields-Persoon.csv) (voor gebruik fields bij raadplegen) en [fields-PersoonBeperkt.csv](https://github.com/VNG-Realisatie/Haal-Centraal-BRP-bevragen/blob/develop/features/fields-PersoonBeperkt.csv) (voor gebruik fields bij zoeken) waarbij in de eerste kolom het fields-pad staat en in de tweede kolom het volledige pad naar het gewenste veld. </value>
        [DataMember(Name = "fields", EmitDefaultValue = false)]
		[JsonConverter(typeof(JsonArrayConverter))]
		public List<string> fields { get; set; } = new List<string>();

		/// <summary>
		/// Een code die aangeeft in welke gemeente de persoon woont, of de laatste gemeente waar de persoon heeft gewoond, of de gemeente waar de persoon voor het eerst is ingeschreven.
		/// </summary>
		/// <value>Een code die aangeeft in welke gemeente de persoon woont, of de laatste gemeente waar de persoon heeft gewoond, of de gemeente waar de persoon voor het eerst is ingeschreven. </value>
        [DataMember(Name = "gemeenteVanInschrijving", EmitDefaultValue = false)]
        public virtual string? gemeenteVanInschrijving { get; set; }
    }
}