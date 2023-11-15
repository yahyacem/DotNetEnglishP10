using Mediscreen.Shared.Entities;

namespace Mediscreen.HistoryAPI.Services
{
    public interface IHistoryService
    {
        /// <summary>
        /// Get all notes of patients.
        /// </summary>
        public Task<List<Note>> GetAsync();
        /// <summary>
        /// Get all notes of a patient by Id.
        /// </summary>
        /// <param name="id">Id of patient.</param>
        public Task<List<Note>> GetAsync(string id);
        /// <summary>
        /// Create a new note record.
        /// </summary>
        /// <param name="newNote">Note to create.</param>
        public Task CreateAsync(Note newNote);
    }
}