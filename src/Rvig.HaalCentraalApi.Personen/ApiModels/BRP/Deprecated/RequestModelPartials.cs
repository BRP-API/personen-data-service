using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated;

public partial class ZoekMetAdresseerbaarObjectIdentificatie
{
    /// <summary>
    /// Het maximaal aantal resultaten in de response. Wanneer het aantal objecten boven deze waarde komt wordt een foutmelding gegeven.
    /// TODO: Dit moet configurabel worden gemaakt met issue: https://github.com/BRP-API/personen-data-service/issues/42
    /// </summary>
    /// <value>De waarde van het maximaal aantal resultaten</value>
    public int maxItems { get; } = 30;
}

public partial class ZoekMetGeslachtsnaamEnGeboortedatum
{
    /// <summary>
    /// Het maximaal aantal resultaten in de response. Wanneer het aantal objecten boven deze waarde komt wordt een foutmelding gegeven.
    /// TODO: Dit moet configurabel worden gemaakt met issue: https://github.com/BRP-API/personen-data-service/issues/42
    /// </summary>
    /// <value>De waarde van het maximaal aantal resultaten</value>
    public int maxItems { get; } = 10;
}

public partial class ZoekMetNaamEnGemeenteVanInschrijving
{
    /// <summary>
    /// Het maximaal aantal resultaten in de response. Wanneer het aantal objecten boven deze waarde komt wordt een foutmelding gegeven.
    /// TODO: Dit moet configurabel worden gemaakt met issue: https://github.com/BRP-API/personen-data-service/issues/42
    /// </summary>
    /// <value>De waarde van het maximaal aantal resultaten</value>
    public int maxItems { get; } = 10;
}

public partial class ZoekMetNummeraanduidingIdentificatie
{
    /// <summary>
    /// Het maximaal aantal resultaten in de response. Wanneer het aantal objecten boven deze waarde komt wordt een foutmelding gegeven.
    /// TODO: Dit moet configurabel worden gemaakt met issue: https://github.com/BRP-API/personen-data-service/issues/42
    /// </summary>
    /// <value>De waarde van het maximaal aantal resultaten</value>
    public int maxItems { get; } = 30;
}

public partial class ZoekMetPostcodeEnHuisnummer
{
    /// <summary>
    /// Het maximaal aantal resultaten in de response. Wanneer het aantal objecten boven deze waarde komt wordt een foutmelding gegeven.
    /// TODO: Dit moet configurabel worden gemaakt met issue: https://github.com/BRP-API/personen-data-service/issues/42
    /// </summary>
    /// <value>De waarde van het maximaal aantal resultaten</value>
    public int maxItems { get; } = 30;
}

public partial class ZoekMetStraatHuisnummerEnGemeenteVanInschrijving
{
    /// <summary>
    /// Het maximaal aantal resultaten in de response. Wanneer het aantal objecten boven deze waarde komt wordt een foutmelding gegeven.
    /// TODO: Dit moet configurabel worden gemaakt met issue: https://github.com/BRP-API/personen-data-service/issues/42
    /// </summary>
    /// <value>De waarde van het maximaal aantal resultaten</value>
    public int maxItems { get; } = 30;
}
