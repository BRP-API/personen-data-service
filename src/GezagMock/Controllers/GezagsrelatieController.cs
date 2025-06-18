using Brp.Gezag.Mock.Generated;
using GezagMock.Repositories;
using Microsoft.AspNetCore.Mvc;

using ControllerBase = Brp.Gezag.Mock.Generated.ControllerBase;

namespace GezagMock.Controllers
{
    [ApiController]
    [Route("api/v2")]
    public class GezagsrelatieController : ControllerBase
    {
        private readonly GezagsrelatieRepository _repository;

        public GezagsrelatieController(GezagsrelatieRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("OpvragenBevoegdheidTotGezag")]
        public override async Task<ActionResult<GezagResponse>> OpvragenBevoegdheidTotGezag([FromHeader(Name = "Accept-Gezag-Version")] AcceptGezagVersion accept_Gezag_Version, [FromBody] GezagRequest body)
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
