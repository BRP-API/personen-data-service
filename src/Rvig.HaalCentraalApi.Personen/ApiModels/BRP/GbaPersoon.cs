using Newtonsoft.Json;
using Rvig.HaalCentraalApi.Shared.ApiModels.PersonenHistorieBase;
using Rvig.HaalCentraalApi.Shared.ApiModels.Universal;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP
{
	[DataContract]
    public class GbaPersoon : IPersoonMetGezag
    {
        /// <summary>
        /// Het A-nummer van de persoon
        /// </summary>
        /// <value>Het A-nummer van de persoon </value>
        [RegularExpression("^[0-9]{10}$")]
        [DataMember(Name = "aNummer", EmitDefaultValue = false)]
        public string? ANummer { get; set; }

        /// <summary>
        /// Gets or Sets Burgerservicenummer
        /// </summary>
        [RegularExpression("^[0-9]{9}$")]
        [DataMember(Name = "burgerservicenummer", EmitDefaultValue = false)]
        public string? Burgerservicenummer { get; set; }

        /// <summary>
        /// Gets or Sets DatumEersteInschrijvingGBA
        /// </summary>
        [RegularExpression("^[0-9]{8}$")]
        [DataMember(Name = "datumEersteInschrijvingGBA", EmitDefaultValue = false)]
        public string? DatumEersteInschrijvingGBA { get; set; }

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
        /// Gets or Sets GezagInOnderzoek
        /// </summary>
        [DataMember(Name = "gezagInOnderzoek", EmitDefaultValue = false)]
        public GbaInOnderzoek? GezagInOnderzoek { get; set; }

        /// <summary>
        /// Gets or Sets UitsluitingKiesrecht
        /// </summary>
        [DataMember(Name = "uitsluitingKiesrecht", EmitDefaultValue = false)]
        public GbaUitsluitingKiesrecht? UitsluitingKiesrecht { get; set; }

        /// <summary>
        /// Gets or Sets EuropeesKiesrecht
        /// </summary>
        [DataMember(Name = "europeesKiesrecht", EmitDefaultValue = false)]
        public GbaEuropeesKiesrecht? EuropeesKiesrecht { get; set; }

        /// <summary>
        /// Gets or Sets Naam
        /// </summary>
        [DataMember(Name = "naam", EmitDefaultValue = false)]
        public GbaNaamPersoon? Naam { get; set; }

        /// <summary>
        /// Gets or Sets Nationaliteiten
        /// </summary>
        [DataMember(Name = "nationaliteiten", EmitDefaultValue = false)]
        public List<GbaNationaliteit>? Nationaliteiten { get; set; }

        /// <summary>
        /// Gets or Sets Geboorte
        /// </summary>
        [DataMember(Name = "geboorte", EmitDefaultValue = false)]
        public GbaGeboorte? Geboorte { get; set; }

        /// <summary>
        /// Gets or Sets OpschortingBijhouding
        /// </summary>
        [DataMember(Name = "opschortingBijhouding", EmitDefaultValue = false)]
        public GbaOpschortingBijhouding? OpschortingBijhouding { get; set; }

        /// <summary>
        /// Gets or Sets Overlijden
        /// </summary>
        [DataMember(Name = "overlijden", EmitDefaultValue = false)]
        public GbaOverlijden? Overlijden { get; set; }

        /// <summary>
        /// Gets or Sets Verblijfplaats
        /// </summary>
        [DataMember(Name = "verblijfplaats", EmitDefaultValue = false)]
        public GbaVerblijfplaats? Verblijfplaats { get; set; }

        /// <summary>
        /// Gets or Sets Immigratie
        /// </summary>
        [DataMember(Name = "immigratie", EmitDefaultValue = false)]
        public GbaImmigratie? Immigratie { get; set; }

        /// <summary>
        /// Gets or Sets GemeenteVanInschrijving
        /// </summary>
        [DataMember(Name = "gemeenteVanInschrijving", EmitDefaultValue = false)]
        public Waardetabel? GemeenteVanInschrijving { get; set; }

        /// <summary>
        /// Gets or Sets DatumInschrijvingInGemeente
        /// </summary>
        [RegularExpression("^[0-9]{8}$")]
        [DataMember(Name = "datumInschrijvingInGemeente", EmitDefaultValue = false)]
        public string? DatumInschrijvingInGemeente { get; set; }

        /// <summary>
        /// Geeft aan dat de persoon onder curatele is gesteld.
        /// </summary>
        /// <value>Geeft aan dat de persoon onder curatele is gesteld. </value>
        [DataMember(Name = "indicatieCurateleRegister", EmitDefaultValue = false)]
        public bool? IndicatieCurateleRegister { get; set; }

        /// <summary>
        /// Gets or Sets IndicatieGezagMinderjarige
        /// </summary>
        [DataMember(Name = "indicatieGezagMinderjarige", EmitDefaultValue = false)]
        public Waardetabel? IndicatieGezagMinderjarige { get; set; }

        /// <summary>
        /// Gets or Sets Verblijfstitel
        /// </summary>
        [DataMember(Name = "verblijfstitel", EmitDefaultValue = false)]
        public GbaVerblijfstitel? Verblijfstitel { get; set; }

        /// <summary>
        /// Gets or Sets Kinderen
        /// </summary>
        [DataMember(Name = "kinderen", EmitDefaultValue = false)]
        public List<GbaKind>? Kinderen { get; set; }

        /// <summary>
        /// Gets or Sets Ouders
        /// </summary>
        [DataMember(Name = "ouders", EmitDefaultValue = false)]
        public List<GbaOuder>? Ouders { get; set; }

        /// <summary>
        /// Gets or Sets Partners
        /// </summary>
        [DataMember(Name = "partners", EmitDefaultValue = false)]
        public List<GbaPartner>? Partners { get; set; }

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

		/// <summary>
		/// Gets or Sets Verificatie
		/// </summary>
		[DataMember(Name = "gezag", EmitDefaultValue = false)]
		public List<AbstractGezagsrelatie>? Gezag { get; set; }

		// Used for gezag. DO NOT SERIALIZE AS PART OF THE API RESPONSE.
		[XmlIgnore, JsonIgnore]
		public List<GbaPartner>? HistorischePartners { get; set; }
	}
}
