using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using NJsonSchema.Converters;
using Rvig.HaalCentraalApi.Shared.Validation;

namespace Rvig.HaalCentraalApi.Personen.ResponseModels.BRP
{
    [DataContract]
    [JsonConverter(typeof(JsonInheritanceConverter), "type")]
    [JsonInheritance("ZoekMetGeslachtsnaamEnGeboortedatum", typeof(ZoekMetGeslachtsnaamEnGeboortedatumResponse))]
    [JsonInheritance("ZoekMetNaamEnGemeenteVanInschrijving", typeof(ZoekMetNaamEnGemeenteVanInschrijvingResponse))]
    [JsonInheritance("RaadpleegMetBurgerservicenummer", typeof(RaadpleegMetBurgerservicenummerResponse))]
    [JsonInheritance("ZoekMetPostcodeEnHuisnummer", typeof(ZoekMetPostcodeEnHuisnummerResponse))]
    [JsonInheritance("ZoekMetStraatHuisnummerEnGemeenteVanInschrijving", typeof(ZoekMetStraatHuisnummerEnGemeenteVanInschrijvingResponse))]
    [JsonInheritance("ZoekMetNummeraanduidingIdentificatie", typeof(ZoekMetNummeraanduidingIdentificatieResponse))]
    [JsonInheritance("ZoekMetAdresseerbaarObjectIdentificatie", typeof(ZoekMetAdresseerbaarObjectIdentificatieResponse))]
    public class PersonenQueryResponse
    {
        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ValidationErrorMessages), ErrorMessageResourceName = nameof(ValidationErrorMessages.Required))]
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string? Type { get; set; }
    }
}
