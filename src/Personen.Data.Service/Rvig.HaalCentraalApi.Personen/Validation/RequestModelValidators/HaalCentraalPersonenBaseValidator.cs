using FluentValidation;
using Rvig.HaalCentraalApi.Personen.RequestModels.BRP;
using Rvig.HaalCentraalApi.Shared.Validation.RequestModelValidators;

namespace Rvig.HaalCentraalApi.Personen.Validation.RequestModelValidators;

public class HaalCentraalPersonenBaseValidator<T> : HaalCentraalBaseValidator<T> where T : PersonenQuery
{
	public HaalCentraalPersonenBaseValidator()
	{
		RuleFor(x => x.type)
			.Cascade(CascadeMode.Stop)
			.NotEmpty().WithMessage(_requiredErrorMessage);

		RuleFor(x => x.fields)
			.Cascade(CascadeMode.Stop)
			.NotNull().WithMessage(_requiredErrorMessage)
			.Must(x => x?.Count > 0).WithMessage(string.Format(_minItemsErrorMessage, 1))
			.Must(x => x?.Count <= 130).WithMessage(string.Format(_maxItemsErrorMessage, 130));

		RuleForEach(x => x.fields)
			.Matches(_fieldsPattern).WithMessage(GetPatternErrorMessage(_fieldsPattern));

		RuleFor(x => x.gemeenteVanInschrijving)
			.Matches(_gemeenteVanInschrijvingPattern).WithMessage(GetPatternErrorMessage(_gemeenteVanInschrijvingPattern))
			.When(x => !string.IsNullOrWhiteSpace(x.gemeenteVanInschrijving));
	}
}
