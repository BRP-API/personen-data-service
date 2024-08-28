using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Shared.Util;

namespace Rvig.HaalCentraalApi.Personen.Util
{
	public class AbstractGezagsrelatieJsonInheritanceConverter : QueryBaseJsonInheritanceConverter
	{
		public AbstractGezagsrelatieJsonInheritanceConverter()
		{
		}

		public AbstractGezagsrelatieJsonInheritanceConverter(string discriminatorName) : base(discriminatorName)
		{
		}

		public AbstractGezagsrelatieJsonInheritanceConverter(Type baseType) : base(baseType)
		{
		}

		public AbstractGezagsrelatieJsonInheritanceConverter(string discriminatorName, bool readTypeProperty) : base(discriminatorName, readTypeProperty)
		{
		}

		public AbstractGezagsrelatieJsonInheritanceConverter(Type baseType, string discriminatorName) : base(baseType, discriminatorName)
		{
		}

		public AbstractGezagsrelatieJsonInheritanceConverter(Type baseType, string discriminatorName, bool readTypeProperty) : base(baseType, discriminatorName, readTypeProperty)
		{
		}

		protected override List<string> _subTypes => new()
		{
			nameof(EenhoofdigOuderlijkGezag),
			nameof(GezagNietTeBepalen),
			nameof(GezamenlijkGezag),
			nameof(TijdelijkGeenGezag),
			nameof(TweehoofdigOuderlijkGezag),
			nameof(Voogdij)
		};
		protected override string _discriminator => nameof(AbstractGezagsrelatie.Type);
	}
}