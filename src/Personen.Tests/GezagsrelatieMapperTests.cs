using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Personen.Mappers;
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

        private readonly Gezag.GezagResponse _gezagResponse_Voogdij_ZonderDerden = new()
        {
            Personen = new List<Gezag.Persoon>()
            {
                new()
                {
                    Gezag = new List<Gezag.AbstractGezagsrelatie>()
                    {
                        new Gezag.Voogdij
                        {
                            Minderjarige = new Gezag.Minderjarige()
                            {
                                Burgerservicenummer = "000000024"
                            }
                        }
                    }
                }
            }
        };

        private readonly Gezag.GezagResponse _gezagResponse_TweehoofdigOuderlijkGezag_ZonderOuders = new()
        {
            Personen = new List<Gezag.Persoon>()
            {
                new()
                {
                    Gezag = new List<Gezag.AbstractGezagsrelatie>()
                    {
                        new Gezag.TweehoofdigOuderlijkGezag
                        {
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

        private readonly List<GbaPersoon> _gezagPersonen_ZonderNaam = new()
        {
            new()
            {
                Burgerservicenummer = "000000012",
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

        private readonly List<GbaPersoon> _gezagPersonen_ZonderGeslacht = new()
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
                }
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
                }
            }
        };

        private readonly List<GbaPersoon> _gezagPersonen_ZonderGeboorte = new()
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
                Geslacht = new Waardetabel()
                {
                    Code = "M",
                    Omschrijving = "Man"
                },
            }
        };

        private readonly List<GbaPersoon> _gezagPersonen_ZonderAdellijkeTitelPredicaat = new()
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
            var gezagsrelaties = GezagsrelatieV1Mapper.Map(_gezagResponse, _gezagPersonen);

            gezagsrelaties.Should().BeEquivalentTo(_expected);
        }

        [Fact]
        public void MapGezagsrelaties_GezagResponse_Null()
        {
            var gezagsrelaties = GezagsrelatieV1Mapper.Map(null, _gezagPersonen);

            gezagsrelaties.Should().BeEmpty();
        }

        [Fact]
        public void MapGezagsrelaties_GezagPersonen_Null()
        {
            var gezagsrelaties = GezagsrelatieV1Mapper.Map(_gezagResponse, new List<GbaPersoon>());

            gezagsrelaties.Count().Should().Be(_gezagResponse.Personen.Count);
        }

        [Fact]
        public void MapGezagsrelaties_GezagPersonen_ZonderNaam()
        {
            var gezagsrelaties = GezagsrelatieV1Mapper.Map(_gezagResponse, _gezagPersonen_ZonderNaam);

            var gezag = gezagsrelaties.First() as EenhoofdigOuderlijkGezag;

            gezag.Should().NotBeNull();
            gezag!.Ouder.Naam.Should().BeNull();
            gezag.Minderjarige.Naam.Should().BeNull();
        }

        [Fact]
        public void MapGezagsrelaties_GezagPersonen_ZonderGeslacht()
        {
            var gezagsrelaties = GezagsrelatieV1Mapper.Map(_gezagResponse, _gezagPersonen_ZonderGeslacht);

            var gezag = gezagsrelaties.First() as EenhoofdigOuderlijkGezag;

            gezag.Should().NotBeNull();
            gezag!.Ouder.Geslacht.Should().BeNull();
            gezag.Minderjarige.Geslacht.Should().BeNull();
        }

        [Fact]
        public void MapGezagsrelaties_GezagPersonen_ZonderGeboorte()
        {
            var gezagsrelaties = GezagsrelatieV1Mapper.Map(_gezagResponse, _gezagPersonen_ZonderGeboorte);

            var gezag = gezagsrelaties.First() as EenhoofdigOuderlijkGezag;

            gezag.Should().NotBeNull();
            gezag!.Minderjarige.Geboorte.Should().BeNull();
        }

        [Fact]
        public void MapGezagsrelaties_GezagPersonen_ZonderAdellijkeTitelOfPredicaat()
        {
            var gezagsrelaties = GezagsrelatieV1Mapper.Map(_gezagResponse, _gezagPersonen_ZonderAdellijkeTitelPredicaat);

            var gezag = gezagsrelaties.First() as EenhoofdigOuderlijkGezag;

            gezag.Should().NotBeNull();
            gezag!.Ouder.Naam.AdellijkeTitelPredicaat.Should().BeNull();
            gezag.Minderjarige.Naam.AdellijkeTitelPredicaat.Should().BeNull();
        }

        [Fact]
        public void MapGezagsrelaties_Voogdij_ZonderDerden()
        {
            var gezagsrelaties = GezagsrelatieV1Mapper.Map(_gezagResponse_Voogdij_ZonderDerden, new List<GbaPersoon>());

            var gezag = gezagsrelaties.First() as Voogdij;

            gezag.Should().NotBeNull();
            gezag!.Derden.Should().BeNull();
        }


        [Fact]
        public void MapGezagsrelaties_TweehoofdigOuderlijkGezag_ZonderOuders()
        {
            var gezagsrelaties = GezagsrelatieV1Mapper.Map(_gezagResponse_TweehoofdigOuderlijkGezag_ZonderOuders, new List<GbaPersoon>());

            var gezag = gezagsrelaties.First() as TweehoofdigOuderlijkGezag;

            gezag.Should().NotBeNull();
            gezag!.Ouders.Should().BeNull();
        }

        [Fact]
        public void MapGezagsrelaties_Voogdij_ShouldReturn_Minderjarige_WhenMinderjarigeIsNull()
        {
            var gezagResponse = new Gezag.GezagResponse
            {
                Personen = new List<Gezag.Persoon>
                {
                    new() {
                        Gezag = new List<Gezag.AbstractGezagsrelatie>
                        {
                            new Gezag.Voogdij
                            {
                                 Derden = new List<Gezag.BekendeDerde>
                                 {
                                     new() { Burgerservicenummer = "000000012" },
                                     new() { Burgerservicenummer = "000000013" }
                                 }
                            }
                        }
                    }
                }
            };

            var gezagsrelaties = GezagsrelatieV1Mapper.Map(gezagResponse, _gezagPersonen);

            gezagsrelaties.First().Minderjarige.Should().BeOfType<Minderjarige>();
        }

        [Theory]
        [InlineData(typeof(Gezag.EenhoofdigOuderlijkGezag))]
        [InlineData(typeof(Gezag.TweehoofdigOuderlijkGezag))]
        [InlineData(typeof(Gezag.GezamenlijkGezag))]
        [InlineData(typeof(Gezag.Voogdij))]
        [InlineData(typeof(Gezag.GezagNietTeBepalen))]
        [InlineData(typeof(Gezag.TijdelijkGeenGezag))]
        public void MapGezagsrelaties_ShouldReturn_Minderjarige_WhenMinderjarigeIsNull(Type gezagType)
        {
            Gezag.AbstractGezagsrelatie? gezagsrelatie = (Gezag.AbstractGezagsrelatie)Activator.CreateInstance(gezagType)!;

            var gezagResponse = new Gezag.GezagResponse
            {
                Personen = new List<Gezag.Persoon>
                {
                    new() {
                        Gezag = new List<Gezag.AbstractGezagsrelatie>
                        {
                           gezagsrelatie
                        }
                    }
                }
            };

            var gezagsrelaties = GezagsrelatieV1Mapper.Map(gezagResponse, _gezagPersonen);

            gezagsrelaties.First().Minderjarige.Should().BeOfType<Minderjarige>();
        }

        [Fact]
        public void MapGezagsrelaties_GezamenlijkGezagMetBekendeDerde()
        {
            var personen = new List<GbaPersoon>
            {
                new()
                {
                    Burgerservicenummer = "000000036",
                    Naam = new GbaNaamPersoon()
                    {
                        Geslachtsnaam = "Jansen",
                        Voornamen = "Anna",
                        Voorvoegsel = "van",
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
            };
            var input = new Gezag.GezagResponse
            {
                Personen = new[]
                {
                    new Gezag.Persoon
                    {
                        Gezag = new[]
                        {
                            new Gezag.GezamenlijkGezag
                            {
                                Ouder = new Gezag.GezagOuder { Burgerservicenummer = "000000012" },
                                Minderjarige = new Gezag.Minderjarige { Burgerservicenummer = "000000024" },
                                Derde = new Gezag.BekendeDerde { Burgerservicenummer = "000000036" }
                            }
                        }
                    }
                }
            };
            var expected = new List<AbstractGezagsrelatie>{
                new GezamenlijkGezag
                {
                    Ouder = new GezagOuder { Burgerservicenummer = "000000012" },
                    Minderjarige = new Minderjarige { Burgerservicenummer = "000000024" },
                    Derde = new BekendeDerde
                    {
                        Burgerservicenummer = "000000036",
                        Naam = new()
                        {
                            Geslachtsnaam = "Jansen",
                            Voornamen = "Anna",
                            Voorvoegsel = "van",
                            AdellijkeTitelPredicaat = new() { Code = "JH", Omschrijving = "Jonkheer", Soort = AdellijkeTitelPredicaatSoort.PredicaatEnum }
                        },
                        Geslacht = new() { Code = "V", Omschrijving = "Vrouw" },
                    },
                }
            };

            GezagsrelatieV1Mapper.Map(input, personen).Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void MapGezagsrelaties_GezamenlijkGezagMetOnbekendeDerde()
        {
            var input = new Gezag.GezagResponse
            {
                Personen = new[]
                {
                    new Gezag.Persoon
                    {
                        Gezag = new[]
                        {
                            new Gezag.GezamenlijkGezag
                            {
                                Ouder = new Gezag.GezagOuder { Burgerservicenummer = "000000012" },
                                Minderjarige = new Gezag.Minderjarige { Burgerservicenummer = "000000024" },
                                Derde = new Gezag.OnbekendeDerde()
                            }
                        }
                    }
                }
            };
            var expected = new List<AbstractGezagsrelatie>{
                new GezamenlijkGezag
                {
                    Ouder = new GezagOuder { Burgerservicenummer = "000000012" },
                    Minderjarige = new Minderjarige { Burgerservicenummer = "000000024" },
                    Derde = new OnbekendeDerde()
                }
            };

            GezagsrelatieV1Mapper.Map(input, new List<GbaPersoon>()).Should().BeEquivalentTo(expected);
        }
    }
}
