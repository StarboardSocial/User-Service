using Microsoft.EntityFrameworkCore;
using StarboardSocial.UserService.Data.Models;

namespace StarboardSocial.UserService.Data.Database;

public class StarboardDbContext(DbContextOptions<StarboardDbContext> options): DbContext(options)
{
    public DbSet<ProfileDto> Profiles { get; set; } = null!;
}