using Mediscreen.Shared.Entities;

namespace Mediscreen.AssessmentAPI.Repositories
{
    public interface ITriggerTermsRepository
    {
        public Task<List<TriggerTerm>> GetAsync();
        public Task CreateAsync(TriggerTerm newTriggerTerm);
    }
}
