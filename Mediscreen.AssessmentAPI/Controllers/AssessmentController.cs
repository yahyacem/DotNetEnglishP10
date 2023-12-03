using Mediscreen.AssessmentAPI.Services;
using Mediscreen.Shared.Entities;
using Mediscreen.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mediscreen.AssessmentAPI.Controllers
{
    [Authorize]
    [RequiredScopeOrAppPermission(AcceptedAppPermission = new[] { "Assessment.Admin" })]
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentController : ControllerBase
    {
        private readonly IPatientsService _patientsService;
        private readonly IAssessmentService _assessmentService;
        private readonly ITriggerTermsService _triggerTermsService;
        public AssessmentController(IPatientsService patientsService, IAssessmentService assessmentService, ITriggerTermsService triggerTermsService)
        {
            _patientsService = patientsService;
            _assessmentService = assessmentService;
            _triggerTermsService = triggerTermsService;
        }
        // GET api/<AssessmentController>/{id}
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<AssessmentModel>> Get(string id)
        {
            Patient? patient = await _patientsService.GetAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            return _assessmentService.GetAssessment(patient, await _assessmentService.CheckTriggers(patient.Id!));
        }
        // POST api/<AssessmentController>/Trigger
        //[HttpGet("Trigger/{term}")]
        //public async Task<IActionResult> Post(string term)
        //{
        //    await _triggerTermsService.CreateAsync(term);
        //    return Ok();
        //}
    }
}
