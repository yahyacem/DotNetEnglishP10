using Mediscreen.Shared.Entities;

namespace Mediscreen.PatientAPI.Services
{
    public interface IPatientsService
    {
        /// <returns>Returns a list of patients from the database.</returns>
        public Task<List<Patient>> GetAsync();
        /// <returns>Returns a patient record with the specified id from the database.</returns>
        public Task<Patient?> GetAsync(string id);
        /// <summary>Insert a new patient record to the database.</summary>
        /// <returns>Returns the created patient record.</returns>
        public Task CreateAsync(Patient newPatient);
        /// <summary>Updates a patient record in the database.</summary>
        /// <returns>Returns the updated patient record.</returns>
        public Task UpdateAsync(string id, Patient updatedPatient);
        /// <summary>Removes a patient record from the database.</summary>
        public Task RemoveAsync(string id);
    }
}
