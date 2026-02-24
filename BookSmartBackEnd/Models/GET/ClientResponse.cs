namespace BookSmartBackEnd.Models.GET;

public class ClientResponse
{
    public required Guid ClientId { get; set; }
    public required string Forename { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public required string? Telephone { get; set; }
}
