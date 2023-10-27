namespace Mediscreen.HistoryAPI.Services
{
    public interface IPatientsService
    {
        public Task<bool> PatientExistsAsync(string id);
    }
}
