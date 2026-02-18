using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSmartBackEndDatabase.Models
{
    public class ServiceScheduleOverride
    {
        [Key]
        public Guid SERVICESCHEDULEOVERRIDE_ID { get; set; }
        [ForeignKey("Service")]
        public required Guid SERVICESCHEDULEOVERRIDE_SERVICEID { get; set; }
        public Service SERVICESCHEDULEOVERRIDE_SERVICE { get; set; } = null!;
        [ForeignKey("ScheduleOverride")]
        public required Guid SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDEID { get; set; }
        public ScheduleOverride SERVICESCHEDULEOVERRIDE_SCHEDULEOVERRIDE { get; set; } = null!;
        public required DateTime SERVICESCHEDULEOVERRIDE_CREATED { get; set; }
        public required DateTime SERVICESCHEDULEOVERRIDE_UPDATED { get; set; }
        public required bool SERVICESCHEDULEOVERRIDE_DELETED { get; set; }
    }
}
