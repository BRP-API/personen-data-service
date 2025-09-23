using Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Common;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
public partial class Gezagsrelatie
{
    public static List<Gezagsrelatie> MapFrom(ICollection<Gezag.Gezagsrelatie> gezagsrelatie)
    {
        var result = new List<Gezagsrelatie>();

        if (gezagsrelatie == null || !gezagsrelatie.Any())
        {
            return result;
        }

          foreach (var item in gezagsrelatie)
        {
            switch (item)
            {
                case Gezag.GezamenlijkOuderlijkGezag og:
                    result.Add(MapGezamenlijkOuderlijkGezag(og));
                    break;
                case Gezag.GezamenlijkGezag g:
                    result.Add(MapGezamenlijkGezag(g));
                    break;
                case Gezag.Voogdij v:
                    result.Add(MapVoogdij(v));
                    break;
                case Gezag.GezagNietTeBepalen n:
                    result.Add(MapGezagNietTeBepalen(n));
                    break;
                case Gezag.TijdelijkGeenGezag t:
                    result.Add(MapTijdelijkGeenGezag(t));
                    break;
                case Gezag.EenhoofdigOuderlijkGezag e:
                    result.Add(MapEenhoofdigOuderlijkGezag(e));
                    break;
                default:
                    throw new InvalidOperationException("Onbekend type gezag");
            }
        }

        return result;
    }

    private static TijdelijkGeenGezag MapTijdelijkGeenGezag(Gezag.TijdelijkGeenGezag t)
    {
        return new TijdelijkGeenGezag
        {
            Minderjarige = MapMinderjarige(t.Minderjarige),
            InOnderzoek = t.InOnderzoek,
            Toelichting = t.Toelichting
        };
    }

    private static GezagNietTeBepalen MapGezagNietTeBepalen(Gezag.GezagNietTeBepalen n)
    {
        return new GezagNietTeBepalen
        {
            Minderjarige = MapMinderjarige(n.Minderjarige),
            InOnderzoek = n.InOnderzoek,
            Toelichting = n.Toelichting
        };
    }

    private static Voogdij MapVoogdij(Gezag.Voogdij v)
    {
        return new Voogdij
        {
            Minderjarige = MapMinderjarige(v.Minderjarige),
            Derden = v.Derden?.Select(MapBekendeDerde).ToList() ?? new List<BekendeDerde>(),
            InOnderzoek = v.InOnderzoek
        };
    }

    private static GezamenlijkOuderlijkGezag MapGezamenlijkOuderlijkGezag(Gezag.GezamenlijkOuderlijkGezag og)
    {
        return new GezamenlijkOuderlijkGezag
        {
            Minderjarige = MapMinderjarige(og.Minderjarige),
            Ouders = og.Ouders?.Select(MapGezagOuder).ToList() ?? new List<GezagOuder>(),
            InOnderzoek = og.InOnderzoek
        };
    }

    private static GezamenlijkGezag MapGezamenlijkGezag(Gezag.GezamenlijkGezag g)
    {
        return new GezamenlijkGezag
        {
            Minderjarige = MapMinderjarige(g.Minderjarige),
            Ouder = MapGezagOuder(g.Ouder),
            Derde = MapDerde(g.Derde),
            InOnderzoek = g.InOnderzoek
        };
    }

    private static EenhoofdigOuderlijkGezag MapEenhoofdigOuderlijkGezag(Gezag.EenhoofdigOuderlijkGezag g)
    {
        return new EenhoofdigOuderlijkGezag
        {
            Minderjarige = MapMinderjarige(g.Minderjarige),
            Ouder = MapGezagOuder(g.Ouder),
            InOnderzoek = g.InOnderzoek
        };
    }

    private static Minderjarige MapMinderjarige(Gezag.Minderjarige m)
    {
        return new Minderjarige
        {
            Burgerservicenummer = m.Burgerservicenummer,
            Naam = new()
            {
                Voornamen = m.Naam?.Voornamen,
                Voorvoegsel = m.Naam?.Voorvoegsel,
                Geslachtsnaam = m.Naam?.Geslachtsnaam,
                AdellijkeTitelPredicaat = MapAdellijkeTitelPredicaatType(m.Naam?.AdellijkeTitelPredicaat)
            },
            Geboorte = new()
            {
                Datum = m.Geboorte?.Datum
            },
            Geslacht = MapGeslacht(m.Geslacht)
        };
    }

    private static GezagOuder MapGezagOuder(Gezag.GezagOuder o)
    {
        return new GezagOuder
        {
            Burgerservicenummer = o.Burgerservicenummer,
            Naam = new()
            {
                Voornamen = o.Naam?.Voornamen,
                Voorvoegsel = o.Naam?.Voorvoegsel,
                Geslachtsnaam = o.Naam?.Geslachtsnaam,
                AdellijkeTitelPredicaat = MapAdellijkeTitelPredicaatType(o.Naam?.AdellijkeTitelPredicaat)
            },
            Geslacht = MapGeslacht(o.Geslacht)
        };
    }

    private static AdellijkeTitelPredicaatType? MapAdellijkeTitelPredicaatType(Gezag.AdellijkeTitelPredicaatType? adellijkeTitelPredicaatType)
    {
        return adellijkeTitelPredicaatType != null ? new()
        {
            Code = adellijkeTitelPredicaatType.Code,
            Omschrijving = adellijkeTitelPredicaatType.Omschrijving
        } : null;
    }

    private static Geslachtsaanduiding? MapGeslacht(Gezag.Geslachtsaanduiding geslacht)
    {
        return geslacht != null ? new()
        {
            Code = geslacht.Code,
            Omschrijving = geslacht.Omschrijving
        } : null;
    }

    private static Derde MapDerde(Gezag.Derde derde)
    {
        return derde switch
        {
            Gezag.BekendeDerde bekendeDerde => MapBekendeDerde(bekendeDerde),
            Gezag.OnbekendeDerde => new OnbekendeDerde(),
            _ => throw new InvalidOperationException("Onbekend type derde"),
        };
    }

    private static BekendeDerde MapBekendeDerde(Gezag.BekendeDerde bekendeDerde)
    {
        return new BekendeDerde
        {
            Burgerservicenummer = bekendeDerde.Burgerservicenummer,
            Naam = new()
            {
                Voornamen = bekendeDerde.Naam?.Voornamen,
                Voorvoegsel = bekendeDerde.Naam?.Voorvoegsel,
                Geslachtsnaam = bekendeDerde.Naam?.Geslachtsnaam,
                AdellijkeTitelPredicaat = MapAdellijkeTitelPredicaatType(bekendeDerde.Naam?.AdellijkeTitelPredicaat)
            },
            Geslacht = MapGeslacht(bekendeDerde.Geslacht)
        };
    }
}
