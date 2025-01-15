namespace Rvig.HaalCentraalApi.Personen.Services
{
    public static class GezagHelper
    {
        public static List<string> GetGezagBsns(List<ApiModels.Gezag.AbstractGezagsrelatie> gezagsrelaties)
        {
            var gezagBsns = new List<string>();

            foreach (var x in gezagsrelaties)
            {
                if (x is ApiModels.Gezag.TweehoofdigOuderlijkGezag y)
                {
                    gezagBsns.AddRange(from z in y.Ouders
                                       select z.Burgerservicenummer);
                    gezagBsns.Add(y.Minderjarige.Burgerservicenummer);
                }
                if (x is ApiModels.Gezag.EenhoofdigOuderlijkGezag y2)
                {
                    gezagBsns.Add(y2.Ouder.Burgerservicenummer);
                    gezagBsns.Add(y2.Minderjarige.Burgerservicenummer);
                }
                if (x is ApiModels.Gezag.Voogdij v)
                {
                    gezagBsns.AddRange(from z in v.Derden
                                       select z.Burgerservicenummer);
                    gezagBsns.Add(v.Minderjarige.Burgerservicenummer);
                }
                if (x is ApiModels.Gezag.GezamenlijkGezag gz)
                {
                    gezagBsns.Add(gz.Derde.Burgerservicenummer);
                    gezagBsns.Add(gz.Ouder.Burgerservicenummer);
                    gezagBsns.Add(gz.Minderjarige.Burgerservicenummer);
                }
                if (x is ApiModels.Gezag.GezagNietTeBepalen gn && gn.Minderjarige != null)
                {
                    gezagBsns.Add(gn.Minderjarige.Burgerservicenummer);
                }
                if (x is ApiModels.Gezag.TijdelijkGeenGezag tg && tg.Minderjarige != null)
                {
                    gezagBsns.Add(tg.Minderjarige.Burgerservicenummer);
                }
            }

            return gezagBsns.Distinct().ToList();
        }
    }
}
