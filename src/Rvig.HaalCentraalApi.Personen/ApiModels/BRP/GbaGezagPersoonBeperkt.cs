using System.Runtime.Serialization;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP
{
	[DataContract]
	public class GbaGezagPersoonBeperkt : GbaPersoonBeperkt, IPersoonMetGezag
	{
		/// <summary>
		/// Gets or Sets Gezag
		/// </summary>
		[DataMember(Name = "gezag", EmitDefaultValue = false)]
		public List<AbstractGezagsrelatie>? Gezag { get; set; }
	}
}
