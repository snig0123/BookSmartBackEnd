namespace BookSmartBackEnd.Models.GET;

public class LoginResult
{
    public required string Token { get; init; }
    public required UserProfile Profile { get; init; }
}