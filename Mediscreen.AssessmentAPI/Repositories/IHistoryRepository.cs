using Mediscreen.Shared.Entities;

namespace Mediscreen.AssessmentAPI.Repositories
{
    public interface IHistoryRepository
    {
        public Task<List<Note>> GetAsync(string id);
    }
}
