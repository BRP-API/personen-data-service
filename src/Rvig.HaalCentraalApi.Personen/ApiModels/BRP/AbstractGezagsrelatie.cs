using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using NJsonSchema.Converters;
using Rvig.HaalCentraalApi.Personen.Util;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP
{
	[DataContract]
    [JsonConverter(typeof(AbstractGezagsrelatieJsonInheritanceConverter), "type")]
	[JsonInheritance("EenhoofdigOuderlijkGezag", typeof(EenhoofdigOuderlijkGezag))]
	[JsonInheritance("GezagNietTeBepalen", typeof(GezagNietTeBepalen))]
	[JsonInheritance("GezamenlijkGezag", typeof(GezamenlijkGezag))]
	[JsonInheritance("TijdelijkGeenGezag", typeof(TijdelijkGeenGezag))]
	[JsonInheritance("TweehoofdigOuderlijkGezag", typeof(TweehoofdigOuderlijkGezag))]
	[JsonInheritance("Voogdij", typeof(Voogdij))]
    public class AbstractGezagsrelatie
    {
        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [Required]
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string? Type { get; set; }
    }
}
