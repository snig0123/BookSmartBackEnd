namespace BookSmartBackEnd.Models;

public class Appointment
{
    public DateTime APPT_DATE { get; set; }
    public string? APPT_COMMENT { get; set; }
    //Potentially appt client/group id to show who the appointment is with
}