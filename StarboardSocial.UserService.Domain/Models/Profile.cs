namespace StarboardSocial.UserService.Domain.Models;

public class Profile
{
    public required Guid Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? SailboatType { get; init; }
}