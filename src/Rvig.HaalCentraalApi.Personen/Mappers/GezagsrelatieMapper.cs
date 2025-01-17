using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Personen.ApiModels.Gezag;

namespace Rvig.HaalCentraalApi.Personen.Mappers
{
    public static class GezagsrelatieMapper
    {
        public static IEnumerable<ApiModels.BRP.AbstractGezagsrelatie> Map(GezagResponse gezagResponse, List<GbaPersoon> gezagPersonen)
        {
            var result = new List<ApiModels.BRP.AbstractGezagsrelatie>();

            if (gezagResponse == null) return result;

            foreach (var persoon in gezagResponse.Personen)
            {
                foreach (var gezagsrelatie in persoon.Gezag)
                {
                    if (gezagsrelatie is ApiModels.Gezag.EenhoofdigOuderlijkGezag eenhoofdigOuderlijkGezag)
                    {
                        var ouder = eenhoofdigOuderlijkGezag.Ouder != null ? MapPersoonToGezagOuder(gezagPersonen, eenhoofdigOuderlijkGezag.Ouder.Burgerservicenummer) : new ApiModels.BRP.GezagOuder();
                        var minderjarige = eenhoofdigOuderlijkGezag.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, eenhoofdigOuderlijkGezag.Minderjarige.Burgerservicenummer) : new ApiModels.BRP.Minderjarige();

                        result.Add(new ApiModels.BRP.EenhoofdigOuderlijkGezag
                        {
                            Ouder = ouder,
                            Minderjarige = minderjarige
                        });
                    }

                    if (gezagsrelatie is ApiModels.Gezag.TweehoofdigOuderlijkGezag tweehoofdigOuderlijkGezag)
                    {
                        var ouders = tweehoofdigOuderlijkGezag.Ouders?.Select(o => MapPersoonToGezagOuder(gezagPersonen, o.Burgerservicenummer)).ToList();

                        var minderjarige = tweehoofdigOuderlijkGezag.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, tweehoofdigOuderlijkGezag.Minderjarige.Burgerservicenummer) : new ApiModels.BRP.Minderjarige();

                        result.Add(new ApiModels.BRP.TweehoofdigOuderlijkGezag
                        {
                            Ouders = ouders,
                            Minderjarige = minderjarige
                        });
                    }

                    if (gezagsrelatie is ApiModels.Gezag.GezamenlijkGezag gezamenlijkGezag)
                    {
                        var ouder = gezamenlijkGezag.Ouder != null ? MapPersoonToGezagOuder(gezagPersonen, gezamenlijkGezag.Ouder.Burgerservicenummer) : new ApiModels.BRP.GezagOuder();
                        var derde = gezamenlijkGezag.Derde != null ? MapPersoonToMeerderjarige(gezagPersonen, gezamenlijkGezag.Derde.Burgerservicenummer) : new ApiModels.BRP.Meerderjarige();
                        var minderjarige = gezamenlijkGezag.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, gezamenlijkGezag.Minderjarige.Burgerservicenummer) : new ApiModels.BRP.Minderjarige();

                        result.Add(new ApiModels.BRP.GezamenlijkGezag
                        {
                            Ouder = ouder,
                            Derde = derde,
                            Minderjarige = minderjarige
                        });
                    }

                    if (gezagsrelatie is ApiModels.Gezag.Voogdij voogdij)
                    {
                        var derden = voogdij.Derden?
                            .Select(d => MapPersoonToMeerderjarige(gezagPersonen, d.Burgerservicenummer))
                            .ToList();

                        var minderjarige = voogdij.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, voogdij.Minderjarige.Burgerservicenummer) : new ApiModels.BRP.Minderjarige();

                        result.Add(new ApiModels.BRP.Voogdij
                        {
                            Derden = derden,
                            Minderjarige = minderjarige
                        });
                    }

                    if (gezagsrelatie is ApiModels.Gezag.TijdelijkGeenGezag tijdelijkGeenGezag)
                    {
                        var minderjarige = tijdelijkGeenGezag.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, tijdelijkGeenGezag.Minderjarige.Burgerservicenummer) : new ApiModels.BRP.Minderjarige();

                        result.Add(new ApiModels.BRP.TijdelijkGeenGezag
                        {
                            Minderjarige = minderjarige,
                            Toelichting = tijdelijkGeenGezag.Toelichting
                        });
                    }

                    if (gezagsrelatie is ApiModels.Gezag.GezagNietTeBepalen gezagNietTeBepalen)
                    {
                        var minderjarige = gezagNietTeBepalen.Minderjarige != null ? MapPersoonToMinderjarige(gezagPersonen, gezagNietTeBepalen.Minderjarige.Burgerservicenummer) : new ApiModels.BRP.Minderjarige();

                        result.Add(new ApiModels.BRP.GezagNietTeBepalen
                        {
                            Minderjarige = minderjarige,
                            Toelichting = gezagNietTeBepalen.Toelichting
                        });
                    }
                }
            }

            return result;
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
                Naam = persoon.Naam,
                Geslacht = persoon.Geslacht
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
                Naam = persoon.Naam,
                Geboorte = new Shared.ApiModels.PersonenHistorieBase.GbaGeboorteBeperkt()
                {
                    Datum = persoon.Geboorte?.Datum

                },
                Geslacht = persoon.Geslacht
            };
        }

        private static ApiModels.BRP.Meerderjarige MapPersoonToMeerderjarige(List<GbaPersoon> personen, string bsn)
        {
            var persoon = personen.FirstOrDefault(p => p.Burgerservicenummer == bsn);

            if (persoon == null) return new ApiModels.BRP.Meerderjarige()
            {
                Burgerservicenummer = bsn
            };

            return new ApiModels.BRP.Meerderjarige
            {
                Burgerservicenummer = persoon.Burgerservicenummer,
                Naam = persoon.Naam,
                Geslacht = persoon.Geslacht
            };
        }
    }
}
