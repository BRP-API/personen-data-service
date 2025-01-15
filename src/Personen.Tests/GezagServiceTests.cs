using Microsoft.Extensions.Options;
using Moq;
using Rvig.HaalCentraalApi.Personen.ApiModels.Gezag;
using Rvig.HaalCentraalApi.Personen.Interfaces;
using Rvig.HaalCentraalApi.Personen.Repositories;
using Rvig.HaalCentraalApi.Personen.Services;
using Rvig.HaalCentraalApi.Shared.Options;

namespace Personen.Tests
{

    public class GezagServiceTests
    {
        private readonly Mock<IRepoGezagsrelatie> _mockGezagsrelatieRepo;
        private readonly Mock<IGetAndMapGbaPersonenService> _mockGetAndMapPersoonService;
        private readonly Mock<IOptions<ProtocolleringAuthorizationOptions>> _mockProtocolleringAuthorizationOptions;

        private readonly GezagService _gezagService;

        public GezagServiceTests()
        {
            _mockGezagsrelatieRepo = new Mock<IRepoGezagsrelatie>();
            _mockGetAndMapPersoonService = new Mock<IGetAndMapGbaPersonenService>();
            _mockProtocolleringAuthorizationOptions = new Mock<IOptions<ProtocolleringAuthorizationOptions>>();

            _gezagService = new GezagService(
                _mockGetAndMapPersoonService.Object,
                null,
                null,
                null,
                _mockGezagsrelatieRepo.Object,
                _mockProtocolleringAuthorizationOptions.Object
            );
        }

        [Fact]
        public async Task GetGezagIfRequested_ReturnsGezag_WhenRequested()
        {
            // Arrange
            var fields = new List<string> { "gezag" };
            var bsns = new List<string?> { "BSN1", "BSN2" };

            var mockResponse = new GezagResponse
            {
                Personen = new List<Persoon>
            {
                new() { Burgerservicenummer = "BSN1", Gezag = new List<Rvig.HaalCentraalApi.Personen.ApiModels.Gezag.AbstractGezagsrelatie>() },
                new() { Burgerservicenummer = "BSN2", Gezag = new List<Rvig.HaalCentraalApi.Personen.ApiModels.Gezag.AbstractGezagsrelatie>() }
            }
            };

            _mockGezagsrelatieRepo.Setup(repo => repo.GetGezag(It.IsAny<List<string>>()))
                .ReturnsAsync(mockResponse);

            // Act
            var result = await _gezagService.GetGezagIfRequested(fields, bsns);

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(2);
        }

        [Fact]
        public async Task GetGezagIfRequested_ReturnsEmpty_WhenNotRequested()
        {
            // Arrange
            var fields = new List<string> { "otherField" };
            var bsns = new List<string?> { "BSN1", "BSN2" };

            _mockGezagsrelatieRepo.Setup(repo => repo.GetGezag(It.IsAny<List<string>>()))
                .ReturnsAsync(new GezagResponse());

            // Act
            var result = await _gezagService.GetGezagIfRequested(fields, bsns);

            // Assert
            result.Should().NotBeNull();
        }
    }
}
