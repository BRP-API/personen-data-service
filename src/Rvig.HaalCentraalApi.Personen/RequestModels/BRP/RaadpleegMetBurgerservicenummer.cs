using Rvig.HaalCentraalApi.Shared.Util;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Rvig.HaalCentraalApi.Personen.RequestModels.BRP
{
	[DataContract]
    public class RaadpleegMetBurgerservicenummer : PersonenQuery
    {
		/// <summary>
		/// Gets or Sets Burgerservicenummer
		/// </summary>
		[DataMember(Name = "burgerservicenummer", EmitDefaultValue = false)]
		[JsonConverter(typeof(JsonArrayConverter))]
		// Null forgiving operator required because of validation. A user may possibly send a request without this list.
		// That should not be accepted but specs require us to throw a validation exception regarding this requirement.
		public List<string> burgerservicenummer { get; set; } = null!;
	}
}
