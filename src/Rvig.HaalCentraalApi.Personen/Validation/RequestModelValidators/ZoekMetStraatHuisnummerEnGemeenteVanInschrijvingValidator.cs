using FluentValidation;
using Rvig.HaalCentraalApi.Personen.RequestModels.BRP;
using System.Text.RegularExpressions;

namespace Rvig.HaalCentraalApi.Personen.Validation.RequestModelValidators;

public class ZoekMetStraatHuisnummerEnGemeenteVanInschrijvingValidator : HuisnummerValidatorBase<ZoekMetStraatHuisnummerEnGemeenteVanInschrijving>
{
	/// <summary>
	/// This pattern is also partially used in PersonenQueryHelper.cs HasDiacritics. If this pattern is changed regarding diacritics
	/// then change it also in the HasDiacritics method.
	/// </summary>
	protected const string _straatnaamPattern = @"^[a-zA-Z0-9À-ž \-\'\.]{1,80}$|^[a-zA-Z0-9À-ž \-\'\.]{7,79}\*{1}$|^\*{1}[a-zA-Z0-9À-ž \-\'\.]{7,79}$";

	public ZoekMetStraatHuisnummerEnGemeenteVanInschrijvingValidator()
	{
		RuleFor(x => x.huisletter)
			.Matches(_huisletterPattern).WithMessage(GetPatternErrorMessage(_huisletterPattern))
				.When(x => !string.IsNullOrWhiteSpace(x.huisletter));

		RuleFor(x => x.huisnummer)
			.Cascade(CascadeMode.Stop)
			.NotEmpty().WithMessage(_requiredErrorMessage)
			.GreaterThanOrEqualTo(1).WithMessage(_rangeMinimumErrorMessage)
			.LessThanOrEqualTo(99999).WithMessage(_rangeMaximumErrorMessage);

		RuleFor(x => x.huisnummertoevoeging)
			.Matches(_huisnummertoevoegingPattern).WithMessage(GetPatternErrorMessage(_huisnummertoevoegingPattern))
				.When(x => !string.IsNullOrWhiteSpace(x.huisnummertoevoeging));

		RuleFor(x => x.straat)
			.Cascade(CascadeMode.Stop)
			.NotEmpty().WithMessage(_requiredErrorMessage)
			.Matches(_straatnaamPattern).WithMessage(GetPatternErrorMessage(_straatnaamPattern));

		RuleFor(x => x.gemeenteVanInschrijving)
			.NotEmpty().WithMessage(_requiredErrorMessage);
	}
}
