using Rvig.HaalCentraalApi.Personen.RequestModels.BRP;

namespace Rvig.HaalCentraalApi.Personen.Validation.RequestModelValidators;

public class HuisnummerValidatorBase<T> : HaalCentraalPersonenBaseValidator<T> where T : PersonenQuery
{
	protected const string _huisletterPattern = @"^[a-zA-Z]{1}$";
	protected const string _huisnummertoevoegingPattern = @"^[a-zA-Z0-9 \-]{1,4}$";
	protected const string _rangeMinimumErrorMessage = "Waarde is lager dan minimum 1.";
	protected const string _rangeMaximumErrorMessage = "Waarde is hoger dan maximum 99999.";
}
