using Mediscreen.Shared.Entities;

namespace Mediscreen.AssessmentAPI.Repositories
{
    public interface IPatientsRepository
    {
        public Task<Patient?> GetAsync(string id);
        public Task<bool> PatientExistsAsync(string id);
    }
}
