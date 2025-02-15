using System.ComponentModel.DataAnnotations;

namespace BookSmartBackEndDatabase.Models
{
    public class User
    {
        [Key]
        public Guid USER_ID { get; set; }
        public required string USER_TITLE { get; set; }
        public required string USER_FORENAME { get; set; }
        public required string USER_SURNAME { get; set; }
        public required string? USER_TELEPHONE { get; set; }
        public required string USER_EMAIL { get; set; }
        public required string USER_PASSWORD { get; set; }
        public required bool USER_PASSWORDEXPIRED { get; set; }
        public required bool USER_LOCKED { get; set; }
        public required DateTime? USER_LASTLOGIN { get; set; }
        public required DateTime USER_CREATED { get; set; }
        public required DateTime USER_UPDATED { get; set; }
        public required bool USER_DELETED { get; set; }
        public required Guid BUSINESS_ID { get; set; }
    }
}
