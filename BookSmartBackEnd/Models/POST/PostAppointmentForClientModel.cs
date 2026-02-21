namespace BookSmartBackEnd.Models.POST;

public class PostAppointmentForClientModel
{
    public required Guid ClientUserId { get; set; }
    public required Guid ServiceId { get; set; }
    public Guid? ScheduleId { get; set; }
    public Guid? ScheduleOverrideId { get; set; }
    public required DateTime RequestedStartTime { get; set; }
    public string? Comment { get; set; }
}
