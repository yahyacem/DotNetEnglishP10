using Mediscreen.AssessmentAPI.Repositories;

namespace Mediscreen.AssessmentAPI.Services
{
    public class TriggerTermsService : ITriggerTermsService
    {
        private readonly ITriggerTermsRepository _triggerTermsRepository;
        public TriggerTermsService(ITriggerTermsRepository triggerTermsRepository)
        {
            _triggerTermsRepository = triggerTermsRepository;
        }
        public async Task CreateAsync(string term)
        {
            bool existsAlready = _triggerTermsRepository.GetAsync().Result.Any(x => x.Term.ToUpper() == term.ToUpper());
            if (!existsAlready)
            {
                await _triggerTermsRepository.CreateAsync(new()
                {
                    Term = term
                });
            }
        }
    }
}
