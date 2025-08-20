using NSubstitute;
using BRP = Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated;
using Rvig.HaalCentraalApi.Personen.ApiModels.Gezag.Deprecated;
using Rvig.HaalCentraalApi.Personen.Interfaces;
using Rvig.HaalCentraalApi.Personen.Repositories;
using Rvig.HaalCentraalApi.Personen.Services;
using Gezag = Rvig.HaalCentraalApi.Personen.ApiModels.Gezag.Deprecated;
using Rvig.HaalCentraalApi.Shared.Interfaces;

namespace Personen.Tests
{

    public class GezagServiceTests
    {
        private readonly GezagServiceDeprecated _gezagService;
        private readonly IRepoGezagsrelatie _gezagRepositoryMock;
        private readonly IGezagPersonenService _personenServiceMock;
        private readonly IDomeinTabellenRepo _domeinTabellenRepoMock;

        public GezagServiceTests()
        {
            _personenServiceMock = Substitute.For<IGezagPersonenService>();
            _gezagRepositoryMock = Substitute.For<IRepoGezagsrelatie>();
            _domeinTabellenRepoMock = Substitute.For<IDomeinTabellenRepo>();
            _gezagService = new GezagServiceDeprecated(_personenServiceMock, _domeinTabellenRepoMock, _gezagRepositoryMock);
        }

        [Fact]
        public async Task GetGezagIfRequested_ReturnsEmpty_WhenGezagIsNotRequested()
        {
            // Arrange
            var fields = new List<string> { "adressering" };
            var bsns = new List<string> { "000000012", "000000013" };

            // Act
            var result = await _gezagService.GetGezagDeprecatedIfRequested(fields, bsns);

            // Assert
            result.Count().Should().Be(0);
        }

        [Fact]
        public async Task GetGezagIfRequested_ReturnsGezag_WhenGezagIsRequested()
        {
            // Arrange
            var fields = new List<string> { "gezag" };
            var bsns = new List<string> { "000000012", "000000013" };

            var mockGezagResponse = new GezagResponse
            {
                Personen = new List<Persoon>
                {
                    new() { Burgerservicenummer = "000000012", Gezag = new List<Gezag.AbstractGezagsrelatie>() },
                    new() { Burgerservicenummer = "000000013", Gezag = new List<Gezag.AbstractGezagsrelatie>() }
                }
            };

            _gezagRepositoryMock
                .GetGezagDeprecated(bsns!)
                .Returns(mockGezagResponse);

            // Act
            var result = await _gezagService.GetGezagDeprecatedIfRequested(fields, bsns);

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(2);
        }

        [Fact]
        public async Task GetGezagPersonenIfRequested_ReturnsGezagPersonen_WhenGezagIsRequested()
        {
            // Arrange
            var fields = new List<string> { "gezag" };
            var bsnMinderjarige = "000000012";
            var bsnOuder = "000000013";

            var gezagMinderjarige = new Persoon()
            {
                Burgerservicenummer = bsnMinderjarige,
                Gezag = new List<Gezag.AbstractGezagsrelatie>()
                   {
                       new Gezag.EenhoofdigOuderlijkGezag()
                       {
                           Minderjarige = new()
                           {
                               Burgerservicenummer = bsnMinderjarige
                           },
                           Ouder = new()
                           {
                               Burgerservicenummer = bsnOuder
                           }
                       }
                   }
            };

            var gezagOuder = new Persoon()
            {
                Burgerservicenummer = bsnOuder,
                Gezag = new List<Gezag.AbstractGezagsrelatie>()
                   {
                        new Gezag.EenhoofdigOuderlijkGezag()
                        {
                            Minderjarige = new()
                            {
                                Burgerservicenummer = bsnMinderjarige
                            },
                            Ouder = new()
                            {
                                Burgerservicenummer = bsnOuder
                            }
                        }
                   }
            };

            var gezag = new List<Persoon>() {
                gezagMinderjarige,
                gezagOuder
            };

            var mockGezagPersonen = new List<BRP.GbaPersoon>()
            {
                new() { Burgerservicenummer = bsnMinderjarige },
                new() { Burgerservicenummer = bsnOuder }
            };

            _personenServiceMock
                .GetGezagPersonen(Arg.Any<List<string>>())
                .Returns(mockGezagPersonen);

            // Act
            var result = await _gezagService.GetGezagPersonenIfRequested(fields, gezag);

            // Assert
            result[0].Burgerservicenummer.Should().Be(bsnMinderjarige);
            result[1].Burgerservicenummer.Should().Be(bsnOuder);
        }

        [Fact]
        public async Task GetGezagPersonenIfRequested_ReturnsEmptyGezagPersonen_WhenGezagIsNotRequested()
        {
            // Arrange
            var fields = new List<string> { "adressering" };
            var bsnMinderjarige = "000000012";
            var bsnOuder = "000000013";

            var gezagMinderjarige = new Persoon()
            {
                Burgerservicenummer = bsnMinderjarige,
                Gezag = new List<Gezag.AbstractGezagsrelatie>()
                   {
                       new Gezag.EenhoofdigOuderlijkGezag()
                       {
                           Minderjarige = new()
                           {
                               Burgerservicenummer = bsnMinderjarige
                           },
                           Ouder = new()
                           {
                               Burgerservicenummer = bsnOuder
                           }
                       }
                   }
            };

            var gezagOuder = new Persoon()
            {
                Burgerservicenummer = bsnOuder,
                Gezag = new List<Gezag.AbstractGezagsrelatie>()
                   {
                        new Gezag.EenhoofdigOuderlijkGezag()
                        {
                            Minderjarige = new()
                            {
                                Burgerservicenummer = bsnMinderjarige
                            },
                            Ouder = new()
                            {
                                Burgerservicenummer = bsnOuder
                            }
                        }
                   }
            };

            var gezag = new List<Persoon>() {
                gezagMinderjarige,
                gezagOuder
            };

            var mockGezagPersonen = new List<BRP.GbaPersoon>()
            {
                new() { Burgerservicenummer = bsnMinderjarige },
                new() { Burgerservicenummer = bsnOuder }
            };

            _personenServiceMock
                .GetGezagPersonen(Arg.Any<List<string>>())
                .Returns(mockGezagPersonen);

            // Act
            var result = await _gezagService.GetGezagPersonenIfRequested(fields, gezag);

            // Assert
            result.Count.Should().Be(0);
        }

        [Fact]
        public void VerrijkPersonenMetGezagIfRequested_VerrijktPersoonMetGezag_WhenGezagIsRequested()
        {
            // Arrange
            var fields = new List<string> { "gezag" };
            var bsn = "000000012";

            var persoonGezagsrelaties = new List<Persoon>
            {
                new() {
                    Burgerservicenummer = bsn,
                    Gezag = new List<Gezag.AbstractGezagsrelatie>
                    {
                        new Gezag.EenhoofdigOuderlijkGezag()
                        {
                            Minderjarige = new()
                            {
                                Burgerservicenummer = bsn
                            }
                        }
                    }
                }
            };

            var gezagPersonen = new List<GbaPersoon>
            {
                new() { Burgerservicenummer = bsn, Naam = new() { Voornamen = "voornamen", Voorvoegsel = "voorvoegsel", Geslachtsnaam = "geslachtsnaam" } }
            };

            var inputPersoon = (new GbaPersoon { Burgerservicenummer = bsn }, 1L);

            var expectedGezag = new List<Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated.AbstractGezagsrelatie>
            {
                new Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated.EenhoofdigOuderlijkGezag()
                {
                    Minderjarige = new Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated.Minderjarige()
                    {
                        Burgerservicenummer = bsn,
                        Naam = new NaamBasis()
                        {
                            Voornamen = "voornamen",
                            Voorvoegsel = "voorvoegsel",
                            Geslachtsnaam = "geslachtsnaam",
                            AdellijkeTitelPredicaat = null
                        }
                    }
                }
            };

            // Act
            _gezagService.VerrijkPersonenMetGezagIfRequested(fields, persoonGezagsrelaties, gezagPersonen, inputPersoon);

            // Assert
            inputPersoon.Item1.Gezag.Should().BeEquivalentTo(expectedGezag);
            inputPersoon.Item1.Gezag![0].Minderjarige.Should().BeEquivalentTo(expectedGezag[0].Minderjarige);
        }

        [Fact]
        public void VerrijkPersonenMetGezagIfRequested_VerrijktPersoonNietMetGezag_WhenGezagIsNotRequested()
        {
            // Arrange
            var fields = new List<string> { "adressering" };
            var bsn = "000000012";

            var persoonGezagsrelaties = new List<Persoon>
            {
                new() {
                    Burgerservicenummer = bsn,
                    Gezag = new List<Gezag.AbstractGezagsrelatie>
                    {
                        new Gezag.EenhoofdigOuderlijkGezag()
                        {
                            Minderjarige = new()
                            {
                                Burgerservicenummer = bsn
                            }
                        }
                    }
                }
            };

            var gezagPersonen = new List<GbaPersoon>
            {
                new() { Burgerservicenummer = bsn, Naam = new() { Voornamen = "voornamen", Voorvoegsel = "voorvoegsel", Geslachtsnaam = "geslachtsnaam" } }
            };

            var inputPersoon = (new GbaPersoon { Burgerservicenummer = bsn }, 1L);

            // Act
            _gezagService.VerrijkPersonenMetGezagIfRequested(fields, persoonGezagsrelaties, gezagPersonen, inputPersoon);

            // Assert
            inputPersoon.Item1.Gezag.Should().BeNull();
        }

        [Fact]
        public void VerrijkPersonenMetGezagIfRequested_VerrijktPersoonBeperktMetGezag_WhenGezagIsRequested()
        {
            // Arrange
            var fields = new List<string> { "gezag" };
            var bsn = "000000012";

            var persoonGezagsrelaties = new List<Persoon>
            {
                new() {
                    Burgerservicenummer = bsn,
                    Gezag = new List<Gezag.AbstractGezagsrelatie>
                    {
                        new Gezag.EenhoofdigOuderlijkGezag()
                        {
                            Minderjarige = new()
                            {
                                Burgerservicenummer = bsn
                            }
                        }
                    }
                }
            };

            var gezagPersonen = new List<GbaPersoon>
            {
                new() { Burgerservicenummer = bsn, Naam = new() { Voornamen = "voornamen", Voorvoegsel = "voorvoegsel", Geslachtsnaam = "geslachtsnaam" } }
            };

            var inputPersoon = (new GbaGezagPersoonBeperkt { Burgerservicenummer = bsn }, 1L);

            var expectedGezag = new List<Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated.AbstractGezagsrelatie>
            {
                new Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated.EenhoofdigOuderlijkGezag()
                {
                    Minderjarige = new()
                    {
                        Burgerservicenummer = bsn,
                        Naam = new()
                        {
                            Voornamen = "voornamen",
                            Voorvoegsel = "voorvoegsel",
                            Geslachtsnaam = "geslachtsnaam"
                        }
                    }
                }
            };

            // Act
            _gezagService.VerrijkPersonenMetGezagIfRequested(fields, persoonGezagsrelaties, gezagPersonen, inputPersoon);

            // Assert
            inputPersoon.Item1.Gezag.Should().BeEquivalentTo(expectedGezag);
        }

        [Fact]
        public void VerrijkPersonenMetGezagIfRequested_VerrijktPersoonBeperktMetGezagRange_WhenGezagIsRequested()
        {
            // Arrange
            var fields = new List<string> { "gezag" };
            var bsnMinderjarige = "000000013";
            var bsnOuder = "000000012";

            var persoonGezagsrelaties = new List<Persoon>
            {
                new() {
                    Burgerservicenummer = bsnOuder,
                    Gezag = new List<Gezag.AbstractGezagsrelatie>
                    {
                        new Gezag.EenhoofdigOuderlijkGezag()
                        {
                            Minderjarige = new()
                            {
                                Burgerservicenummer = bsnMinderjarige
                            },
                            Ouder = new()
                            {
                                Burgerservicenummer = bsnOuder
                            }
                        }
                    }
                }
            };

            var gezagPersonen = new List<GbaPersoon>
            {
                new() { Burgerservicenummer = bsnOuder, Naam = new() { Voornamen = "voornamen", Voorvoegsel = "voorvoegsel", Geslachtsnaam = "geslachtsnaam" } },
                new() { Burgerservicenummer = bsnMinderjarige, Naam = new() { Voornamen = "voornamen", Voorvoegsel = "voorvoegsel", Geslachtsnaam = "geslachtsnaam" } }
            };

            var inputPersoon = (new GbaGezagPersoonBeperkt { Burgerservicenummer = bsnOuder }, 1L);

            var expectedGezag = new List<Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated.AbstractGezagsrelatie>
            {
                new Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated.EenhoofdigOuderlijkGezag()
                {
                    Minderjarige = new()
                    {
                        Burgerservicenummer = bsnMinderjarige,
                        Naam = new()
                        {
                            Voornamen = "voornamen",
                            Voorvoegsel = "voorvoegsel",
                            Geslachtsnaam = "geslachtsnaam"
                        }
                    },
                    Ouder = new()
                    {
                        Burgerservicenummer = bsnOuder,
                         Naam = new()
                        {
                            Voornamen = "voornamen",
                            Voorvoegsel = "voorvoegsel",
                            Geslachtsnaam = "geslachtsnaam"
                        }
                    }
                }
            };

            // Act
            _gezagService.VerrijkPersonenMetGezagIfRequested(fields, persoonGezagsrelaties, gezagPersonen, inputPersoon);

            // Assert
            inputPersoon.Item1.Gezag.Should().BeEquivalentTo(expectedGezag);
        }

        [Fact]
        public void VerrijkPersonenMetGezagIfRequested_VerrijktPersoonBeperktNietMetGezag_WhenGezagIsNotRequested()
        {
            // Arrange
            var fields = new List<string> { "adressering" };
            var bsn = "000000012";

            var persoonGezagsrelaties = new List<Persoon>
            {
                new() {
                    Burgerservicenummer = bsn,
                    Gezag = new List<Gezag.AbstractGezagsrelatie>
                    {
                        new Gezag.EenhoofdigOuderlijkGezag()
                        {
                            Minderjarige = new()
                            {
                                Burgerservicenummer = bsn
                            }
                        }
                    }
                }
            };

            var gezagPersonen = new List<GbaPersoon>
            {
                new() { Burgerservicenummer = bsn, Naam = new() { Voornamen = "voornamen", Voorvoegsel = "voorvoegsel", Geslachtsnaam = "geslachtsnaam" } }
            };

            var inputPersoon = (new GbaGezagPersoonBeperkt { Burgerservicenummer = bsn }, 1L);

            // Act
            _gezagService.VerrijkPersonenMetGezagIfRequested(fields, persoonGezagsrelaties, gezagPersonen, inputPersoon);

            // Assert
            inputPersoon.Item1.Gezag.Should().BeNull();
        }
    }
}
