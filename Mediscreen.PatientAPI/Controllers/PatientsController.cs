using Mediscreen.PatientAPI.Services;
using Mediscreen.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mediscreen.PatientAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientsService _patientsService;
        public PatientsController(IPatientsService patientsService) =>
            _patientsService = patientsService;

        // GET: api/<PatientsController>
        [HttpGet]
        public async Task<List<Patient>> Get() =>
            await _patientsService.GetAsync();
        // GET api/<PatientsController>/id
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Patient>> Get(string id)
        {
            var patient = await _patientsService.GetAsync(id);

            if (patient is null)
            {
                return NotFound();
            }

            return patient;
        }
        // POST api/<PatientsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Patient newPatient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _patientsService.CreateAsync(newPatient);

            return CreatedAtAction(nameof(Get), new { id = newPatient.Id }, newPatient);
        }
        // PUT api/<PatientsController>/id
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Patient updatedPatient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patient = await _patientsService.GetAsync(id);

            if (patient is null)
            {
                return NotFound();
            }

            updatedPatient.Id = patient.Id;
            await _patientsService.UpdateAsync(id, updatedPatient);

            return NoContent();
        }

        // DELETE api/<PatientsController>/id
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var patient = await _patientsService.GetAsync(id);

            if (patient is null)
            {
                return NotFound();
            }

            await _patientsService.RemoveAsync(id);

            return NoContent();
        }
    }
}
