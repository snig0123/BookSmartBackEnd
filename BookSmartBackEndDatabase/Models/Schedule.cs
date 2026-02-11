using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSmartBackEndDatabase.Models
{
    public class Schedule
    {
        [Key]
        public Guid SCHEDULE_ID { get; set; }
        [ForeignKey("User")]
        public required Guid SCHEDULE_USERID { get; set; }
        public User SCHEDULE_USER { get; set; } = null!;
        public required int SCHEDULE_DAYOFWEEK { get; set; }
        public required TimeOnly SCHEDULE_STARTTIME { get; set; }
        public required TimeOnly SCHEDULE_ENDTIME { get; set; }
        public required bool SCHEDULE_ACTIVE { get; set; }
        public required DateTime SCHEDULE_CREATED { get; set; }
        public required DateTime SCHEDULE_UPDATED { get; set; }
        public required bool SCHEDULE_DELETED { get; set; }
    }
}
