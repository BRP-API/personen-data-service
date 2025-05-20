namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP
{
    public interface IGezagResponseWithPersonen
    {
        IEnumerable<object> Personen { get; }
    }
}
