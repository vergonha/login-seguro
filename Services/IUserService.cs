using secure_api.Resources;

namespace secure_api.Services;

public interface IUserService
{
    Task<UserResource> Register(RegisterResource resource, CancellationToken cancellationToken);
    Task<UserResource> Login(LoginResource resource, CancellationToken cancellationToken);
}
