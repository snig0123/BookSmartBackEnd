using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSmartBackEndDatabase.Models
{
    public class Appointment
    {
        [Key]
        public Guid APPOINTMENT_ID { get; set; }
        [ForeignKey("ClientUser")]
        public required Guid APPOINTMENT_CLIENTUSERID { get; set; }
        public User APPOINTMENT_CLIENTUSER { get; set; } = null!;
        [ForeignKey("StaffUser")]
        public required Guid APPOINTMENT_STAFFUSERID { get; set; }
        public User APPOINTMENT_STAFFUSER { get; set; } = null!;
        [ForeignKey("Service")]
        public required Guid APPOINTMENT_SERVICEID { get; set; }
        public Service APPOINTMENT_SERVICE { get; set; } = null!;
        public required DateTime APPOINTMENT_STARTDATETIME { get; set; }
        public required DateTime APPOINTMENT_ENDDATETIME { get; set; }
        public required string APPOINTMENT_STATUS { get; set; }
        public string? APPOINTMENT_COMMENT { get; set; }
        public required DateTime APPOINTMENT_CREATED { get; set; }
        public required DateTime APPOINTMENT_UPDATED { get; set; }
        public required bool APPOINTMENT_DELETED { get; set; }
    }
}
