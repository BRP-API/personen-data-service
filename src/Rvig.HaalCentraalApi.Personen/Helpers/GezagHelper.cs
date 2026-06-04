using Rvig.HaalCentraalApi.Personen.ApiModels.Gezag.Deprecated;

namespace Rvig.HaalCentraalApi.Personen.Helpers
{
    public static class GezagHelper
    {
        public static bool GezagIsRequested(List<string> fields) =>
            fields.Any(field =>
                field.Contains("gezag", StringComparison.CurrentCultureIgnoreCase) &&
                !field.StartsWith("indicatieGezagMinderjarige"));

        public static List<string> GetGezagBsns(List<AbstractGezagsrelatie> gezagsrelaties)
        {
            var gezagBsns = new List<string>();

            foreach (var x in gezagsrelaties)
            {
                GetTweehoofdigOuderlijkGezagBsns(gezagBsns, x);
                GetEenhoofdigOuderlijkGezagBsns(gezagBsns, x);
                GetVoogdijBsns(gezagBsns, x);
                GetGezamenlijkGezagBsns(gezagBsns, x);
                GetGezagNietTeBepalenBsns(gezagBsns, x);
                GetTijdelijkGeenGezagBsns(gezagBsns, x);
            }

            return gezagBsns.Distinct().ToList();
        }

        private static void GetTijdelijkGeenGezagBsns(List<string> gezagBsns, AbstractGezagsrelatie x)
        {
            if (x is TijdelijkGeenGezag tg && tg.Minderjarige != null)
            {
                gezagBsns.Add(tg.Minderjarige.Burgerservicenummer);
            }
        }

        private static void GetGezagNietTeBepalenBsns(List<string> gezagBsns, AbstractGezagsrelatie x)
        {
            if (x is GezagNietTeBepalen gn && gn.Minderjarige != null)
            {
                gezagBsns.Add(gn.Minderjarige.Burgerservicenummer);
            }
        }

        private static void GetGezamenlijkGezagBsns(List<string> gezagBsns, AbstractGezagsrelatie x)
        {
            if (x is GezamenlijkGezag gz)
            {
                if (gz.Derde is BekendeDerde bekendeDerde)
                {
                    gezagBsns.Add(bekendeDerde.Burgerservicenummer);
                }
                gezagBsns.Add(gz.Ouder.Burgerservicenummer);
                gezagBsns.Add(gz.Minderjarige.Burgerservicenummer);
            }
        }

        private static void GetVoogdijBsns(List<string> gezagBsns, AbstractGezagsrelatie x)
        {
            if (x is Voogdij v)
            {
                gezagBsns.AddRange(from z in v.Derden
                                   select z.Burgerservicenummer);
                gezagBsns.Add(v.Minderjarige.Burgerservicenummer);
            }
        }

        private static void GetEenhoofdigOuderlijkGezagBsns(List<string> gezagBsns, AbstractGezagsrelatie x)
        {
            if (x is EenhoofdigOuderlijkGezag y2)
            {
                gezagBsns.Add(y2.Ouder.Burgerservicenummer);
                gezagBsns.Add(y2.Minderjarige.Burgerservicenummer);
            }
        }

        private static void GetTweehoofdigOuderlijkGezagBsns(List<string> gezagBsns, AbstractGezagsrelatie x)
        {
            if (x is TweehoofdigOuderlijkGezag y)
            {
                gezagBsns.AddRange(from z in y.Ouders
                                   select z.Burgerservicenummer);
                gezagBsns.Add(y.Minderjarige.Burgerservicenummer);
            }
        }
    }
}
