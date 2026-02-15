namespace BookSmartBackEnd.Models.POST;

public class PostScheduleOverrideModel
{
    public required Guid UserId { get; set; }
    public required DateOnly Date { get; set; }
    public required bool IsAvailable { get; set; }
    public TimeOnly? StartTime { get; set; }
    public TimeOnly? EndTime { get; set; }
}
