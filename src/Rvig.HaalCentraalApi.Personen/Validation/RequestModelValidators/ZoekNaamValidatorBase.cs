using Rvig.HaalCentraalApi.Personen.RequestModels.BRP;

namespace Rvig.HaalCentraalApi.Personen.Validation.RequestModelValidators;

public abstract class ZoekNaamValidatorBase<T> : HaalCentraalPersonenBaseValidator<T> where T : PersonenQuery
{
    protected const string _geslachtPattern = "^([Mm]|[Vv]|[Oo])$";

	/// <summary>
	/// This pattern is also partially used in PersonenQueryHelper.cs HasDiacritics. If this pattern is changed regarding diacritics
	/// then change it also in the HasDiacritics method.
	/// </summary>
	protected const string _geslachtsnaamPattern = @"^[a-zA-Z0-9À-ž \.\-\']{1,200}$|^[a-zA-Z0-9À-ž \.\-\']{3,199}\*{1}$";

    protected const string _voorvoegselPattern = @"^[a-zA-Z \']{1,10}$";
}
