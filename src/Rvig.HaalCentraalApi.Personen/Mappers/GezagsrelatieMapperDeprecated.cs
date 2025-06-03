using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Personen.ApiModels.Gezag;
using Rvig.HaalCentraalApi.Shared.ApiModels.PersonenHistorieBase;
using Rvig.HaalCentraalApi.Shared.ApiModels.Universal;
using Deprecated = Rvig.HaalCentraalApi.Personen.Generated.Deprecated;

namespace Rvig.HaalCentraalApi.Personen.Mappers
{
    public static class GezagsrelatieMapperDeprecated
    {
        public static IEnumerable<Deprecated.AbstractGezagsrelatie> Map(GezagResponse gezagResponse, List<GbaPersoon> gezagPersonen)
        {
            var result = new List<Deprecated.AbstractGezagsrelatie>();

            if (gezagResponse == null) return result;

            foreach (var persoon in gezagResponse.Personen)
            {
                foreach (var gezagsrelatie in persoon.Gezag)
                {
                    MapEenhoofdigOuderlijkGezag(gezagPersonen, result, gezagsrelatie);

                    MapTweehoofdigOuderlijkGezag(gezagPersonen, result, gezagsrelatie);

                    MapGezamenlijkGezag(gezagPersonen, result, gezagsrelatie);

                    MapVoogdij(gezagPersonen, result, gezagsrelatie);

                    MapTijdelijkGeenGezag(gezagPersonen, result, gezagsrelatie);

                    MapGezagNietTeBepalen(gezagPersonen, result, gezagsrelatie);
                }
            }

            return result;
        }

        private static void MapGezagNietTeBepalen(List<GbaPersoon> gezagPersonen, List<Deprecated.AbstractGezagsrelatie> result, ApiModels.Gezag.AbstractGezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is ApiModels.Gezag.GezagNietTeBepalen gezagNietTeBepalen)
            {
                var minderjarige = gezagNietTeBepalen.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, gezagNietTeBepalen.Minderjarige.Burgerservicenummer) : new Deprecated.Minderjarige();

                result.Add(new Deprecated.GezagNietTeBepalen
                {
                    Minderjarige = minderjarige,
                    Toelichting = gezagNietTeBepalen.Toelichting,
                    InOnderzoek = gezagNietTeBepalen.InOnderzoek
                });
            }
        }

        private static void MapTijdelijkGeenGezag(List<GbaPersoon> gezagPersonen, List<Deprecated.AbstractGezagsrelatie> result, ApiModels.Gezag.AbstractGezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is ApiModels.Gezag.TijdelijkGeenGezag tijdelijkGeenGezag)
            {
                var minderjarige = tijdelijkGeenGezag.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, tijdelijkGeenGezag.Minderjarige.Burgerservicenummer) : new Deprecated.Minderjarige();

                result.Add(new Deprecated.TijdelijkGeenGezag
                {
                    Minderjarige = minderjarige,
                    Toelichting = tijdelijkGeenGezag.Toelichting,
                    InOnderzoek = tijdelijkGeenGezag.InOnderzoek
                });
            }
        }

        private static void MapVoogdij(List<GbaPersoon> gezagPersonen, List<Deprecated.AbstractGezagsrelatie> result, ApiModels.Gezag.AbstractGezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is ApiModels.Gezag.Voogdij voogdij)
            {
                var derden = MapPersonenToBekendeDerden(gezagPersonen, voogdij.Derden);

                var minderjarige = voogdij.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, voogdij.Minderjarige.Burgerservicenummer) : new Deprecated.Minderjarige();

                result.Add(new Deprecated.Voogdij
                {
                    Derden = derden,
                    Minderjarige = minderjarige,
                    InOnderzoek = voogdij.InOnderzoek
                });
            }
        }

        private static void MapGezamenlijkGezag(List<GbaPersoon> gezagPersonen, List<Deprecated.AbstractGezagsrelatie> result, ApiModels.Gezag.AbstractGezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is ApiModels.Gezag.GezamenlijkGezag gezamenlijkGezag)
            {
                var ouder = gezamenlijkGezag.Ouder != null ? MapPersoonToGezagOuder(gezagPersonen, gezamenlijkGezag.Ouder.Burgerservicenummer) : new Deprecated.GezagOuder();
                var derde = MapPersoonToDerde(gezagPersonen, gezamenlijkGezag.Derde);
                var minderjarige = gezamenlijkGezag.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, gezamenlijkGezag.Minderjarige.Burgerservicenummer) : new Deprecated.Minderjarige();

                result.Add(new Deprecated.GezamenlijkGezag
                {
                    Derde = derde,
                    Minderjarige = minderjarige,
                    Ouder = ouder,
                    InOnderzoek = gezamenlijkGezag.InOnderzoek
                });
            }
        }

        private static void MapTweehoofdigOuderlijkGezag(List<GbaPersoon> gezagPersonen, List<Deprecated.AbstractGezagsrelatie> result, ApiModels.Gezag.AbstractGezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is ApiModels.Gezag.TweehoofdigOuderlijkGezag tweehoofdigOuderlijkGezag)
            {
                var ouders = tweehoofdigOuderlijkGezag.Ouders?.Select(o => MapPersoonToGezagOuder(gezagPersonen, o.Burgerservicenummer)).ToList();

                var minderjarige = tweehoofdigOuderlijkGezag.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, tweehoofdigOuderlijkGezag.Minderjarige.Burgerservicenummer) : new Deprecated.Minderjarige();

                result.Add(new Deprecated.TweehoofdigOuderlijkGezag
                {
                    Minderjarige = minderjarige,
                    Ouders = ouders,
                    InOnderzoek = tweehoofdigOuderlijkGezag.InOnderzoek
                });
            }
        }

        private static void MapEenhoofdigOuderlijkGezag(List<GbaPersoon> gezagPersonen, List<Deprecated.AbstractGezagsrelatie> result, ApiModels.Gezag.AbstractGezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is ApiModels.Gezag.EenhoofdigOuderlijkGezag eenhoofdigOuderlijkGezag)
            {
                var ouder = eenhoofdigOuderlijkGezag.Ouder != null ? MapPersoonToGezagOuder(gezagPersonen, eenhoofdigOuderlijkGezag.Ouder.Burgerservicenummer) : new Deprecated.GezagOuder();
                var minderjarige = eenhoofdigOuderlijkGezag.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, eenhoofdigOuderlijkGezag.Minderjarige.Burgerservicenummer) : new Deprecated.Minderjarige();

                result.Add(new Deprecated.EenhoofdigOuderlijkGezag
                {
                    Minderjarige = minderjarige,
                    Ouder = ouder,
                    InOnderzoek = eenhoofdigOuderlijkGezag.InOnderzoek
                });
            }
        }

        private static Deprecated.GezagOuder MapPersoonToGezagOuder(List<GbaPersoon> personen, string bsn)
        {
            var persoon = personen.FirstOrDefault(p => p.Burgerservicenummer == bsn);

            if (persoon == null) return new Deprecated.GezagOuder()
            {
                Burgerservicenummer = bsn
            };

            return new Deprecated.GezagOuder
            {
                Burgerservicenummer = persoon.Burgerservicenummer,
                Geslacht = MapGeslacht(persoon.Geslacht),
                Naam = MapNaam(persoon)
            };
        }

        private static Deprecated.Minderjarige MapPersoonToMinderjarige(List<GbaPersoon> personen, string bsn)
        {
            var persoon = personen.FirstOrDefault(p => p.Burgerservicenummer == bsn);

            if (persoon == null) return new Deprecated.Minderjarige()
            {
                Burgerservicenummer = bsn
            };

            return new Deprecated.Minderjarige
            {
                Burgerservicenummer = persoon.Burgerservicenummer,
                Geboorte = MapGeboorteToGeboorteBasis(persoon.Geboorte),
                Naam = MapNaam(persoon),
                Geslacht = MapGeslacht(persoon.Geslacht)
            };
        }

        private static Generated.Common.Geslachtsaanduiding? MapGeslacht(Waardetabel? geslacht)
        {
            return geslacht != null ? new Generated.Common.Geslachtsaanduiding
            {
                Code = geslacht.Code,
                Omschrijving = geslacht.Omschrijving
            } : null;
        }

        private static Generated.Common.GeboorteBasis? MapGeboorteToGeboorteBasis(GbaGeboorte? geboorte)
        {
            return geboorte != null ? new Generated.Common.GeboorteBasis
            {
                Datum = geboorte.Datum
            } : null;
        }

        private static List<Deprecated.BekendeDerde>? MapPersonenToBekendeDerden(List<GbaPersoon> personen, IEnumerable<ApiModels.Gezag.Derde>? derden)
        {
            if (derden == null) return null;

            var retval = new List<Deprecated.BekendeDerde>();

            foreach (var gezagDerde in derden)
            {
                var brpDerde = MapPersoonToDerde(personen, gezagDerde);
                if(brpDerde is Deprecated.BekendeDerde bekendeDerde)
                {
                    retval.Add(bekendeDerde);
                }
            }

            return retval;
        }

        private static Deprecated.Derde? MapPersoonToDerde(List<GbaPersoon> personen, ApiModels.Gezag.Derde? derde)
        {
            if (derde == null) return null;
            if (derde is ApiModels.Gezag.OnbekendeDerde) return new Deprecated.Derde();

            var bsn = ((ApiModels.Gezag.BekendeDerde)derde).Burgerservicenummer;

            var persoon = personen.FirstOrDefault(p => p.Burgerservicenummer == bsn);

            if (persoon == null) return new Deprecated.BekendeDerde()
            {
                Burgerservicenummer = bsn,
            };

            return new Deprecated.BekendeDerde
            {
                Burgerservicenummer = persoon.Burgerservicenummer,
                Geslacht = new()
                {
                    Code = persoon.Geslacht?.Code,
                    Omschrijving = persoon.Geslacht?.Omschrijving
                },
                Naam = MapNaam(persoon)
            };
        }

        private static Generated.Common.NaamBasis? MapNaam(GbaPersoon persoon)
        {
            return persoon.Naam != null ? new Generated.Common.NaamBasis
            {
                Voornamen = persoon.Naam?.Voornamen,
                AdellijkeTitelPredicaat = new()
                {
                    Code = persoon.Naam?.AdellijkeTitelPredicaat?.Code,
                    Omschrijving = persoon.Naam?.AdellijkeTitelPredicaat?.Omschrijving
                },
                Voorvoegsel = persoon.Naam?.Voorvoegsel,
                Geslachtsnaam = persoon.Naam?.Geslachtsnaam
            } : null;
        }

    }
}
