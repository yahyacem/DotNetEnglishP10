using Mediscreen.Shared.Entities;
using Mediscreen.Shared.Models;
using Mediscreen.WebApp.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mediscreen.WebApp.Models
{
    public class PatientViewModel
    {
        public string? Id { get; set; }
        [DisplayName("Given name")]
        [Required]
        public string GivenName { get; set; } = null!;
        [DisplayName("Family name")]
        [Required]
        public string FamilyName { get; set; } = null!;
        [DisplayName("Date of birth")]
        [DataType(DataType.Date)]
        [DateBeforeNow(ErrorMessage = "Date cannot be after now")]
        [Required]
        public DateTime DateOfBirth { get; set; }
        [DisplayName("Sex")]
        [StringLength(1, MinimumLength = 1,  ErrorMessage = "Sex can only be M or F")]
        [Required]
        public string Sex { get; set; } = null!;
        [DisplayName("Home address")]
        public string? HomeAddress { get; set; } = null!;
        [DisplayName("Phone number")]
        public string? PhoneNumber { get; set; } = null!;
        [DisplayName("Notes")]
        public List<Note> Notes { get; set; } = new();
        [DisplayName("Assessment")]
        public AssessmentModel? Assessment { get; set; } = null!;
    }
}
