using Mediscreen.Shared.Entities;
using Mediscreen.Shared.Models;

namespace Mediscreen.AssessmentAPI.Services
{
    public interface IAssessmentService
    {
        public Task<List<TriggerDetectedModel>> CheckTriggers(string patientId);
        public AssessmentModel GetAssessment(Patient patient, List<TriggerDetectedModel> triggers);
    }
}
