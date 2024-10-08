using Rvig.HaalCentraalApi.Shared.ApiModels.PersonenHistorieBase;
using Rvig.HaalCentraalApi.Shared.ApiModels.Universal;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP
{
	/// <summary>
	/// Gegevens over de ouder van de persoon. * **datumIngangFamilierechtelijkeBetrekking** - De datum waarop de familierechtelijke betrekking is ontstaan. 
	/// </summary>
	[DataContract]
    public class GbaOuder
    {
        /// <summary>
        /// Gets or Sets Burgerservicenummer
        /// </summary>
        [RegularExpression("^[0-9]{9}$")]
        [DataMember(Name = "burgerservicenummer", EmitDefaultValue = false)]
        public string? Burgerservicenummer { get; set; }

        /// <summary>
        /// Gets or Sets Geslacht
        /// </summary>
        [DataMember(Name = "geslacht", EmitDefaultValue = false)]
        public Waardetabel? Geslacht { get; set; }

        /// <summary>
        /// Gets or Sets OuderAanduiding
        /// </summary>
        [RegularExpression("^[1|2]$")]
        [DataMember(Name = "ouderAanduiding", EmitDefaultValue = false)]
        public string? OuderAanduiding { get; set; }

        /// <summary>
        /// Gets or Sets DatumIngangFamilierechtelijkeBetrekking
        /// </summary>
        [RegularExpression("^[0-9]{8}$")]
        [DataMember(Name = "datumIngangFamilierechtelijkeBetrekking", EmitDefaultValue = false)]
        public string? DatumIngangFamilierechtelijkeBetrekking { get; set; }

        /// <summary>
        /// Gets or Sets Naam
        /// </summary>
        [DataMember(Name = "naam", EmitDefaultValue = false)]
        public GbaNaamBasis? Naam { get; set; }

        /// <summary>
        /// Gets or Sets InOnderzoek
        /// </summary>
        [DataMember(Name = "inOnderzoek", EmitDefaultValue = false)]
        public GbaInOnderzoek? InOnderzoek { get; set; }

        /// <summary>
        /// Gets or Sets Geboorte
        /// </summary>
        [DataMember(Name = "geboorte", EmitDefaultValue = false)]
        public GbaGeboorte? Geboorte { get; set; }
    }
}
