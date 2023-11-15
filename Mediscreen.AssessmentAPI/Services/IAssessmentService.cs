using Mediscreen.Shared.Entities;
using Mediscreen.Shared.Models;

namespace Mediscreen.AssessmentAPI.Services
{
    public interface IAssessmentService
    {
        /// <summary>
        /// Check and return the trigger words detected in the notes for a patient.
        /// </summary>
        /// <param name="patientId">Id of the patient.</param>
        public Task<List<TriggerDetectedModel>> CheckTriggers(string patientId);
        /// <summary>
        /// Get assessment of a patient.
        /// </summary>
        /// <param name="patient">Patient to assess.</param>
        /// <param name="triggers">Trigger words detected in patient notes.</param>
        public AssessmentModel GetAssessment(Patient patient, List<TriggerDetectedModel> triggers);
    }
}
