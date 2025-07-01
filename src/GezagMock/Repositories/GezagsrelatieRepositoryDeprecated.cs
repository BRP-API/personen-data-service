using Brp.Gezag.Mock.Generated.Deprecated;
using Newtonsoft.Json;

namespace GezagMock.Repositories;

public class GezagsrelatieRepositoryDeprecated
{
    private readonly IWebHostEnvironment _environment;

    public GezagsrelatieRepositoryDeprecated(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<IEnumerable<Persoon>?> Zoek(string bsn)
    {
        var path = Path.Combine(_environment.ContentRootPath, "Data", "test-data.json");
        if(!File.Exists(path))
        {
            throw new FileNotFoundException($"invalid file: '{path}'");
        }

        var data = await File.ReadAllTextAsync(path);

        return JsonConvert.DeserializeObject<List<Persoon>>(data)?.Where(x => x.Burgerservicenummer == bsn);
    }
}
