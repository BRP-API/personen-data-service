using Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated;
using Rvig.HaalCentraalApi.Personen.ApiModels.Gezag;

namespace Rvig.HaalCentraalApi.Personen.Mappers
{
    public static class GezagsrelatieMapper
    {
        public static IEnumerable<ApiModels.BRP.Deprecated.AbstractGezagsrelatie> Map(GezagResponse gezagResponse, List<GbaPersoon> gezagPersonen)
        {
            var result = new List<ApiModels.BRP.Deprecated.AbstractGezagsrelatie>();

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

        private static void MapGezagNietTeBepalen(List<GbaPersoon> gezagPersonen, List<ApiModels.BRP.Deprecated.AbstractGezagsrelatie> result, ApiModels.Gezag.AbstractGezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is ApiModels.Gezag.GezagNietTeBepalen gezagNietTeBepalen)
            {
                var minderjarige = gezagNietTeBepalen.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, gezagNietTeBepalen.Minderjarige.Burgerservicenummer) : new ApiModels.BRP.Deprecated.Minderjarige();

                result.Add(new ApiModels.BRP.Deprecated.GezagNietTeBepalen
                {
                    Minderjarige = minderjarige,
                    Toelichting = gezagNietTeBepalen.Toelichting,
                    InOnderzoek = gezagNietTeBepalen.InOnderzoek
                });
            }
        }

        private static void MapTijdelijkGeenGezag(List<GbaPersoon> gezagPersonen, List<ApiModels.BRP.Deprecated.AbstractGezagsrelatie> result, ApiModels.Gezag.AbstractGezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is ApiModels.Gezag.TijdelijkGeenGezag tijdelijkGeenGezag)
            {
                var minderjarige = tijdelijkGeenGezag.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, tijdelijkGeenGezag.Minderjarige.Burgerservicenummer) : new ApiModels.BRP.Deprecated.Minderjarige();

                result.Add(new ApiModels.BRP.Deprecated.TijdelijkGeenGezag
                {
                    Minderjarige = minderjarige,
                    Toelichting = tijdelijkGeenGezag.Toelichting,
                    InOnderzoek = tijdelijkGeenGezag.InOnderzoek
                });
            }
        }

        private static void MapVoogdij(List<GbaPersoon> gezagPersonen, List<ApiModels.BRP.Deprecated.AbstractGezagsrelatie> result, ApiModels.Gezag.AbstractGezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is ApiModels.Gezag.Voogdij voogdij)
            {
                var derden = MapPersonenToBekendeDerden(gezagPersonen, voogdij.Derden);

                var minderjarige = voogdij.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, voogdij.Minderjarige.Burgerservicenummer) : new ApiModels.BRP.Deprecated.Minderjarige();

                result.Add(new ApiModels.BRP.Deprecated.Voogdij
                {
                    Derden = derden,
                    Minderjarige = minderjarige,
                    InOnderzoek = voogdij.InOnderzoek
                });
            }
        }

        private static void MapGezamenlijkGezag(List<GbaPersoon> gezagPersonen, List<ApiModels.BRP.Deprecated.AbstractGezagsrelatie> result, ApiModels.Gezag.AbstractGezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is ApiModels.Gezag.GezamenlijkGezag gezamenlijkGezag)
            {
                var ouder = gezamenlijkGezag.Ouder != null ? MapPersoonToGezagOuder(gezagPersonen, gezamenlijkGezag.Ouder.Burgerservicenummer) : new ApiModels.BRP.Deprecated.GezagOuder();
                var derde = MapPersoonToDerde(gezagPersonen, gezamenlijkGezag.Derde);
                var minderjarige = gezamenlijkGezag.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, gezamenlijkGezag.Minderjarige.Burgerservicenummer) : new ApiModels.BRP.Deprecated.Minderjarige();

                result.Add(new ApiModels.BRP.Deprecated.GezamenlijkGezag
                {
                    Derde = derde,
                    Minderjarige = minderjarige,
                    Ouder = ouder,
                    InOnderzoek = gezamenlijkGezag.InOnderzoek
                });
            }
        }

        private static void MapTweehoofdigOuderlijkGezag(List<GbaPersoon> gezagPersonen, List<ApiModels.BRP.Deprecated.AbstractGezagsrelatie> result, ApiModels.Gezag.AbstractGezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is ApiModels.Gezag.TweehoofdigOuderlijkGezag tweehoofdigOuderlijkGezag)
            {
                var ouders = tweehoofdigOuderlijkGezag.Ouders?.Select(o => MapPersoonToGezagOuder(gezagPersonen, o.Burgerservicenummer)).ToList();

                var minderjarige = tweehoofdigOuderlijkGezag.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, tweehoofdigOuderlijkGezag.Minderjarige.Burgerservicenummer) : new ApiModels.BRP.Deprecated.Minderjarige();

                result.Add(new ApiModels.BRP.Deprecated.TweehoofdigOuderlijkGezag
                {
                    Minderjarige = minderjarige,
                    Ouders = ouders,
                    InOnderzoek = tweehoofdigOuderlijkGezag.InOnderzoek
                });
            }
        }

        private static void MapEenhoofdigOuderlijkGezag(List<GbaPersoon> gezagPersonen, List<ApiModels.BRP.Deprecated.AbstractGezagsrelatie> result, ApiModels.Gezag.AbstractGezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is ApiModels.Gezag.EenhoofdigOuderlijkGezag eenhoofdigOuderlijkGezag)
            {
                var ouder = eenhoofdigOuderlijkGezag.Ouder != null ? MapPersoonToGezagOuder(gezagPersonen, eenhoofdigOuderlijkGezag.Ouder.Burgerservicenummer) : new ApiModels.BRP.Deprecated.GezagOuder();
                var minderjarige = eenhoofdigOuderlijkGezag.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, eenhoofdigOuderlijkGezag.Minderjarige.Burgerservicenummer) : new ApiModels.BRP.Deprecated.Minderjarige();

                result.Add(new ApiModels.BRP.Deprecated.EenhoofdigOuderlijkGezag
                {
                    Minderjarige = minderjarige,
                    Ouder = ouder,
                    InOnderzoek = eenhoofdigOuderlijkGezag.InOnderzoek
                });
            }
        }

        private static ApiModels.BRP.Deprecated.GezagOuder MapPersoonToGezagOuder(List<GbaPersoon> personen, string bsn)
        {
            var persoon = personen.FirstOrDefault(p => p.Burgerservicenummer == bsn);

            if (persoon == null) return new ApiModels.BRP.Deprecated.GezagOuder()
            {
                Burgerservicenummer = bsn
            };

            return new ApiModels.BRP.Deprecated.GezagOuder
            {
                Burgerservicenummer = persoon.Burgerservicenummer,
                Geslacht = persoon.Geslacht,
                Naam = MapNaam(persoon)
            };
        }

        private static ApiModels.BRP.Deprecated.Minderjarige MapPersoonToMinderjarige(List<GbaPersoon> personen, string bsn)
        {
            var persoon = personen.FirstOrDefault(p => p.Burgerservicenummer == bsn);

            if (persoon == null) return new ApiModels.BRP.Deprecated.Minderjarige()
            {
                Burgerservicenummer = bsn
            };

            return new ApiModels.BRP.Deprecated.Minderjarige
            {
                Burgerservicenummer = persoon.Burgerservicenummer,
                Geboorte = MapGeboorte(persoon),
                Geslacht = persoon.Geslacht,
                Naam = MapNaam(persoon)
            };
        }

        private static List<ApiModels.BRP.Deprecated.BekendeDerde>? MapPersonenToBekendeDerden(List<GbaPersoon> personen, IEnumerable<ApiModels.Gezag.Derde>? derden)
        {
            if (derden == null) return null;

            var retval = new List<ApiModels.BRP.Deprecated.BekendeDerde>();

            foreach (var gezagDerde in derden)
            {
                var brpDerde = MapPersoonToDerde(personen, gezagDerde);
                if(brpDerde is ApiModels.BRP.Deprecated.BekendeDerde bekendeDerde)
                {
                    retval.Add(bekendeDerde);
                }
            }

            return retval;
        }

        private static ApiModels.BRP.Deprecated.Derde? MapPersoonToDerde(List<GbaPersoon> personen, ApiModels.Gezag.Derde? derde)
        {
            if (derde == null) return null;
            if (derde is ApiModels.Gezag.OnbekendeDerde) return new ApiModels.BRP.Deprecated.OnbekendeDerde();

            var bsn = ((ApiModels.Gezag.BekendeDerde)derde).Burgerservicenummer;

            var persoon = personen.FirstOrDefault(p => p.Burgerservicenummer == bsn);

            if (persoon == null) return new ApiModels.BRP.Deprecated.BekendeDerde()
            {
                Burgerservicenummer = bsn,
            };

            return new ApiModels.BRP.Deprecated.BekendeDerde
            {
                Burgerservicenummer = persoon.Burgerservicenummer,
                Geslacht = persoon.Geslacht,
                Naam = MapNaam(persoon)
            };
        }

        private static NaamBasis? MapNaam(GbaPersoon persoon)
        {
            return persoon.Naam != null ? new NaamBasis
            {
                Voornamen = persoon.Naam?.Voornamen,
                AdellijkeTitelPredicaat = persoon.Naam?.AdellijkeTitelPredicaat,
                Voorvoegsel = persoon.Naam?.Voorvoegsel,
                Geslachtsnaam = persoon.Naam?.Geslachtsnaam
            } : null;
        }

        private static GeboorteBasis? MapGeboorte(GbaPersoon persoon)
        {
            return persoon.Geboorte != null ? new GeboorteBasis()
            {
                Datum = persoon.Geboorte?.Datum
            } : null;
        }
    }
}
