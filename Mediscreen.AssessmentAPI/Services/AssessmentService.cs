using Mediscreen.AssessmentAPI.Repositories;
using Mediscreen.Shared.Entities;
using Mediscreen.Shared.Models;

namespace Mediscreen.AssessmentAPI.Services
{
    public class AssessmentService : IAssessmentService
    {
        public readonly ITriggerTermsRepository _triggerTermsRepository;
        public readonly IHistoryRepository _historyRepository;
        public AssessmentService(ITriggerTermsRepository triggerTermsRepository, IHistoryRepository historyRepository)
        {
            _triggerTermsRepository = triggerTermsRepository;
            _historyRepository = historyRepository;
        }
        public async Task<List<TriggerDetectedModel>> CheckTriggers(string patientId)
        {
            List<TriggerDetectedModel> triggersDetected = new List<TriggerDetectedModel>();
            List<TriggerTerm> triggerTerms = await _triggerTermsRepository.GetAsync();
            List<Note> notes = await _historyRepository.GetAsync(patientId);

            foreach (var triggerTerm in triggerTerms)
            {
                if (notes.Any(x => x.NotesRecommendations.ToUpper().Contains(triggerTerm.Term.ToUpper())))
                {
                    if (triggersDetected.Any(x => x.TriggerDetected == triggerTerm.Term))
                    {
                        foreach (var t in triggersDetected.Where(x => x.TriggerDetected == triggerTerm.Term))
                        {
                            t.Amount++;
                        }
                    } else
                    {
                        TriggerDetectedModel triggerDetected = new();
                        triggerDetected.TriggerDetected = triggerTerm.Term;
                        foreach (var t in notes.Where(x => x.NotesRecommendations.ToUpper().Contains(triggerTerm.Term.ToUpper())))
                        {
                            triggerDetected.Amount++;
                        }
                        triggersDetected.Add(triggerDetected);
                    }
                }
            }

            return triggersDetected;
        }
        public AssessmentModel GetAssessment(Patient patient, List<TriggerDetectedModel> triggers)
        {
            return new AssessmentModel()
            {
                Patient = patient,
                TriggersDetected = triggers
            };
        }
    }
}
