using Microsoft.Extensions.Options;
using NSubstitute;
using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Personen.ApiModels.Gezag;
using Rvig.HaalCentraalApi.Personen.Interfaces;
using Rvig.HaalCentraalApi.Personen.Repositories;
using Rvig.HaalCentraalApi.Personen.Services;
using Rvig.HaalCentraalApi.Shared.Interfaces;
using Rvig.HaalCentraalApi.Shared.Options;
using Gezag = Rvig.HaalCentraalApi.Personen.ApiModels.Gezag;

namespace Personen.Tests
{

    public class GezagServiceTests
    {
        private readonly GezagService _gezagService;
        private readonly IRepoGezagsrelatie _gezagRepositoryMock;
        private readonly IGezagPersonenService _personenServiceMock;
        private readonly IOptions<ProtocolleringAuthorizationOptions> _optionsMock;

        public GezagServiceTests()
        {
            _personenServiceMock = Substitute.For<IGezagPersonenService>();
            _gezagRepositoryMock = Substitute.For<IRepoGezagsrelatie>();
            _optionsMock = Substitute.For<IOptions<ProtocolleringAuthorizationOptions>>();
            _optionsMock.Value.Returns(new ProtocolleringAuthorizationOptions { UseAuthorizationChecks = false });
            _gezagService = new GezagService(_personenServiceMock, null, null, null, _gezagRepositoryMock, _optionsMock);
        }

        [Fact]
        public async Task GetGezagIfRequested_ReturnsEmpty_WhenGezagIsNotRequested()
        {
            // Arrange
            var fields = new List<string> { "adressering" };
            var bsns = new List<string?> { "000000012", "000000013" };

            // Act
            var result = await _gezagService.GetGezagIfRequested(fields, bsns);

            // Assert
            result.Count().Should().Be(0);
        }

        [Fact]
        public async Task GetGezagIfRequested_ReturnsGezag_WhenGezagIsRequested()
        {
            // Arrange
            var fields = new List<string> { "gezag" };
            var bsns = new List<string?> { "000000012", "000000013" };

            var mockGezagResponse = new GezagResponse
            {
                Personen = new List<Persoon>
                {
                    new() { Burgerservicenummer = "000000012", Gezag = new List<Gezag.AbstractGezagsrelatie>() },
                    new() { Burgerservicenummer = "000000013", Gezag = new List<Gezag.AbstractGezagsrelatie>() }
                }
            };

            _gezagRepositoryMock
                .GetGezag(bsns!)
                .Returns(mockGezagResponse);

            // Act
            var result = await _gezagService.GetGezagIfRequested(fields, bsns);

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

            var mockGezagPersonen = new List<GbaPersoon>()
            {
                new GbaPersoon() { Burgerservicenummer = bsnMinderjarige },
                new GbaPersoon() { Burgerservicenummer = bsnOuder }
            };


            var fieldsPersonen = new List<string>() { "naam", "geslacht", "geboorte.datum" };

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

            var mockGezagPersonen = new List<GbaPersoon>()
            {
                new GbaPersoon() { Burgerservicenummer = bsnMinderjarige },
                new GbaPersoon() { Burgerservicenummer = bsnOuder }
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

            var expectedGezag = new List<Rvig.HaalCentraalApi.Personen.ApiModels.BRP.AbstractGezagsrelatie>
            {
                new Rvig.HaalCentraalApi.Personen.ApiModels.BRP.EenhoofdigOuderlijkGezag()
                {
                    Minderjarige = new Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Minderjarige()
                    {
                        Burgerservicenummer = bsn,
                        Naam = new Rvig.HaalCentraalApi.Shared.ApiModels.PersonenHistorieBase.GbaNaamBasis()
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

            var expectedGezag = new List<Rvig.HaalCentraalApi.Personen.ApiModels.BRP.AbstractGezagsrelatie>
            {
                new Rvig.HaalCentraalApi.Personen.ApiModels.BRP.EenhoofdigOuderlijkGezag()
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
