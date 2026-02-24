namespace BookSmartBackEnd.Models.GET;

public class AppointmentResponse
{
    public required Guid AppointmentId { get; set; }
    public required Guid ClientUserId { get; set; }
    public required string ClientName { get; set; }
    public required Guid StaffUserId { get; set; }
    public required Guid ServiceId { get; set; }
    public required string ServiceName { get; set; }
    public Guid? ScheduleId { get; set; }
    public Guid? ScheduleOverrideId { get; set; }
    public required DateTime StartDateTime { get; set; }
    public required DateTime EndDateTime { get; set; }
    public required string Status { get; set; }
    public string? Comment { get; set; }
    public required DateTime Created { get; set; }
}
