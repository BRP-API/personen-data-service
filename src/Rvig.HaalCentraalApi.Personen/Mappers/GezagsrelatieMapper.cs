using BRP = Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated;
using Rvig.HaalCentraalApi.Personen.ApiModels.Gezag.Deprecated;

namespace Rvig.HaalCentraalApi.Personen.Mappers
{
    public static class GezagsrelatieMapper
    {
        public static IEnumerable<BRP.AbstractGezagsrelatie> Map(GezagResponse gezagResponse, List<BRP.GbaPersoon> gezagPersonen)
        {
            var result = new List<BRP.AbstractGezagsrelatie>();

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

        private static void MapGezagNietTeBepalen(List<BRP.GbaPersoon> gezagPersonen, List<BRP.AbstractGezagsrelatie> result, AbstractGezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is GezagNietTeBepalen gezagNietTeBepalen)
            {
                var minderjarige = gezagNietTeBepalen.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, gezagNietTeBepalen.Minderjarige.Burgerservicenummer) : new BRP.Minderjarige();

                result.Add(new BRP.GezagNietTeBepalen
                {
                    Minderjarige = minderjarige,
                    Toelichting = gezagNietTeBepalen.Toelichting,
                    InOnderzoek = gezagNietTeBepalen.InOnderzoek
                });
            }
        }

        private static void MapTijdelijkGeenGezag(List<BRP.GbaPersoon> gezagPersonen, List<BRP.AbstractGezagsrelatie> result, AbstractGezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is TijdelijkGeenGezag tijdelijkGeenGezag)
            {
                var minderjarige = tijdelijkGeenGezag.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, tijdelijkGeenGezag.Minderjarige.Burgerservicenummer) : new BRP.Minderjarige();

                result.Add(new BRP.TijdelijkGeenGezag
                {
                    Minderjarige = minderjarige,
                    Toelichting = tijdelijkGeenGezag.Toelichting,
                    InOnderzoek = tijdelijkGeenGezag.InOnderzoek
                });
            }
        }

        private static void MapVoogdij(List<BRP.GbaPersoon> gezagPersonen, List<BRP.AbstractGezagsrelatie> result, AbstractGezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is Voogdij voogdij)
            {
                var derden = MapPersonenToBekendeDerden(gezagPersonen, voogdij.Derden);

                var minderjarige = voogdij.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, voogdij.Minderjarige.Burgerservicenummer) : new BRP.Minderjarige();

                result.Add(new BRP.Voogdij
                {
                    Derden = derden,
                    Minderjarige = minderjarige,
                    InOnderzoek = voogdij.InOnderzoek
                });
            }
        }

        private static void MapGezamenlijkGezag(List<BRP.GbaPersoon> gezagPersonen, List<BRP.AbstractGezagsrelatie> result, AbstractGezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is GezamenlijkGezag gezamenlijkGezag)
            {
                var ouder = gezamenlijkGezag.Ouder != null ? MapPersoonToGezagOuder(gezagPersonen, gezamenlijkGezag.Ouder.Burgerservicenummer) : new BRP.GezagOuder();
                var derde = MapPersoonToDerde(gezagPersonen, gezamenlijkGezag.Derde);
                var minderjarige = gezamenlijkGezag.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, gezamenlijkGezag.Minderjarige.Burgerservicenummer) : new BRP.Minderjarige();

                result.Add(new BRP.GezamenlijkGezag
                {
                    Derde = derde,
                    Minderjarige = minderjarige,
                    Ouder = ouder,
                    InOnderzoek = gezamenlijkGezag.InOnderzoek
                });
            }
        }

        private static void MapTweehoofdigOuderlijkGezag(List<BRP.GbaPersoon> gezagPersonen, List<BRP.AbstractGezagsrelatie> result, AbstractGezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is TweehoofdigOuderlijkGezag tweehoofdigOuderlijkGezag)
            {
                var ouders = tweehoofdigOuderlijkGezag.Ouders?.Select(o => MapPersoonToGezagOuder(gezagPersonen, o.Burgerservicenummer)).ToList();

                var minderjarige = tweehoofdigOuderlijkGezag.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, tweehoofdigOuderlijkGezag.Minderjarige.Burgerservicenummer) : new BRP.Minderjarige();

                result.Add(new BRP.TweehoofdigOuderlijkGezag
                {
                    Minderjarige = minderjarige,
                    Ouders = ouders,
                    InOnderzoek = tweehoofdigOuderlijkGezag.InOnderzoek
                });
            }
        }

        private static void MapEenhoofdigOuderlijkGezag(List<BRP.GbaPersoon> gezagPersonen, List<BRP.AbstractGezagsrelatie> result, AbstractGezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is EenhoofdigOuderlijkGezag eenhoofdigOuderlijkGezag)
            {
                var ouder = eenhoofdigOuderlijkGezag.Ouder != null ? MapPersoonToGezagOuder(gezagPersonen, eenhoofdigOuderlijkGezag.Ouder.Burgerservicenummer) : new BRP.GezagOuder();
                var minderjarige = eenhoofdigOuderlijkGezag.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, eenhoofdigOuderlijkGezag.Minderjarige.Burgerservicenummer) : new BRP.Minderjarige();

                result.Add(new BRP.EenhoofdigOuderlijkGezag
                {
                    Minderjarige = minderjarige,
                    Ouder = ouder,
                    InOnderzoek = eenhoofdigOuderlijkGezag.InOnderzoek
                });
            }
        }

        private static BRP.GezagOuder MapPersoonToGezagOuder(List<BRP.GbaPersoon> personen, string bsn)
        {
            var persoon = personen.FirstOrDefault(p => p.Burgerservicenummer == bsn);

            if (persoon == null) return new BRP.GezagOuder()
            {
                Burgerservicenummer = bsn
            };

            return new BRP.GezagOuder
            {
                Burgerservicenummer = persoon.Burgerservicenummer,
                Geslacht = persoon.Geslacht,
                Naam = MapNaam(persoon)
            };
        }

        private static BRP.Minderjarige MapPersoonToMinderjarige(List<BRP.GbaPersoon> personen, string bsn)
        {
            var persoon = personen.FirstOrDefault(p => p.Burgerservicenummer == bsn);

            if (persoon == null) return new BRP.Minderjarige()
            {
                Burgerservicenummer = bsn
            };

            return new BRP.Minderjarige
            {
                Burgerservicenummer = persoon.Burgerservicenummer,
                Geboorte = MapGeboorte(persoon),
                Geslacht = persoon.Geslacht,
                Naam = MapNaam(persoon)
            };
        }

        private static List<BRP.BekendeDerde>? MapPersonenToBekendeDerden(List<BRP.GbaPersoon> personen, IEnumerable<Derde>? derden)
        {
            if (derden == null) return null;

            var retval = new List<BRP.BekendeDerde>();

            foreach (var gezagDerde in derden)
            {
                var brpDerde = MapPersoonToDerde(personen, gezagDerde);
                if(brpDerde is BRP.BekendeDerde bekendeDerde)
                {
                    retval.Add(bekendeDerde);
                }
            }

            return retval;
        }

        private static BRP.Derde? MapPersoonToDerde(List<BRP.GbaPersoon> personen, Derde? derde)
        {
            if (derde == null) return null;
            if (derde is OnbekendeDerde) return new BRP.OnbekendeDerde();

            var bsn = ((BekendeDerde)derde).Burgerservicenummer;

            var persoon = personen.FirstOrDefault(p => p.Burgerservicenummer == bsn);

            if (persoon == null) return new BRP.BekendeDerde()
            {
                Burgerservicenummer = bsn,
            };

            return new BRP.BekendeDerde
            {
                Burgerservicenummer = persoon.Burgerservicenummer,
                Geslacht = persoon.Geslacht,
                Naam = MapNaam(persoon)
            };
        }

        private static BRP.NaamBasis? MapNaam(BRP.GbaPersoon persoon)
        {
            return persoon.Naam != null ? new BRP.NaamBasis
            {
                Voornamen = persoon.Naam?.Voornamen,
                AdellijkeTitelPredicaat = persoon.Naam?.AdellijkeTitelPredicaat,
                Voorvoegsel = persoon.Naam?.Voorvoegsel,
                Geslachtsnaam = persoon.Naam?.Geslachtsnaam
            } : null;
        }

        private static BRP.GeboorteBasis? MapGeboorte(BRP.GbaPersoon persoon)
        {
            return persoon.Geboorte != null ? new BRP.GeboorteBasis()
            {
                Datum = persoon.Geboorte?.Datum
            } : null;
        }
    }
}
