using StarboardSocial.UserService.Data.Models;
using StarboardSocial.UserService.Domain.Models;

namespace StarboardSocial.UserService.Data.Mappers;

public static class ProfileMapper
{
    public static ProfileDto ProfileToDto(Profile profile)
    {
        return new ProfileDto()
        {
            Id = profile.Id.ToString(),
            FirstName = profile.FirstName,
            LastName = profile.LastName,
            SailboatType = profile.SailboatType
        };
    }

    public static Profile DtoToProfile(ProfileDto dto)
    {
        return new Profile()
        {
            Id = Guid.Parse(dto.Id),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            SailboatType = dto.SailboatType
        };
    }
}