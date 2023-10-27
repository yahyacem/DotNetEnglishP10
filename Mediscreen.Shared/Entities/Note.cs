using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Mediscreen.Shared.Entities
{
    public class Note
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [Required]
        public string PatientId { get; set; } = null!;
        private DateTime _creationDate { get; set; }
        public DateTime? CreationDate
        {
            get => _creationDate.ToLocalTime();
            set => _creationDate = value!.Value.ToUniversalTime();
        }
        [Required]
        public string NotesRecommendations { get; set; } = null!;
    }
}