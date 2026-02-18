namespace BookSmartBackEnd.Models.POST;

public class PostServiceScheduleOverrideModel
{
    public required Guid ServiceId { get; set; }
    public required Guid ScheduleOverrideId { get; set; }
}
