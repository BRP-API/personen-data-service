using Brp.AutorisatieEnProtocollering.Proxy.Helpers;
using Brp.Shared.Infrastructure.Autorisatie;

namespace Brp.AutorisatieEnProtocollering.Proxy.Autorisatie.Historie;

public class AutorisatieService : AbstractAutorisatieService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AutorisatieService(IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor)
        : base(serviceProvider)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override AuthorisationResult Authorize(int afnemerCode, int? gemeenteCode, string requestBody)
    {
        if (gemeenteCode.HasValue)
        {
            _httpContextAccessor.HttpContext?.Items.Add("Autorisatie", $"afnemer: {afnemerCode} is gemeente '{gemeenteCode}'");
            return Authorized();
        }

        var autorisatie = GetActueleAutorisatieFor(afnemerCode);
        if (autorisatie != null)
        {
            _httpContextAccessor.HttpContext?.Items.Add("Autorisatie", autorisatie);
        }

        var geautoriseerdeElementNrs = autorisatie!.RubrieknummerAdHoc!.Split(' ');

        var fieldElementNrs = new[] { "verblijfplaatshistorie" }.ToKeyStringArray(Constanten.FieldElementNrDictionary, "", BepaalKeyVoor);

        var nietGeautoriseerdFieldNames = BepaalNietGeautoriseerdeElementNamen(geautoriseerdeElementNrs, fieldElementNrs);
        if (nietGeautoriseerdFieldNames.Any())
        {
            return NietGeautoriseerdVoorEndpoint("verblijfplaatshistorie", nietGeautoriseerdFieldNames, afnemerCode);
        }

        return Authorized();
    }

    private string BepaalKeyVoor(string field, string zoekType) => field;
}
