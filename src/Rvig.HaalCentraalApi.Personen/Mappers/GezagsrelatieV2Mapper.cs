using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Personen.ApiModels.GezagV2;
using Rvig.HaalCentraalApi.Shared.ApiModels.PersonenHistorieBase;

namespace Rvig.HaalCentraalApi.Personen.Mappers
{
    public static class GezagsrelatieV2Mapper
    {
        public static IEnumerable<AbstractGezagsrelatie> Map(GezagResponse gezagResponse, List<GbaPersoon> gezagPersonen)
        {
            var result = new List<AbstractGezagsrelatie>();

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

        private static void MapGezagNietTeBepalen(List<GbaPersoon> gezagPersonen, List<AbstractGezagsrelatie> result, Gezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is ApiModels.GezagV2.GezagNietTeBepalen gezagNietTeBepalen)
            {
                var minderjarige = gezagNietTeBepalen.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, gezagNietTeBepalen.Minderjarige.Burgerservicenummer) : new ApiModels.BRP.Minderjarige();

                result.Add(new ApiModels.BRP.GezagNietTeBepalen
                {
                    Minderjarige = minderjarige,
                    Toelichting = gezagNietTeBepalen.Toelichting,
                    InOnderzoek = gezagNietTeBepalen.InOnderzoek
                });
            }
        }

        private static void MapTijdelijkGeenGezag(List<GbaPersoon> gezagPersonen, List<AbstractGezagsrelatie> result, Gezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is ApiModels.GezagV2.TijdelijkGeenGezag tijdelijkGeenGezag)
            {
                var minderjarige = tijdelijkGeenGezag.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, tijdelijkGeenGezag.Minderjarige.Burgerservicenummer) : new ApiModels.BRP.Minderjarige();

                result.Add(new ApiModels.BRP.TijdelijkGeenGezag
                {
                    Minderjarige = minderjarige,
                    Toelichting = tijdelijkGeenGezag.Toelichting,
                    InOnderzoek = tijdelijkGeenGezag.InOnderzoek
                });
            }
        }

        private static void MapVoogdij(List<GbaPersoon> gezagPersonen, List<AbstractGezagsrelatie> result, Gezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is ApiModels.GezagV2.Voogdij voogdij)
            {
                var derden = MapPersonenToBekendeDerden(gezagPersonen, voogdij.Derden);

                var minderjarige = voogdij.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, voogdij.Minderjarige.Burgerservicenummer) : new ApiModels.BRP.Minderjarige();

                result.Add(new ApiModels.BRP.Voogdij
                {
                    Derden = derden,
                    Minderjarige = minderjarige,
                    InOnderzoek = voogdij.InOnderzoek
                });
            }
        }

        private static void MapGezamenlijkGezag(List<GbaPersoon> gezagPersonen, List<AbstractGezagsrelatie> result, Gezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is ApiModels.GezagV2.GezamenlijkGezag gezamenlijkGezag)
            {
                var ouder = gezamenlijkGezag.Ouder != null ? MapPersoonToGezagOuder(gezagPersonen, gezamenlijkGezag.Ouder.Burgerservicenummer) : new ApiModels.BRP.GezagOuder();
                var derde = MapPersoonToDerde(gezagPersonen, gezamenlijkGezag.Derde);
                var minderjarige = gezamenlijkGezag.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, gezamenlijkGezag.Minderjarige.Burgerservicenummer) : new ApiModels.BRP.Minderjarige();

                result.Add(new ApiModels.BRP.GezamenlijkGezag
                {
                    Derde = derde,
                    Minderjarige = minderjarige,
                    Ouder = ouder,
                    InOnderzoek = gezamenlijkGezag.InOnderzoek
                });
            }
        }

        private static void MapTweehoofdigOuderlijkGezag(List<GbaPersoon> gezagPersonen, List<AbstractGezagsrelatie> result, Gezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is GezamenlijkOuderlijkGezag gezamenlijkOuderlijkGezag)
            {
                var ouders = gezamenlijkOuderlijkGezag.Ouders?.Select(o => MapPersoonToGezagOuder(gezagPersonen, o.Burgerservicenummer)).ToList();

                var minderjarige = gezamenlijkOuderlijkGezag.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, gezamenlijkOuderlijkGezag.Minderjarige.Burgerservicenummer) : new ApiModels.BRP.Minderjarige();

                result.Add(new ApiModels.BRP.TweehoofdigOuderlijkGezag
                {
                    Minderjarige = minderjarige,
                    Ouders = ouders,
                    InOnderzoek = gezamenlijkOuderlijkGezag.InOnderzoek
                });
            }
        }

        private static void MapEenhoofdigOuderlijkGezag(List<GbaPersoon> gezagPersonen, List<AbstractGezagsrelatie> result, Gezagsrelatie? gezagsrelatie)
        {
            if (gezagsrelatie is ApiModels.GezagV2.EenhoofdigOuderlijkGezag eenhoofdigOuderlijkGezag)
            {
                var ouder = eenhoofdigOuderlijkGezag.Ouder != null ? MapPersoonToGezagOuder(gezagPersonen, eenhoofdigOuderlijkGezag.Ouder.Burgerservicenummer) : new ApiModels.BRP.GezagOuder();
                var minderjarige = eenhoofdigOuderlijkGezag.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, eenhoofdigOuderlijkGezag.Minderjarige.Burgerservicenummer) : new ApiModels.BRP.Minderjarige();

                result.Add(new ApiModels.BRP.EenhoofdigOuderlijkGezag
                {
                    Minderjarige = minderjarige,
                    Ouder = ouder,
                    InOnderzoek = eenhoofdigOuderlijkGezag.InOnderzoek
                });
            }
        }

        private static ApiModels.BRP.GezagOuder MapPersoonToGezagOuder(List<GbaPersoon> personen, string bsn)
        {
            var persoon = personen.FirstOrDefault(p => p.Burgerservicenummer == bsn);

            if (persoon == null) return new ApiModels.BRP.GezagOuder()
            {
                Burgerservicenummer = bsn
            };

            return new ApiModels.BRP.GezagOuder
            {
                Burgerservicenummer = persoon.Burgerservicenummer,
                Geslacht = persoon.Geslacht,
                Naam = MapNaam(persoon)
            };
        }

        private static ApiModels.BRP.Minderjarige MapPersoonToMinderjarige(List<GbaPersoon> personen, string bsn)
        {
            var persoon = personen.FirstOrDefault(p => p.Burgerservicenummer == bsn);

            if (persoon == null) return new ApiModels.BRP.Minderjarige()
            {
                Burgerservicenummer = bsn
            };

            return new ApiModels.BRP.Minderjarige
            {
                Burgerservicenummer = persoon.Burgerservicenummer,
                Geboorte = MapGeboorte(persoon),
                Geslacht = persoon.Geslacht,
                Naam = MapNaam(persoon)
            };
        }

        private static List<ApiModels.BRP.BekendeDerde>? MapPersonenToBekendeDerden(List<GbaPersoon> personen, IEnumerable<ApiModels.GezagV2.Derde>? derden)
        {
            if (derden == null) return null;

            var retval = new List<ApiModels.BRP.BekendeDerde>();

            foreach (var gezagDerde in derden)
            {
                var brpDerde = MapPersoonToDerde(personen, gezagDerde);
                if (brpDerde is ApiModels.BRP.BekendeDerde bekendeDerde)
                {
                    retval.Add(bekendeDerde);
                }
            }

            return retval;
        }

        private static ApiModels.BRP.Derde? MapPersoonToDerde(List<GbaPersoon> personen, ApiModels.GezagV2.Derde? derde)
        {
            if (derde == null) return null;
            if (derde is not ApiModels.GezagV2.BekendeDerde) return new OnbekendeDerde();
            var bsn = ((ApiModels.GezagV2.BekendeDerde)derde).Burgerservicenummer;

            var persoon = personen.FirstOrDefault(p => p.Burgerservicenummer == bsn);

            if (persoon == null) return new ApiModels.BRP.BekendeDerde()
            {
                Burgerservicenummer = bsn,
            };

            return new ApiModels.BRP.BekendeDerde
            {
                Burgerservicenummer = persoon.Burgerservicenummer,
                Geslacht = persoon.Geslacht,
                Naam = MapNaam(persoon)
            };
        }

        private static GbaNaamBasis? MapNaam(GbaPersoon persoon)
        {
            return persoon.Naam != null ? new GbaNaamBasis
            {
                Voornamen = persoon.Naam?.Voornamen,
                AdellijkeTitelPredicaat = persoon.Naam?.AdellijkeTitelPredicaat,
                Voorvoegsel = persoon.Naam?.Voorvoegsel,
                Geslachtsnaam = persoon.Naam?.Geslachtsnaam
            } : null;
        }

        private static GbaGeboorteBeperkt? MapGeboorte(GbaPersoon persoon)
        {
            return persoon.Geboorte != null ? new GbaGeboorteBeperkt()
            {
                Datum = persoon.Geboorte?.Datum
            } : null;
        }
    }
}
