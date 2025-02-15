using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSmartBackEndDatabase.Models
{
    public class Role
    {
        [Key]
        public required Guid ROLE_ID { get; set; }
        [ForeignKey("User")]
        public required Guid ROLE_USERID { get; set; }
        [ForeignKey("RoleType")]
        public required Guid ROLE_ROLETYPEID { get; set; }
        public virtual RoleType ROLE_ROLETYPE { get; set; }
    }
}
