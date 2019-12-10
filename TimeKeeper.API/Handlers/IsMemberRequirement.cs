using Microsoft.AspNetCore.Authorization;

namespace TimeKeeper.API.Handlers
{
    public class IsMemberRequirement: IAuthorizationRequirement
    {
        public IsMemberRequirement() { }
    }
}
