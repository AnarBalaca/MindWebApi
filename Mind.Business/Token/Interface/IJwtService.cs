using Mind.Entity.Identity;

namespace Mind.Business.Token.Interface;

public interface IJwtService
{
    public string GetJwt(AppUser user, IList<string> roles);
}
