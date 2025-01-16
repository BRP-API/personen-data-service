using NSubstitute;
using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Personen.ApiModels.Gezag;
using Rvig.HaalCentraalApi.Personen.Interfaces;
using Rvig.HaalCentraalApi.Personen.Repositories;
using Rvig.HaalCentraalApi.Personen.Services;
using Gezag = Rvig.HaalCentraalApi.Personen.ApiModels.Gezag;

namespace Personen.Tests
{

    public class GezagServiceTests
    {
        private readonly GezagService _gezagService;
        private readonly IRepoGezagsrelatie _gezagRepositoryMock;
        private readonly IGetAndMapGbaPersonenService _personenServiceMock;

        public GezagServiceTests()
        {
            _personenServiceMock = Substitute.For<IGetAndMapGbaPersonenService>();
            _gezagRepositoryMock = Substitute.For<IRepoGezagsrelatie>();
            _gezagService = new GezagService(_personenServiceMock, null, null, null, _gezagRepositoryMock, null);
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
                    new() { Burgerservicenummer = "000000012", Gezag = new List<Gezag.AbstractGezagsrelatie>() }
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
    }
}
