namespace StarboardSocial.UserService.Domain.Models;

public class ProfileReturnModel
{
    public required string Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? SailboatType { get; init; }
}