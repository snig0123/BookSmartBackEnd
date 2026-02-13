namespace BookSmartBackEnd.Models.GET;

public class UserProfile
{
    public required string Forename { get; set; }
    public required string Surname { get; set; }
    public required List<string> Roles { get; set; }
}
