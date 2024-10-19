namespace StarboardSocial.UserService.Server.Models;

public class ProfileCreationModel
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? SailboatType { get; init; }
}