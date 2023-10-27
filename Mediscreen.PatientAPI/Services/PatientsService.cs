using Mediscreen.PatientAPI.Repositories;
using Mediscreen.Shared.Entities;
using Mediscreen.Shared.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Mediscreen.PatientAPI.Services
{
    public class PatientsService : IPatientsService
    {
        private readonly IPatientsRepository _patientsRepository;
        public PatientsService(IPatientsRepository patientsRepository)
        {
            _patientsRepository = patientsRepository;
        }

        public async Task<List<Patient>> GetAsync() =>
            await _patientsRepository.GetAsync();
        public async Task<Patient?> GetAsync(string id) =>
            await _patientsRepository.GetAsync(id);
        public async Task CreateAsync(Patient newPatient)
        {
            newPatient.Id = null;
            await _patientsRepository.CreateAsync(newPatient);
        }
        public async Task UpdateAsync(string id, Patient updatedPatient) =>
            await _patientsRepository.UpdateAsync(id, updatedPatient);
        public async Task RemoveAsync(string id) =>
            await _patientsRepository.RemoveAsync(id);
    }
}
