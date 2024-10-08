using Rvig.HaalCentraalApi.Shared.ApiModels.Universal;
using System.Runtime.Serialization;

namespace Rvig.HaalCentraalApi.Shared.ApiModels.PersonenHistorieBase
{
	[DataContract]
	public class GbaNationaliteitHistorie : GbaNationaliteit
	{
		/// <summary>
		/// Gets or Sets RedenOpname
		/// </summary>
		[DataMember(Name = "redenBeeindigen", EmitDefaultValue = false)]
		public Waardetabel? RedenBeeindigen { get; set; }
	}
}
