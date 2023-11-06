using Mediscreen.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mediscreen.Shared.Models
{
    public class AssessmentModel
    {
        public Patient Patient { get; set; } = null!;
        public List<TriggerDetectedModel> TriggersDetected { get; set; } = new List<TriggerDetectedModel>();
        [DisplayName("Risk level")]
        public string RiskLevel { get => AssessRiskLevel(); }
        private string AssessRiskLevel()
        {
            if (Patient == null)
                return "None";

            // Patient over 30
            if (DateTime.Now.Year - Patient.DateOfBirth.Year > 30)
            {
                switch (TriggersDetected.Count)
                {
                    case var _ when TriggersDetected.Count >= 8:
                        return "Early Onset";
                    case var _ when TriggersDetected.Count >= 6:
                        return "In danger";
                    case var _ when TriggersDetected.Count >= 1:
                        return "Borderline";
                    default:
                        return "None";
                }
            }

            // Patient under 30

            if (Patient.Sex == "F")
            {
                // Patient is female
                switch (TriggersDetected.Count)
                {
                    case var _ when TriggersDetected.Count >= 7:
                        return "Early Onset";
                    case var _ when TriggersDetected.Count >= 4:
                        return "In danger";
                    default:
                        return "None";
                }
            } else
            {
                // Patient is male
                switch (TriggersDetected.Count)
                {
                    case var _ when TriggersDetected.Count >= 5:
                        return "Early Onset";
                    case var _ when TriggersDetected.Count >= 3:
                        return "In danger";
                    default:
                        return "None";
                }
            }
        }
    }
}
