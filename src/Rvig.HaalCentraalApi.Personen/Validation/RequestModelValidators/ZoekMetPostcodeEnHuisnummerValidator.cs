using FluentValidation;
using Rvig.HaalCentraalApi.Personen.RequestModels.BRP;

namespace Rvig.HaalCentraalApi.Personen.Validation.RequestModelValidators;

public class ZoekMetPostcodeEnHuisnummerValidator : HuisnummerValidatorBase<ZoekMetPostcodeEnHuisnummer>
{
    protected string _geslachtsnaamPattern = @"^[a-zA-Z0-9À-ž \.\-\']{1,200}$|^[a-zA-Z0-9À-ž \.\-\']{3,199}\*{1}$";
    protected const string _postcodePattern = @"^[1-9]{1}[0-9]{3}[ ]?[A-Za-z]{2}$";

	public ZoekMetPostcodeEnHuisnummerValidator()
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

		RuleFor(x => x.postcode)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(_requiredErrorMessage)
            .Matches(_postcodePattern).WithMessage(GetPatternErrorMessage(_postcodePattern));

		RuleFor(x => x.geboortedatum)
			.Matches(_datePattern).WithMessage(_dateErrorMessage);

        RuleFor(x => x.geslachtsnaam)
           .Cascade(CascadeMode.Stop)
           .Matches(_geslachtsnaamPattern).WithMessage(GetPatternErrorMessage(_geslachtsnaamPattern))
           .When(x => !string.IsNullOrWhiteSpace(x.geslachtsnaam));
    }
}
