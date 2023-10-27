using Mediscreen.Shared.Entities;
using Mediscreen.Shared.Models;
using Mediscreen.WebApp.Models;
using System.Net.Http;
using System.Text;

namespace Mediscreen.WebApp.Services
{
    public interface IApiService
    {
        public Task<List<PatientViewModel>> GetPatientsAsync();
        public Task<PatientViewModel?> GetPatientAsync(string id);
        public Task<AssessmentModel?> GetAssessmentAsync(string id);
        public Task<PatientViewModel?> CreatePatientAsync(PatientViewModel patientViewModel);
        public Task<bool> CreateNoteAsync(NoteViewModel newNote);
        public Task<bool> EditPatientAsync(PatientViewModel patientViewModel);
        public Task<bool> DeletePatientAsync(string id);
    }
}
