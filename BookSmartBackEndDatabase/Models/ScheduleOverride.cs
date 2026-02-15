using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSmartBackEndDatabase.Models
{
    public class ScheduleOverride
    {
        [Key]
        public Guid SCHEDULEOVERRIDE_ID { get; set; }
        [ForeignKey("User")]
        public required Guid SCHEDULEOVERRIDE_USERID { get; set; }
        public User SCHEDULEOVERRIDE_USER { get; set; } = null!;
        public required DateOnly SCHEDULEOVERRIDE_DATE { get; set; }
        public TimeOnly? SCHEDULEOVERRIDE_STARTTIME { get; set; }
        public TimeOnly? SCHEDULEOVERRIDE_ENDTIME { get; set; }
        public required bool SCHEDULEOVERRIDE_ISAVAILABLE { get; set; }
        public required DateTime SCHEDULEOVERRIDE_CREATED { get; set; }
        public required DateTime SCHEDULEOVERRIDE_UPDATED { get; set; }
        public required bool SCHEDULEOVERRIDE_DELETED { get; set; }
    }
}
