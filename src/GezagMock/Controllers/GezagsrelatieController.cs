using Brp.Gezag.Mock.Generated;
using GezagMock.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GezagMock.Controllers
{
    [ApiController]
    public class GezagsrelatieController : Brp.Gezag.Mock.Generated.ControllerBase
    {
        private readonly GezagsrelatieRepository _repository;

        public GezagsrelatieController(GezagsrelatieRepository repository)
        {
            _repository = repository;
        }
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

            foreach(var bsn in body.Burgerservicenummer)
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
