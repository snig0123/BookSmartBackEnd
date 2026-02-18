namespace BookSmartBackEnd.Models.POST;

public class PostServiceScheduleModel
{
    public required Guid ServiceId { get; set; }
    public required Guid ScheduleId { get; set; }
}
