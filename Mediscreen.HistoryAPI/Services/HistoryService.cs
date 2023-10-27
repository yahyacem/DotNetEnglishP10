using Mediscreen.HistoryAPI.Repositories;
using Mediscreen.Shared.Entities;

namespace Mediscreen.HistoryAPI.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryRepository _historyRepository;
        public HistoryService(IHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }
        public async Task<List<Note>> GetAsync(string id) =>
            await _historyRepository.GetAsync(id);
        public async Task CreateAsync(Note newNote)
        {
            newNote.Id = null;
            newNote.CreationDate = DateTime.Now;
            await _historyRepository.CreateAsync(newNote);
        }
    }
}
