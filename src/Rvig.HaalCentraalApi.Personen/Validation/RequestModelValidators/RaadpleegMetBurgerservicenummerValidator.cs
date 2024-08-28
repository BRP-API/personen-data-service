using FluentValidation;
using Rvig.HaalCentraalApi.Personen.RequestModels.BRP;

namespace Rvig.HaalCentraalApi.Personen.Validation.RequestModelValidators;

public class RaadpleegMetBurgerservicenummerValidator : HaalCentraalPersonenBaseValidator<RaadpleegMetBurgerservicenummer>
{
	const string _bsnPattern = "^[0-9]{9}$";

	public RaadpleegMetBurgerservicenummerValidator()
	{
		RuleFor(x => x.burgerservicenummer)
			.Cascade(CascadeMode.Stop)
			.NotNull().WithMessage(_requiredErrorMessage)
			.Must(x => x?.Count > 0).WithMessage(string.Format(_minItemsErrorMessage, 1))
			.Must(x => x?.Count <= 20).WithMessage(string.Format(_maxItemsErrorMessage, 20));

		RuleForEach(x => x.burgerservicenummer)
			.Matches(_bsnPattern).WithMessage(GetPatternErrorMessage(_bsnPattern));
	}
}
