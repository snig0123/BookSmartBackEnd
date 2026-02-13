namespace BookSmartBackEnd.Models.GET;

public class ServiceResponse
{
    public required Guid ServiceId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required int Duration { get; set; }
    public required decimal Price { get; set; }
    public required bool Active { get; set; }
}