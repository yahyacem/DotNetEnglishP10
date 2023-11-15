namespace Mediscreen.HistoryAPI.Services
{
    public interface IPatientsService
    {
        /// <summary>
        /// Check if patient record with provided Id exists.
        /// </summary>
        /// <param name="id">Id of the patient.</param>
        public Task<bool> PatientExistsAsync(string id);
    }
}
