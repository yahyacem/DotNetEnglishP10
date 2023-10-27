using Mediscreen.AssessmentAPI.Repositories;
using Mediscreen.Shared.Entities;
using Mediscreen.Shared.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Mediscreen.AssessmentAPI.Services
{
    public class PatientsService : IPatientsService
    {
        private readonly IPatientsRepository _patientsRepository;
        public PatientsService(IPatientsRepository patientsRepository)
        {
            _patientsRepository = patientsRepository;
        }
        public async Task<Patient?> GetAsync(string id) =>
            await _patientsRepository.GetAsync(id);
        public async Task<bool> ExistsAsync(string id) =>
            await _patientsRepository.PatientExistsAsync(id);
    }
}
