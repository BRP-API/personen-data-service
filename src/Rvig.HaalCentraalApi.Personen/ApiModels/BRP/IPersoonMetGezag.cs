namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP;

public interface IPersoonMetGezag
{
    string? Burgerservicenummer { get; }
    List<AbstractGezagsrelatie>? Gezag { get; set; }
}
