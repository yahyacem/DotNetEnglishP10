using Mediscreen.Shared.Entities;

namespace Mediscreen.HistoryAPI.Repositories
{
    public interface IPatientsRepository
    {
        public Task<bool> PatientExistsAsync(string id);
    }
}
