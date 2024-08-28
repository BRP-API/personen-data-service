using FluentValidation;
using Rvig.HaalCentraalApi.Personen.RequestModels.BRP;

namespace Rvig.HaalCentraalApi.Personen.Validation.RequestModelValidators;

public class ZoekMetAdresseerbaarObjectIdentificatieValidator : ZoekMetBagIdentificatieValidatorBase<ZoekMetAdresseerbaarObjectIdentificatie>
{
    public ZoekMetAdresseerbaarObjectIdentificatieValidator()
    {
        RuleFor(x => x.adresseerbaarObjectIdentificatie)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(_requiredErrorMessage)
            .Matches(_bagIdentificatiePattern).WithMessage(GetPatternErrorMessage(_bagIdentificatiePattern));
	}
}
