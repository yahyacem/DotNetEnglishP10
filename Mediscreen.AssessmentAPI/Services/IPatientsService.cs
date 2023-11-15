using Mediscreen.Shared.Entities;

namespace Mediscreen.AssessmentAPI.Services
{
    public interface IPatientsService
    {
        /// <summary>
        /// Get a patient record by Id.
        /// </summary>
        /// <param name="id">Id of the patient.</param>
        public Task<Patient?> GetAsync(string id);
        /// <summary>
        /// Check if a patient exists.
        /// </summary>
        /// <param name="id">Id of the patient.</param>
        public Task<bool> ExistsAsync(string id);
    }
}
