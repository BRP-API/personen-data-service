using Rvig.HaalCentraalApi.Personen.RequestModels.BRP;
using Rvig.HaalCentraalApi.Shared.Util;

namespace Rvig.HaalCentraalApi.Personen.Util
{
	public class PersonenQueryJsonInheritanceConverter : QueryBaseJsonInheritanceConverter
	{
		public PersonenQueryJsonInheritanceConverter()
		{
		}

		public PersonenQueryJsonInheritanceConverter(string discriminatorName) : base(discriminatorName)
		{
		}

		public PersonenQueryJsonInheritanceConverter(Type baseType) : base(baseType)
		{
		}

		public PersonenQueryJsonInheritanceConverter(string discriminatorName, bool readTypeProperty) : base(discriminatorName, readTypeProperty)
		{
		}

		public PersonenQueryJsonInheritanceConverter(Type baseType, string discriminatorName) : base(baseType, discriminatorName)
		{
		}

		public PersonenQueryJsonInheritanceConverter(Type baseType, string discriminatorName, bool readTypeProperty) : base(baseType, discriminatorName, readTypeProperty)
		{
		}

		protected override List<string> _subTypes => new()
		{
			nameof(ZoekMetGeslachtsnaamEnGeboortedatum),
			nameof(ZoekMetNaamEnGemeenteVanInschrijving),
			nameof(RaadpleegMetBurgerservicenummer),
			nameof(ZoekMetPostcodeEnHuisnummer),
			nameof(ZoekMetStraatHuisnummerEnGemeenteVanInschrijving),
			nameof(ZoekMetNummeraanduidingIdentificatie),
			nameof(ZoekMetAdresseerbaarObjectIdentificatie)
		};
		protected override string _discriminator => nameof(PersonenQuery.type);
	}
}