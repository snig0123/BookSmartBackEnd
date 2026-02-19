namespace BookSmartBackEnd.Models.GET;

public class ScheduleOverrideResponse
{
    public required Guid ScheduleOverrideId { get; set; }
    public required Guid UserId { get; set; }
    public required DateOnly Date { get; set; }
    public required bool IsAvailable { get; set; }
    public TimeOnly? StartTime { get; set; }
    public TimeOnly? EndTime { get; set; }
}
