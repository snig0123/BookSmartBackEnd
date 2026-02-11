using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSmartBackEndDatabase.Models
{
    public class Service
    {
        [Key]
        public Guid SERVICE_ID { get; set; }
        public required string SERVICE_NAME { get; set; }
        public string? SERVICE_DESCRIPTION { get; set; }
        public required int SERVICE_DURATION { get; set; }
        public required decimal SERVICE_PRICE { get; set; }
        [ForeignKey("Business")]
        public required Guid SERVICE_BUSINESSID { get; set; }
        public Business SERVICE_BUSINESS { get; set; } = null!;
        public required bool SERVICE_ACTIVE { get; set; }
        public required DateTime SERVICE_CREATED { get; set; }
        public required DateTime SERVICE_UPDATED { get; set; }
        public required bool SERVICE_DELETED { get; set; }
    }
}
