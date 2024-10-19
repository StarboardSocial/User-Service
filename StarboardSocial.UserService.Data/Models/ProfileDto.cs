using System.ComponentModel.DataAnnotations;

namespace StarboardSocial.UserService.Data.Models;

public class ProfileDto
{
    [Key]
    [MaxLength(255)]
    public required string Id { get; init; }
    
    [MaxLength(255)]
    public required string FirstName { get; init; }
    
    [MaxLength(255)]
    public required string LastName { get; init; }
    
    [MaxLength(255)]
    public string? SailboatType { get; init; }
}