using Mediscreen.Shared.Entities;
using Mediscreen.Shared.Models;
using Mediscreen.WebApp.Models;
using System.Net.Http;
using System.Text;

namespace Mediscreen.WebApp.Services
{
    public interface IApiService
    {
        /// <summary>
        /// Get list of all patients from API.
        /// </summary>
        public Task<List<PatientViewModel>> GetPatientsAsync();
        /// <summary>
        /// Get a patient details by Id.
        /// </summary>
        /// <param name="id">Id of the patient record.</param>
        public Task<PatientViewModel?> GetPatientAsync(string id);
        /// <summary>
        /// Get the assessment of a patient.
        /// </summary>
        /// <param name="id">Id of the patient record.</param>
        public Task<AssessmentModel?> GetAssessmentAsync(string id);
        /// <summary>
        /// Create a new patient.
        /// </summary>
        /// <param name="patientViewModel">Patient to create.</param>
        public Task<PatientViewModel?> CreatePatientAsync(PatientViewModel patientViewModel);
        /// <summary>
        /// Create a new note for a patient.
        /// </summary>
        /// <param name="newNote">Note to create.</param>
        public Task<bool> CreateNoteAsync(NoteViewModel newNote);
        /// <summary>
        /// Edit a patient record.
        /// </summary>
        /// <param name="patientViewModel">Patient updated.</param>
        public Task<bool> EditPatientAsync(PatientViewModel patientViewModel);
        /// <summary>
        /// Delete a patient record.
        /// </summary>
        /// <param name="id">Patient to delete.</param>
        public Task<bool> DeletePatientAsync(string id);
    }
}
