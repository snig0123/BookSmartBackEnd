namespace BookSmartBackEnd.Models.POST;

public class PostServiceModel
{
    public required Guid StaffUserId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required int Duration { get; set; }
    public required decimal Price { get; set; }
    public required int Capacity { get; set; }
}