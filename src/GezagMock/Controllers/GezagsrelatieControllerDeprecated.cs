using Brp.Gezag.Mock.Generated.Deprecated;
using GezagMock.Repositories;
using Microsoft.AspNetCore.Mvc;

using ControllerBase = Brp.Gezag.Mock.Generated.Deprecated.ControllerBase;

namespace GezagMock.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class GezagsrelatieControllerDeprecated : ControllerBase
    {
        private readonly GezagsrelatieRepositoryDeprecated _repository;

        public GezagsrelatieControllerDeprecated(GezagsrelatieRepositoryDeprecated repository)
        {
            _repository = repository;
        }

        [HttpPost("OpvragenBevoegdheidTotGezag")]
        public override async Task<ActionResult<GezagResponse>> OpvragenBevoegdheidTotGezag([FromHeader] string? oIN, [FromBody] GezagRequest body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var retval = new GezagResponse
            {
                Personen = new List<Persoon>()
            };

            foreach (var bsn in body.Burgerservicenummer)
            {
                var p = await _repository.Zoek(bsn);
                if (p != null && p.Any())
                {
                    retval.Personen.AddRange(p);
                }
            }

            return Ok(retval);
        }
    }
}
