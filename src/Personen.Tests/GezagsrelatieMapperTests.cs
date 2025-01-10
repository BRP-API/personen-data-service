using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Personen.Services;
using Rvig.HaalCentraalApi.Shared.ApiModels.PersonenHistorieBase;
using Rvig.HaalCentraalApi.Shared.ApiModels.Universal;
using Gezag = Rvig.HaalCentraalApi.Personen.ApiModels.Gezag;

namespace Personen.Tests
{
    public class GezagsrelatieMapperTests
    {
        private readonly Gezag.GezagResponse _gezagResponse = new()
        {
            Personen = new List<Gezag.Persoon>()
            {
                new()
                {
                    Gezag = new List<Gezag.AbstractGezagsrelatie>()
                    {
                        new Gezag.EenhoofdigOuderlijkGezag
                        {
                            Ouder = new Gezag.GezagOuder()
                            {
                                Burgerservicenummer = "000000012"
                            },
                            Minderjarige = new Gezag.Minderjarige()
                            {
                                Burgerservicenummer = "000000024"
                            }
                        }
                    }
                }
            }
        };

        private readonly List<GbaPersoon> _gezagPersonen = new()
        {
            new()
            {
                Burgerservicenummer = "000000012",
                Naam = new GbaNaamPersoon()
                {
                    Geslachtsnaam = "Jansen",
                    Voornamen = "Anna",
                    Voorvoegsel = "van",
                    AanduidingNaamgebruik = new Waardetabel()
                    {
                        Code = "E",
                        Omschrijving = "Eigen naam"
                    },
                    AdellijkeTitelPredicaat = new AdellijkeTitelPredicaatType()
                    {
                        Code = "JH",
                        Omschrijving = "Jonkheer",
                        Soort = AdellijkeTitelPredicaatSoort.PredicaatEnum
                    }
                },
                Geboorte = new GbaGeboorte()
                {
                    Datum = "20200101",
                    Land = new Waardetabel()
                    {
                        Code = "6030",
                        Omschrijving = "Nederland"
                    },
                    Plaats = new Waardetabel()
                    {
                        Code = "6030",
                        Omschrijving = "Amsterdam"
                    }
                },
                Geslacht = new Waardetabel()
                {
                    Code = "V",
                    Omschrijving = "Vrouw"
                },
            },
            new()
            {
                Burgerservicenummer = "000000024",
                Naam = new GbaNaamPersoon()
                {
                    Geslachtsnaam = "Jansen",
                    Voornamen = "Alex",
                    Voorvoegsel = "van",
                    AanduidingNaamgebruik = new Waardetabel()
                    {
                        Code = "E",
                        Omschrijving = "Eigen naam"
                    },
                    AdellijkeTitelPredicaat = new AdellijkeTitelPredicaatType()
                    {
                        Code = "JH",
                        Omschrijving = "Jonkheer",
                        Soort = AdellijkeTitelPredicaatSoort.PredicaatEnum
                    }
                },
                Geboorte = new GbaGeboorte()
                {
                    Datum = "20200101",
                    Land = new Waardetabel()
                    {
                        Code = "6030",
                        Omschrijving = "Nederland"
                    },
                    Plaats = new Waardetabel()
                    {
                        Code = "6030",
                        Omschrijving = "Amsterdam"
                    }
                },
                Geslacht = new Waardetabel()
                {
                    Code = "M",
                    Omschrijving = "Man"
                },
            }
        };

        private readonly List<AbstractGezagsrelatie> _expected = new()
        {
            new EenhoofdigOuderlijkGezag
            {
                Ouder = new GezagOuder()
                {
                    Burgerservicenummer = "000000012",
                    Naam = new GbaNaamPersoon()
                    {
                        Geslachtsnaam = "Jansen",
                        Voornamen = "Anna",
                        Voorvoegsel = "van",
                        AanduidingNaamgebruik = new Waardetabel()
                        {
                            Code = "E",
                            Omschrijving = "Eigen naam"
                        },
                        AdellijkeTitelPredicaat = new AdellijkeTitelPredicaatType()
                        {
                            Code = "JH",
                            Omschrijving = "Jonkheer",
                            Soort = AdellijkeTitelPredicaatSoort.PredicaatEnum
                        }
                    },
                    Geslacht = new Waardetabel()
                    {
                        Code = "V",
                        Omschrijving = "Vrouw"
                    },
                },
                Minderjarige = new Minderjarige()
                {
                    Burgerservicenummer = "000000024",
                    Naam = new GbaNaamPersoon()
                    {
                        Geslachtsnaam = "Jansen",
                        Voornamen = "Alex",
                        Voorvoegsel = "van",
                        AanduidingNaamgebruik = new Waardetabel()
                        {
                            Code = "E",
                            Omschrijving = "Eigen naam"
                        },
                        AdellijkeTitelPredicaat = new AdellijkeTitelPredicaatType()
                        {
                            Code = "JH",
                            Omschrijving = "Jonkheer",
                            Soort = AdellijkeTitelPredicaatSoort.PredicaatEnum
                        }
                    },
                    Geboorte = new GbaGeboorte()
                    {
                        Datum = "20200101",
                        Land = new Waardetabel()
                        {
                            Code = "6030",
                            Omschrijving = "Nederland"
                        },
                        Plaats = new Waardetabel()
                        {
                            Code = "6030",
                            Omschrijving = "Amsterdam"
                        }
                    },
                    Geslacht = new Waardetabel()
                    {
                        Code = "M",
                        Omschrijving = "Man"
                    },
                }
            }
        };

        [Fact]
        public void MapGezagsrelaties()
        {
            var gezagsrelaties = GezagsrelatieMapper.Map(_gezagResponse, _gezagPersonen);

            gezagsrelaties.Should().BeEquivalentTo(_expected);
        }
    }
}