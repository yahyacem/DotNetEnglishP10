using Mediscreen.HistoryAPI.Services;
using Mediscreen.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Mediscreen.HistoryAPI.Controllers
{
    [Authorize]
    [RequiredScopeOrAppPermission(AcceptedAppPermission = new[] { "History.Admin" })]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : Controller
    {
        private readonly IHistoryService _historyService;
        private readonly IPatientsService _patientsService;
        public PatientsController(IHistoryService historyService, IPatientsService patientsService)
        {
            _historyService = historyService;
            _patientsService = patientsService;
        }
        // GET api/patients/{id}/history
        [HttpGet("{id:length(24)}/history")]
        public async Task<ActionResult<List<Note>>> Get(string id)
        {
            var patientVisits = await _historyService.GetAsync(id);

            if (patientVisits is null)
            {
                return NotFound();
            }

            return patientVisits;
        }
        // POST api/patients/{id}/history
        [HttpPost("{id}/history")]
        public async Task<IActionResult> Post(string id, [FromBody] Note newNote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _patientsService.PatientExistsAsync(newNote.PatientId))
            {
                return NotFound();
            }

            await _historyService.CreateAsync(newNote);

            return CreatedAtAction(nameof(Get), new { id = newNote.Id }, newNote);
        }
    }
}
