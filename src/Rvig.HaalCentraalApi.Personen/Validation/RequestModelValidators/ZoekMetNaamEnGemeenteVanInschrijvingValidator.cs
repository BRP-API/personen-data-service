using FluentValidation;
using Rvig.HaalCentraalApi.Personen.RequestModels.BRP;

namespace Rvig.HaalCentraalApi.Personen.Validation.RequestModelValidators;

public class ZoekMetNaamEnGemeenteVanInschrijvingValidator : ZoekNaamValidatorBase<ZoekMetNaamEnGemeenteVanInschrijving>
{
	/// <summary>
	/// This pattern is also partially used in PersonenQueryHelper.cs HasDiacritics. If this pattern is changed regarding diacritics
	/// then change it also in the HasDiacritics method.
	/// </summary>
	private const string _voornamenPattern = @"^[a-zA-Z0-9À-ž \.\-\']{1,200}$|^[a-zA-Z0-9À-ž \.\-\']{3,199}\*{1}$";

	public ZoekMetNaamEnGemeenteVanInschrijvingValidator()
	{
		RuleFor(x => x.geslacht)
			.Matches(_geslachtPattern).WithMessage(GetPatternErrorMessage(_geslachtPattern))
				.When(x => !string.IsNullOrWhiteSpace(x.geslacht));

		RuleFor(x => x.geslachtsnaam)
			.Cascade(CascadeMode.Stop)
			.NotEmpty().WithMessage(_requiredErrorMessage)
			.Matches(_geslachtsnaamPattern).WithMessage(GetPatternErrorMessage(_geslachtsnaamPattern));

		RuleFor(x => x.voorvoegsel)
			.Matches(_voorvoegselPattern).WithMessage(GetPatternErrorMessage(_voorvoegselPattern))
				.When(x => !string.IsNullOrWhiteSpace(x.voorvoegsel));

		RuleFor(x => x.voornamen)
			.Cascade(CascadeMode.Stop)
			.NotEmpty().WithMessage(_requiredErrorMessage)
			.Matches(_voornamenPattern).WithMessage(GetPatternErrorMessage(_voornamenPattern));

		RuleFor(x => x.gemeenteVanInschrijving)
			.NotEmpty().WithMessage(_requiredErrorMessage);
	}
}
