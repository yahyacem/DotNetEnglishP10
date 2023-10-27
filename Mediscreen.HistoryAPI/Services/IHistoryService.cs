using Mediscreen.Shared.Entities;

namespace Mediscreen.HistoryAPI.Services
{
    public interface IHistoryService
    {
        public Task<List<Note>> GetAsync(string id);
        public Task CreateAsync(Note newNote);
    }
}