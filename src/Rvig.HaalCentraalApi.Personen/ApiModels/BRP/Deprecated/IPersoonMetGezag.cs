namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated;

public interface IPersoonMetGezag
{
    string? Burgerservicenummer { get; }
    List<AbstractGezagsrelatie>? Gezag { get; set; }
}
