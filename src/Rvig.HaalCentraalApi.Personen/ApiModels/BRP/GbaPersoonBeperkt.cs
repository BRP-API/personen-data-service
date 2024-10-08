using Newtonsoft.Json;
using Rvig.HaalCentraalApi.Shared.ApiModels.PersonenHistorieBase;
using Rvig.HaalCentraalApi.Shared.ApiModels.Universal;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP
{
	[DataContract]
    public class GbaPersoonBeperkt
    {
        /// <summary>
        /// Gets or Sets Burgerservicenummer
        /// </summary>
        [RegularExpression("^[0-9]{9}$")]
        [DataMember(Name = "burgerservicenummer", EmitDefaultValue = false)]
        public string? Burgerservicenummer { get; set; }

        /// <summary>
        /// Gets or Sets Geboorte
        /// </summary>
        [DataMember(Name = "geboorte", EmitDefaultValue = false)]
        public GbaGeboorteBeperkt? Geboorte { get; set; }

        /// <summary>
        /// Gets or Sets GeheimhoudingPersoonsgegevens
        /// </summary>
        [DataMember(Name = "geheimhoudingPersoonsgegevens", EmitDefaultValue = false)]
        public int? GeheimhoudingPersoonsgegevens { get; set; }

        /// <summary>
        /// Gets or Sets Geslacht
        /// </summary>
        [DataMember(Name = "geslacht", EmitDefaultValue = false)]
        public Waardetabel? Geslacht { get; set; }

		/// <summary>
		/// Gets or Sets PersoonInOnderzoek
		/// </summary>
		[DataMember(Name = "persoonInOnderzoek", EmitDefaultValue = false)]
		public GbaInOnderzoek? PersoonInOnderzoek { get; set; }

		/// <summary>
		/// Gets or Sets Naam
		/// </summary>
		[DataMember(Name = "naam", EmitDefaultValue = false)]
        public GbaNaamBasis? Naam { get; set; }

        /// <summary>
        /// Gets or Sets OpschortingBijhouding
        /// </summary>
        [DataMember(Name = "opschortingBijhouding", EmitDefaultValue = false)]
        public GbaOpschortingBijhouding? OpschortingBijhouding { get; set; }

        /// <summary>
        /// Gets or Sets GemeenteVanInschrijving
        /// </summary>
        [DataMember(Name = "gemeenteVanInschrijving", EmitDefaultValue = false)]
        public Waardetabel? GemeenteVanInschrijving { get; set; }

        /// <summary>
        /// Gets or Sets Verblijfplaats
        /// </summary>
        [DataMember(Name = "verblijfplaats", EmitDefaultValue = false)]
        public GbaVerblijfplaatsBeperkt? Verblijfplaats { get; set; }

		/// <summary>
		/// Gets or Sets Rni
		/// </summary>
		[DataMember(Name = "rni", EmitDefaultValue = false)]
		public List<RniDeelnemer>? Rni { get; set; }

		/// <summary>
		/// Gets or Sets Verificatie
		/// </summary>
		[DataMember(Name = "verificatie", EmitDefaultValue = false)]
		public GbaVerificatie? Verificatie { get; set; }

		// Used for gezag. DO NOT SERIALIZE AS PART OF THE API RESPONSE.
		[XmlIgnore, JsonIgnore]
		public List<GbaPartner>? Partners { get; set; }

		[XmlIgnore, JsonIgnore]
		public List<GbaPartner>? HistorischePartners { get; set; }

		[XmlIgnore, JsonIgnore]
		public List<GbaKind>? Kinderen { get; set; }

		[XmlIgnore, JsonIgnore]
		public List<GbaOuder>? Ouders { get; set; }
	}
}
