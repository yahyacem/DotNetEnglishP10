using Mediscreen.Shared.Entities;

namespace Mediscreen.AssessmentAPI.Services
{
    public interface IPatientsService
    {
        public Task<Patient?> GetAsync(string id);
        public Task<bool> ExistsAsync(string id);
    }
}
