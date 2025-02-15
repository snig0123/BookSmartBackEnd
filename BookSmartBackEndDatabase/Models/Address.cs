using System.ComponentModel.DataAnnotations;

namespace BookSmartBackEndDatabase.Models
{
    public class Address
    {
        [Key]
        public required Guid ADDRESS_ID { get; set; }
        public required string ADDRESS_LINE_1 { get; set; }
        public string? ADDRESS_LINE_2 { get; set; }
        public required string ADDRESS_TOWN_CITY { get; set; }
        public required string ADDRESS_POSTCODE_1 { get; set; }
        public required string ADDRESS_POSTCODE_2 { get; set; }
        public required DateTime ADDRESS_CREATED { get; set; }
        public required bool ADDRESS_DELETED { get; set; }
    }
}
