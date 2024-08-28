using FluentValidation;
using Rvig.HaalCentraalApi.Personen.RequestModels.BRP;

namespace Rvig.HaalCentraalApi.Personen.Validation.RequestModelValidators;

public class ZoekMetBagIdentificatieValidatorBase<T> : HaalCentraalPersonenBaseValidator<T> where T : PersonenQuery
{
    protected const string _bagIdentificatiePattern = "^[0-9]{16}$";
}
