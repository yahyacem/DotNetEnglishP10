using Mediscreen.Shared.Entities;

namespace Mediscreen.HistoryAPI.Repositories
{
    public interface IHistoryRepository
    {
        public Task<List<Note>> GetAsync(string id);
        public Task CreateAsync(Note newNote);
    }
}
