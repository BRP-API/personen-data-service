using Deprecated = Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated;
using Common = Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Common;
using Rvig.HaalCentraalApi.Personen.ApiModels.Gezag.Deprecated;

namespace Rvig.HaalCentraalApi.Personen.Mappers
{
    public static class GezagsrelatieMapper
    {
        public static IEnumerable<Deprecated.AbstractGezagsrelatie> Map(GezagResponse gezagResponse, List<Deprecated.GbaPersoon> gezagPersonen)
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

        private static void MapGezagNietTeBepalen(List<Deprecated.GbaPersoon> gezagPersonen, List<Deprecated.AbstractGezagsrelatie> result, AbstractGezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is GezagNietTeBepalen gezagNietTeBepalen)
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

        private static void MapTijdelijkGeenGezag(List<Deprecated.GbaPersoon> gezagPersonen, List<Deprecated.AbstractGezagsrelatie> result, AbstractGezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is TijdelijkGeenGezag tijdelijkGeenGezag)
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

        private static void MapVoogdij(List<Deprecated.GbaPersoon> gezagPersonen, List<Deprecated.AbstractGezagsrelatie> result, AbstractGezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is Voogdij voogdij)
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

        private static void MapGezamenlijkGezag(List<Deprecated.GbaPersoon> gezagPersonen, List<Deprecated.AbstractGezagsrelatie> result, AbstractGezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is GezamenlijkGezag gezamenlijkGezag)
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

        private static void MapTweehoofdigOuderlijkGezag(List<Deprecated.GbaPersoon> gezagPersonen, List<Deprecated.AbstractGezagsrelatie> result, AbstractGezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is TweehoofdigOuderlijkGezag tweehoofdigOuderlijkGezag)
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

        private static void MapEenhoofdigOuderlijkGezag(List<Deprecated.GbaPersoon> gezagPersonen, List<Deprecated.AbstractGezagsrelatie> result, AbstractGezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is EenhoofdigOuderlijkGezag eenhoofdigOuderlijkGezag)
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

        private static Deprecated.GezagOuder MapPersoonToGezagOuder(List<Deprecated.GbaPersoon> personen, string bsn)
        {
            var persoon = personen.FirstOrDefault(p => p.Burgerservicenummer == bsn);

            if (persoon == null) return new Deprecated.GezagOuder()
            {
                Burgerservicenummer = bsn
            };

            return new Deprecated.GezagOuder
            {
                Burgerservicenummer = persoon.Burgerservicenummer,
                Geslacht = persoon.Geslacht,
                Naam = MapNaam(persoon)
            };
        }

        private static Deprecated.Minderjarige MapPersoonToMinderjarige(List<Deprecated.GbaPersoon> personen, string bsn)
        {
            var persoon = personen.FirstOrDefault(p => p.Burgerservicenummer == bsn);

            if (persoon == null) return new Deprecated.Minderjarige()
            {
                Burgerservicenummer = bsn
            };

            return new Deprecated.Minderjarige
            {
                Burgerservicenummer = persoon.Burgerservicenummer,
                Geboorte = MapGeboorte(persoon),
                Geslacht = persoon.Geslacht,
                Naam = MapNaam(persoon)
            };
        }

        private static List<Deprecated.BekendeDerde>? MapPersonenToBekendeDerden(List<Deprecated.GbaPersoon> personen, IEnumerable<Derde>? derden)
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

        private static Deprecated.Derde? MapPersoonToDerde(List<Deprecated.GbaPersoon> personen, Derde? derde)
        {
            if (derde == null) return null;
            if (derde is OnbekendeDerde) return new Deprecated.OnbekendeDerde();

            var bsn = ((BekendeDerde)derde).Burgerservicenummer;

            var persoon = personen.FirstOrDefault(p => p.Burgerservicenummer == bsn);

            if (persoon == null) return new Deprecated.BekendeDerde()
            {
                Burgerservicenummer = bsn,
            };

            return new Deprecated.BekendeDerde
            {
                Burgerservicenummer = persoon.Burgerservicenummer,
                Geslacht = persoon.Geslacht,
                Naam = MapNaam(persoon)
            };
        }

        private static Common.NaamBasis? MapNaam(Deprecated.GbaPersoon persoon)
        {
            return persoon.Naam != null ? new Common.NaamBasis
            {
                Voornamen = persoon.Naam?.Voornamen,
                AdellijkeTitelPredicaat = persoon.Naam?.AdellijkeTitelPredicaat,
                Voorvoegsel = persoon.Naam?.Voorvoegsel,
                Geslachtsnaam = persoon.Naam?.Geslachtsnaam
            } : null;
        }

        private static Common.GeboorteBasis? MapGeboorte(Deprecated.GbaPersoon persoon)
        {
            return persoon.Geboorte != null ? new Common.GeboorteBasis()
            {
                Datum = persoon.Geboorte?.Datum
            } : null;
        }
    }
}
