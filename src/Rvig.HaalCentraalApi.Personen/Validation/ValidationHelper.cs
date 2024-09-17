using Rvig.HaalCentraalApi.Shared.ApiModels.Universal;
using Rvig.HaalCentraalApi.Shared.Exceptions;
using Rvig.HaalCentraalApi.Shared.Interfaces;
using Rvig.HaalCentraalApi.Personen.RequestModels.BRP;
using Rvig.HaalCentraalApi.Shared.Util;
using System.Text.RegularExpressions;
using Rvig.HaalCentraalApi.Shared.Validation;

namespace Rvig.HaalCentraalApi.Personen.Validation;
public static class ValidationHelper
{
    public static async Task ValidateGemeenteInschrijving(string? gemeenteVanInschrijving, IDomeinTabellenRepo domeinTabellenRepo)
    {
        if (string.IsNullOrEmpty(gemeenteVanInschrijving))
            return;

        if (gemeenteVanInschrijving.Length != 4 || !long.TryParse(gemeenteVanInschrijving, out long longValue) || longValue == 0 || string.IsNullOrEmpty(await domeinTabellenRepo.GetGemeenteNaam(longValue)))
            throw new InvalidParamsException(
                new List<InvalidParams>
                {
                    new InvalidParams {
                        Code = "table",
                        Name = "gemeenteVanInschrijving",
                        Reason = "Waarde komt niet voor in de tabel."
                    }
                }
            );
    }

    public static async Task ValidateVoorvoegsel(string? voorvoegsel, IDomeinTabellenRepo domeinTabellenRepo)
    {
        if (string.IsNullOrEmpty(voorvoegsel))
            return;

        if (!await domeinTabellenRepo.VoorvoegselExist(voorvoegsel))
            throw new InvalidParamsException(
                new List<InvalidParams>
                {
                    new InvalidParams {
                        Code = "table",
                        Name = "voorvoegsel",
                        Reason = "Waarde komt niet voor in de tabel."
                    }
                }
            );
    }

    public static void ValidateBurgerservicenummers(IEnumerable<string>? burgerservicenummers)
	{
		var burgerservicenummersFiltered = burgerservicenummers?.Distinct().ToList();

		if (burgerservicenummersFiltered?.Any() == true)
		{
			var invalidParams = new List<InvalidParams>();
			var searchModelParam = "burgerservicenummer";

			burgerservicenummersFiltered.ForEach(x =>
			{
				if (x.Length < 9)
					invalidParams.Add(CreateInvalidParam(ValidationErrorMessages.MinLength.Replace(@"\d*", "9"), searchModelParam));
				if (x.Length > 9)
					invalidParams.Add(CreateInvalidParam(ValidationErrorMessages.MaxLength.Replace(@"\d*", "9"), searchModelParam));
				if (!Regex.IsMatch(x, "^[0-9]*$"))
					invalidParams.Add(CreateInvalidParam(ValidationErrorMessages.PatternBsn, searchModelParam));
			});

			if (invalidParams.Any())
				throw new InvalidParamsException(invalidParams);
		}
		else
		{
			throw new InvalidParamCombinationException("Combinatie van gevulde velden was niet correct. De correcte veld combinatie is burgerservicenummer.");
		}
	}

    public static void ValidateBurgerservicenummersCommaSeparated(string? burgerservicenummer)
    {
        if (string.IsNullOrEmpty(burgerservicenummer))
        {
            return;
        }

        var burgerservicenummers = burgerservicenummer.Split(',').Distinct().ToList();
        ValidateBurgerservicenummers(burgerservicenummers);
    }

    public static void ValidateWildcards(ZoekMetGeslachtsnaamEnGeboortedatum model)
    {
        IsWildcardCorrectlyUsed(model.geslachtsnaam, nameof(model.geslachtsnaam));
        IsWildcardCorrectlyUsed(model.voornamen, nameof(model.voornamen));
    }

    public static void ValidateWildcards(ZoekMetNaamEnGemeenteVanInschrijving model)
    {
        IsWildcardCorrectlyUsed(model.geslachtsnaam, nameof(model.geslachtsnaam));
        IsWildcardCorrectlyUsed(model.voornamen, nameof(model.voornamen));
    }

    public static void ValidateWildcards(ZoekMetStraatHuisnummerEnGemeenteVanInschrijving model)
    {
        IsWildcardCorrectlyUsed(model.straat, nameof(model.straat));
	}

	public static void IsWildcardCorrectlyUsed(string? parameter, string name)
    {
        if (string.IsNullOrEmpty(parameter))
        {
            return;
        }

        if (parameter.Contains("*"))
        {
            var result = parameter;

            // * at start of string is allowed
            if (result.Substring(0, 1).Equals("*"))
                result = result.Remove(0, 1);

            // * at end of string is allowed
            if (result.Length > 0 && result.Substring(result.Length - 1, 1).Equals("*"))
                result = result.Remove(result.Length - 1, 1);

            // * in the middle of the parameter is not allowed and * should be accompanied by at least 2 other characters
            if (result.Contains("*") || result.Length < 2)
                ThrowWildCardException(name, "*");
        }

        if (parameter.Contains("?"))
        {
            var result = parameter;

            // multiple ? at start of string is allowed
            while (result.Length > 0 && result.Substring(0, 1).Equals("?"))
                result = result.Remove(0, 1);

            // multiple ? at end of string is allowed
            while (result.Length > 0 && result.Substring(result.Length - 1, 1).Equals("?"))
                result = result.Remove(result.Length - 1, 1);

            // ? in the middle of the parameter is not allowed and ? should be accompanied by at least 2 other characters
            if (result.Contains("?") || result.Length < 2)
                ThrowWildCardException(name, "?");
        }
    }

    private static InvalidParams CreateInvalidParam(string errorMessage, string parameterName)
    {
        return new InvalidParams { Code = ValidationErrorMessages.GetInvalidParamCode(errorMessage), Name = parameterName, Reason = errorMessage };
    }

    private static void ThrowWildCardException(string paramName, string wildcard)
    {
        throw new InvalidParamsException(
            new List<InvalidParams>
            {
                    new InvalidParams {
                        Code = "wildcard",
                        Name = paramName,
                        Reason = $"Incorrect gebruik van wildcard karakter {wildcard}."
                    }
            }
        );
    }
}