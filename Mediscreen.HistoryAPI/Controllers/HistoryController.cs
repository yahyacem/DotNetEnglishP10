using Mediscreen.HistoryAPI.Services;
using Mediscreen.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mediscreen.HistoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : Controller
    {
        private readonly IHistoryService _historyService;
        private readonly IPatientsService _patientsService;
        public HistoryController(IHistoryService historyService, IPatientsService patientsService)
        {
            _historyService = historyService;
            _patientsService = patientsService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Note>>> Get() =>
            await _historyService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<List<Note>>> Get(string id)
        {
            var patientVisits = await _historyService.GetAsync(id);

            if (patientVisits is null)
            {
                return NotFound();
            }

            return patientVisits;
        }
        // POST api/<PatientsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Note newNote)
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
