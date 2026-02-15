namespace BookSmartBackEnd.Models.POST;

public class PostScheduleModel
{
    public required Guid StaffUserId { get; set; }
    public required int DayOfWeek { get; set; }
    public required TimeOnly StartTime { get; set; }
    public required TimeOnly EndTime { get; set; }
}