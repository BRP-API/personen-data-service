using FluentValidation;
using Rvig.HaalCentraalApi.Personen.RequestModels.BRP;

namespace Rvig.HaalCentraalApi.Personen.Validation.RequestModelValidators;

public class ZoekMetNummeraanduidingIdentificatieValidator : ZoekMetBagIdentificatieValidatorBase<ZoekMetNummeraanduidingIdentificatie>
{
    public ZoekMetNummeraanduidingIdentificatieValidator()
    {
        RuleFor(x => x.nummeraanduidingIdentificatie)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(_requiredErrorMessage)
			.Matches(_bagIdentificatiePattern).WithMessage(GetPatternErrorMessage(_bagIdentificatiePattern));
	}
}
