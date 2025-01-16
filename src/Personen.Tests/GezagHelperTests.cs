using Rvig.HaalCentraalApi.Personen.Services;
using Gezag = Rvig.HaalCentraalApi.Personen.ApiModels.Gezag;

namespace Personen.Tests
{
    public class GezagHelperTests
    {
        [Fact]
        public void GetGezagBsns_ShouldReturnDistinctBsns_WhenValidGezagsrelatiesAreProvided()
        {
            // Arrange
            var gezagsrelaties = new List<Gezag.AbstractGezagsrelatie>
            {
                new Gezag.TweehoofdigOuderlijkGezag
                {
                    Ouders = new List<Gezag.GezagOuder>
                    {
                        new() { Burgerservicenummer = "1" },
                        new() { Burgerservicenummer = "2" }
                    },
                    Minderjarige = new Gezag.Minderjarige { Burgerservicenummer = "3" }
                },
                new Gezag.EenhoofdigOuderlijkGezag
                {
                    Ouder = new Gezag.GezagOuder { Burgerservicenummer = "4" },
                    Minderjarige = new Gezag.Minderjarige { Burgerservicenummer = "5" }
                },
                new Gezag.GezamenlijkGezag
                {
                    Ouder = new Gezag.GezagOuder { Burgerservicenummer = "6" },
                    Derde = new Gezag.Meerderjarige { Burgerservicenummer = "7" },
                    Minderjarige = new Gezag.Minderjarige { Burgerservicenummer = "8" }
                },
                new Gezag.Voogdij
                {
                    Derden = new List<Gezag.Meerderjarige>
                    {
                        new() { Burgerservicenummer = "9" },
                        new() { Burgerservicenummer = "10" }
                    },
                    Minderjarige = new Gezag.Minderjarige { Burgerservicenummer = "11" }
                },
                new Gezag.GezagNietTeBepalen
                {
                    Minderjarige = new Gezag.Minderjarige { Burgerservicenummer = "12" }
                },
                new Gezag.TijdelijkGeenGezag
                {
                    Minderjarige = new Gezag.Minderjarige { Burgerservicenummer = "13" }
                }
            };

            // Act
            var result = GezagHelper.GetGezagBsns(gezagsrelaties);

            // Assert
            result.Should().BeEquivalentTo(new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13" });
        }

        [Fact]
        public void GetGezagBsns_ShouldReturnEmptyList_WhenNoGezagsrelatiesAreProvided()
        {
            // Arrange
            var gezagsrelaties = new List<Gezag.AbstractGezagsrelatie>();

            // Act
            var result = GezagHelper.GetGezagBsns(gezagsrelaties);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void GetGezagBsns_ShouldHandleNullMinderjarige()
        {
            // Arrange
            var gezagsrelaties = new List<Gezag.AbstractGezagsrelatie>
        {
            new Gezag.GezagNietTeBepalen { Minderjarige = null },
            new Gezag.TijdelijkGeenGezag { Minderjarige = null }
        };

            // Act
            var result = GezagHelper.GetGezagBsns(gezagsrelaties);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void GetGezagBsns_ShouldIncludeBsnsFromVoogdijAndGezamenlijkGezag()
        {
            // Arrange
            var gezagsrelaties = new List<Gezag.AbstractGezagsrelatie>
        {
            new Gezag.Voogdij
            {
                Derden = new List<Gezag.Meerderjarige>
                {
                    new() { Burgerservicenummer = "111" },
                    new() { Burgerservicenummer = "222" }
                },
                Minderjarige = new Gezag.Minderjarige { Burgerservicenummer = "333" }
            },
            new Gezag.GezamenlijkGezag
            {
                Derde = new Gezag.Meerderjarige { Burgerservicenummer = "444" },
                Ouder = new Gezag.GezagOuder { Burgerservicenummer = "555" },
                Minderjarige = new Gezag.Minderjarige { Burgerservicenummer = "666" }
            }
        };

            // Act
            var result = GezagHelper.GetGezagBsns(gezagsrelaties);

            // Assert
            result.Should().BeEquivalentTo(new List<string> { "111", "222", "333", "444", "555", "666" });
        }

        [Fact]
        public void GetGezagBsns_ShouldIgnoreDuplicateBsns()
        {
            // Arrange
            var gezagsrelaties = new List<Gezag.AbstractGezagsrelatie>
        {
            new Gezag.EenhoofdigOuderlijkGezag
            {
                Ouder = new Gezag.GezagOuder { Burgerservicenummer = "123" },
                Minderjarige = new Gezag.Minderjarige { Burgerservicenummer = "123" }
            },
            new Gezag.TijdelijkGeenGezag
            {
                Minderjarige = new Gezag.Minderjarige { Burgerservicenummer = "123" }
            }
        };

            // Act
            var result = GezagHelper.GetGezagBsns(gezagsrelaties);

            // Assert
            result.Should().BeEquivalentTo(new List<string> { "123" });
        }
    }
}
