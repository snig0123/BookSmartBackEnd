using System.ComponentModel.DataAnnotations;

namespace BookSmartBackEndDatabase.Models
{
    public class Business
    {
        [Key]
        public required Guid BUSINESS_ID { get; set; }
        public required string BUSINESS_NAME { get; set; }
        public required Address BUSINESS_ADDRESS {set; get;}
        public required DateTime BUSINESS_SUBSTART { get; set; }
        public required DateTime BUSINESS_SUBEND { get; set; }
        public required DateTime BUSINESS_CREATED { get; set; }
        public required DateTime BUSINESS_UPDATED { get; set; }
        public required bool BUSINESS_DELETED { get; set; }
    }
}
