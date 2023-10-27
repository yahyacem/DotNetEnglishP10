using Mediscreen.HistoryAPI.Repositories;

namespace Mediscreen.HistoryAPI.Services
{
    public class PatientsService : IPatientsService
    {
        private readonly IPatientsRepository _patientsRepository;
        public PatientsService(IPatientsRepository patientsRepository)
        {
            _patientsRepository = patientsRepository;
        }
        public async Task<bool> PatientExistsAsync(string id) =>
            await _patientsRepository.PatientExistsAsync(id);
    }
}
