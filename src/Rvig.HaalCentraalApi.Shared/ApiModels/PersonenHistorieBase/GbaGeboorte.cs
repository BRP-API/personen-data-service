using Rvig.HaalCentraalApi.Shared.ApiModels.Universal;
using System.Runtime.Serialization;

namespace Rvig.HaalCentraalApi.Shared.ApiModels.PersonenHistorieBase
{
	[DataContract]
	public class GbaGeboorte : GbaGeboorteBeperkt
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
	}
}
