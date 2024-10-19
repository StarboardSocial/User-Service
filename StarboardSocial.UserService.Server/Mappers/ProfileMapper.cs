using StarboardSocial.UserService.Domain.Models;
using StarboardSocial.UserService.Server.Models;

namespace StarboardSocial.UserService.Server.Mappers;

public static class ProfileMapper
{
    public static Profile ProfileCreationModelToProfile(ProfileCreationModel creationModel, string userId)
    {
        return new Profile()
        {
            Id = Guid.Parse(userId),
            FirstName = creationModel.FirstName,
            LastName = creationModel.LastName,
            SailboatType = creationModel.SailboatType
        };
    }

    public static ProfileReturnModel ProfileToProfileReturnModel(Profile profile)
    {
        return new ProfileReturnModel()
        {
            Id = profile.Id.ToString(),
            FirstName = profile.FirstName,
            LastName = profile.LastName,
            SailboatType = profile.SailboatType
        };
    } 
}