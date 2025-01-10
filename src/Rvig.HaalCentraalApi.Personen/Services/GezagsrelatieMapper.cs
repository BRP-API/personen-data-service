using AutoMapper;
using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Personen.ApiModels.Gezag;

namespace Rvig.HaalCentraalApi.Personen.Services
{
    public static class GezagsrelatieMapper
    {
        public static IEnumerable<ApiModels.BRP.AbstractGezagsrelatie> Map(GezagResponse gezagResponse, List<GbaPersoon> gezagPersonen)
        {
            var result = new List<ApiModels.BRP.AbstractGezagsrelatie>();

            foreach (var persoon in gezagResponse.Personen)
            {
                foreach (var gezagsrelatie in persoon.Gezag)
                {
                    if (gezagsrelatie is ApiModels.Gezag.EenhoofdigOuderlijkGezag eenhoofdigOuderlijkGezag)
                    {
                        var ouder = MapPersoonToGezagOuder(gezagPersonen, eenhoofdigOuderlijkGezag.Ouder.Burgerservicenummer);
                        var minderjarige = MapPersoonToMinderjarige(gezagPersonen, eenhoofdigOuderlijkGezag.Minderjarige.Burgerservicenummer);

                        result.Add(new ApiModels.BRP.EenhoofdigOuderlijkGezag
                        {
                            Ouder = ouder,
                            Minderjarige = minderjarige
                        });
                    }

                    if (gezagsrelatie is ApiModels.Gezag.TweehoofdigOuderlijkGezag tweehoofdigOuderlijkGezag)
                    {
                        var ouders = tweehoofdigOuderlijkGezag.Ouders
                            .Select(o => MapPersoonToGezagOuder(gezagPersonen, o.Burgerservicenummer))
                            .ToList();

                        var minderjarige = MapPersoonToMinderjarige(gezagPersonen, tweehoofdigOuderlijkGezag.Minderjarige.Burgerservicenummer);

                        result.Add(new ApiModels.BRP.TweehoofdigOuderlijkGezag
                        {
                            Ouders = ouders,
                            Minderjarige = minderjarige
                        });
                    }

                    if (gezagsrelatie is ApiModels.Gezag.GezamenlijkGezag gezamenlijkGezag)
                    {
                        var ouder = MapPersoonToGezagOuder(gezagPersonen, gezamenlijkGezag.Ouder.Burgerservicenummer);
                        var derde = MapPersoonToMeerderjarige(gezagPersonen, gezamenlijkGezag.Derde.Burgerservicenummer);
                        var minderjarige = MapPersoonToMinderjarige(gezagPersonen, gezamenlijkGezag.Minderjarige.Burgerservicenummer);

                        result.Add(new ApiModels.BRP.GezamenlijkGezag
                        {
                            Ouder = ouder,
                            Derde = derde,
                            Minderjarige = minderjarige
                        });
                    }

                    if (gezagsrelatie is ApiModels.Gezag.Voogdij voogdij)
                    {
                        var derden = voogdij.Derden
                            .Select(d => MapPersoonToMeerderjarige(gezagPersonen, d.Burgerservicenummer))
                            .ToList();

                        var minderjarige = MapPersoonToMinderjarige(gezagPersonen, voogdij.Minderjarige.Burgerservicenummer);

                        result.Add(new ApiModels.BRP.Voogdij
                        {
                            Derden = derden,
                            Minderjarige = minderjarige
                        });
                    }

                    if (gezagsrelatie is ApiModels.Gezag.TijdelijkGeenGezag tijdelijkGeenGezag)
                    {
                        var minderjarige = MapPersoonToMinderjarige(gezagPersonen, tijdelijkGeenGezag.Minderjarige.Burgerservicenummer);

                        result.Add(new ApiModels.BRP.TijdelijkGeenGezag
                        {
                            Minderjarige = minderjarige,
                            Toelichting = tijdelijkGeenGezag.Toelichting
                        });
                    }

                    if (gezagsrelatie is ApiModels.Gezag.GezagNietTeBepalen gezagNietTeBepalen)
                    {
                        var minderjarige = MapPersoonToMinderjarige(gezagPersonen, gezagNietTeBepalen.Minderjarige.Burgerservicenummer);

                        result.Add(new ApiModels.BRP.TijdelijkGeenGezag
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

            if (persoon == null) return new ApiModels.BRP.GezagOuder();

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

            if (persoon == null) return new ApiModels.BRP.Minderjarige();

            return new ApiModels.BRP.Minderjarige
            {
                Burgerservicenummer = persoon.Burgerservicenummer,
                Naam = persoon.Naam,
                Geboorte = persoon.Geboorte,
                Geslacht = persoon.Geslacht
            };
        }

        private static ApiModels.BRP.Meerderjarige MapPersoonToMeerderjarige(List<GbaPersoon> personen, string bsn)
        {
            var persoon = personen.FirstOrDefault(p => p.Burgerservicenummer == bsn);

            if (persoon == null) return new ApiModels.BRP.Meerderjarige();

            return new ApiModels.BRP.Meerderjarige
            {
                Burgerservicenummer = persoon.Burgerservicenummer,
                Naam = persoon.Naam,
                Geslacht = persoon.Geslacht
            };
        }
    }
}
