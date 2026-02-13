namespace BookSmartBackEnd.Models.GET;

public class ScheduleResponse
{
    public required Guid ScheduleId { get; set; }
    public required Guid UserId { get; set; }
    public required int DayOfWeek { get; set; }
    public required TimeOnly StartTime { get; set; }
    public required TimeOnly EndTime { get; set; }
    public required bool Active { get; set; }
}