using System.ComponentModel.DataAnnotations;

namespace Mediscreen.WebApp.Models
{
    public class NoteViewModel
    {
        public string? Id { get; set; }
        [Required]
        public string PatientId { get; set; } = null!;
        public DateTime? CreationDate { get; set; } = DateTime.Now;
        [DataType(DataType.MultilineText)]
        [Required]
        public string NotesRecommendations { get; set; } = null!;
    }
}
