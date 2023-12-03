using Mediscreen.AssessmentAPI.Services;
using Mediscreen.Shared.Entities;
using Mediscreen.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Mediscreen.AssessmentAPI.Controllers
{
    [Authorize]
    [RequiredScopeOrAppPermission(AcceptedAppPermission = new[] { "Assessment.Admin" })]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : Controller
    {
        private readonly IPatientsService _patientsService;
        private readonly IAssessmentService _assessmentService;
        public PatientsController(IPatientsService patientsService, IAssessmentService assessmentService)
        {
            _patientsService = patientsService;
            _assessmentService = assessmentService;
        }
        // GET api/<AssessmentController>/{id}
        [HttpGet("{id:length(24)}/assessment")]
        public async Task<ActionResult<AssessmentModel>> Get(string id)
        {
            Patient? patient = await _patientsService.GetAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            return _assessmentService.GetAssessment(patient, await _assessmentService.CheckTriggers(patient.Id!));
        }
    }
}
