using Rvig.HaalCentraalApi.Shared.ApiModels.Universal;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP
{
	[DataContract]
    public class GbaOverlijden
    {
        /// <summary>
        /// Gets or Sets Land
        /// </summary>
        [DataMember(Name = "land", EmitDefaultValue = false)]
        public Waardetabel? Land { get; set; }

        /// <summary>
        /// Gets or Sets Plaats
        /// </summary>
        [DataMember(Name = "plaats", EmitDefaultValue = false)]
        public Waardetabel? Plaats { get; set; }

        /// <summary>
        /// Gets or Sets InOnderzoek
        /// </summary>
        [DataMember(Name = "inOnderzoek", EmitDefaultValue = false)]
        public GbaInOnderzoek? InOnderzoek { get; set; }

		/// <summary>
		/// Gets or Sets Datum
		/// </summary>
		[RegularExpression("^[0-9]{8}$")]
		[DataMember(Name = "datum", EmitDefaultValue = false)]
		public string? Datum { get; set; }
	}
}
