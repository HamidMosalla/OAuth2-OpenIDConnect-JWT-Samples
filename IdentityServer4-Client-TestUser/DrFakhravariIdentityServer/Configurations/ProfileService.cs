using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace DrFakhravariIdentityServer.Configurations
{
    public class ProfileService : IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.IssuedClaims.AddRange(context.Subject.Claims);

            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.FromResult(0);
        }
    }
}