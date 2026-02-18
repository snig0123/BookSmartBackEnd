using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSmartBackEndDatabase.Models
{
    public class ServiceSchedule
    {
        [Key]
        public Guid SERVICESCHEDULE_ID { get; set; }
        [ForeignKey("Service")]
        public required Guid SERVICESCHEDULE_SERVICEID { get; set; }
        public Service SERVICESCHEDULE_SERVICE { get; set; } = null!;
        [ForeignKey("Schedule")]
        public required Guid SERVICESCHEDULE_SCHEDULEID { get; set; }
        public Schedule SERVICESCHEDULE_SCHEDULE { get; set; } = null!;
        public required DateTime SERVICESCHEDULE_CREATED { get; set; }
        public required DateTime SERVICESCHEDULE_UPDATED { get; set; }
        public required bool SERVICESCHEDULE_DELETED { get; set; }
    }
}
