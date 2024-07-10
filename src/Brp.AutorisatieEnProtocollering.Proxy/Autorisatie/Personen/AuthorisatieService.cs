using Brp.AutorisatieEnProtocollering.Proxy.Helpers;
using Brp.Shared.Infrastructure.Autorisatie;
using Brp.Shared.Infrastructure.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using static Brp.AutorisatieEnProtocollering.Proxy.Helpers.StringDictionaryHelpers;

namespace Brp.AutorisatieEnProtocollering.Proxy.Autorisatie.Personen;

public class AuthorisatieService : AbstractAutorisatieService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorisatieService(IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor)
        : base(serviceProvider)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override AuthorisationResult Authorize(int afnemerCode, int? gemeenteCode, string requestBody)
    {
        if (gemeenteCode.HasValue)
        {
            _httpContextAccessor.HttpContext?.Items.Add("Autorisatie", $"afnemer met id {afnemerCode} is gemeente met code '{gemeenteCode}'");
            return Authorized();
        }

        var autorisatie = GetActueleAutorisatieFor(afnemerCode);
        if (autorisatie != null)
        {
           _httpContextAccessor.HttpContext?.Items.Add("Autorisatie", autorisatie.ToJsonCompact());
        }

        if (GeenAutorisatieOfNietGeautoriseerdVoorAdHocGegevensverstrekking(autorisatie))
        {
            return NietGeautoriseerdVoorAdhocGegevensverstrekking(autorisatie, afnemerCode);
        }

        var input = JObject.Parse(requestBody);

        var zoekElementNrs = input.BepaalElementNrVanZoekParameters(Constanten.FieldElementNrDictionary);

        var geautoriseerdeElementNrs = autorisatie!.RubrieknummerAdHoc!.Split(' ');

        var nietGeautoriseerdQueryElementNrs = BepaalNietGeautoriseerdeElementNamen(geautoriseerdeElementNrs, zoekElementNrs);
        if (nietGeautoriseerdQueryElementNrs.Any())
        {
            return NietGeautoriseerdVoorParameters(nietGeautoriseerdQueryElementNrs);
        }

        var fieldElementNrs = BepaalElementNrVanFields(input);

        var nietGeautoriseerdFieldNames = BepaalNietGeautoriseerdeElementNamen(geautoriseerdeElementNrs, fieldElementNrs);
        if (nietGeautoriseerdFieldNames.Any())
        {
            return NietGeautoriseerdVoorFields(nietGeautoriseerdFieldNames, afnemerCode);
        }

        return Authorized();
    }

    private static string BepaalKeyVoor(string gevraagdField, string zoekType)
    {
        return zoekType != "RaadpleegMetBurgerservicenummer"
                ? $"{gevraagdField}-beperkt"
                : gevraagdField;
    }

    private static IEnumerable<(string Name, string[] Value)> BepaalElementNrVanFields(JObject input)
    {
        var zoekType = input.WaardeTypeParameter();
        var gevraagdeFields = input.WaardeFieldsParameter();

        return gevraagdeFields.ToKeyStringArray(Constanten.FieldElementNrDictionary, zoekType!, BepaalKeyVoor);
    }
}
