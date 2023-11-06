using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Mediscreen.Shared.Entities
{
    [BsonIgnoreExtraElements]
    public class Patient
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [Required]
        public string GivenName { get; set; } = null!;
        [Required]
        public string FamilyName { get; set; } = null!;
        public DateTime _dateOfBirth { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [Required]
        public DateTime DateOfBirth
        {
            get => _dateOfBirth.ToLocalTime();
            set => _dateOfBirth = value.ToUniversalTime();
        }
        [Required]
        public string Sex { get; set; } = null!;
        public string? HomeAddress { get; set; } = null!;
        public string? PhoneNumber { get; set; } = null!;
    }
}
