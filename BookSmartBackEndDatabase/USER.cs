using System.ComponentModel.DataAnnotations;

namespace BookSmartBackEndDatabase
{
    public class User
    {
        [Key]
        public Guid USER_ID { get; set; }
        public required string USER_TITLE { get; set; }
        public required string USER_FORENAME { get; set; }
        public required string USER_SURNAME { get; set; }
        public required string USER_EMAIL { get; set; }
        public required string USER_PASSWORD { get; set; }
        public required string? USER_TELEPHONE { get; set; }
        public required DateTime USER_CREATED { get; set; }
        public required DateTime USER_UPDATED { get; set; }
        public required bool USER_DELETED { get; set; }
        public required Guid BUSINESS_ID { get; set; }

        //Add in a guid for tenant ID to separate businesses
    }
}
