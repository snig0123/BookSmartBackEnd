using System.ComponentModel.DataAnnotations;

namespace BookSmartBackEndDatabase.Models
{
    public class RoleType
    {
        [Key]
        public required Guid ROLETYPE_ID { get; set; }
        public required string ROLETYPE_NAME { get; set; }
        public required string ROLETYPE_DESCRIPTION { get; set; }
        public required bool ROLETYPE_LOCKED { get; set; }
    }
}
