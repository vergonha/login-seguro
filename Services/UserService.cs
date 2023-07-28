using Microsoft.EntityFrameworkCore;
using secure_api.Data;
using secure_api.Entities;
using secure_api.Resources;
using System.Text;

namespace secure_api.Services;

public sealed class UserService : IUserService
{
    private readonly DataContext _context;
    private readonly string _pepper;
    private readonly int _iteration = 7;


    public UserService(IConfiguration configuration, DataContext context)
    {
        _context = context;
        _pepper = configuration.GetValue<string>("Secrets:Pepper");

    }

    public async Task<UserResource> Register(RegisterResource resource, CancellationToken cancellationToken)
    {

        if (string.IsNullOrEmpty(resource.Username) || string.IsNullOrEmpty(resource.Password))
            throw new ArgumentException("Username and password cannot be empty.");

        User user = new User
        {
            Username = resource.Username,
            Email = resource.Email,
            PasswordSalt = PasswordHasher.GenerateSalt()
        };

        byte[] passwordSaltBytes = Encoding.UTF8.GetBytes(user.PasswordSalt);
        byte[] pepperBytes = Encoding.UTF8.GetBytes(_pepper);

        user.PasswordHash = Convert.ToBase64String(PasswordHasher.GenerateHashedPassword(resource.Password, passwordSaltBytes, pepperBytes, _iteration));
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new UserResource(user.Id, user.Username, user.Email);
    }

    public async Task<UserResource> Login(LoginResource resource, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Username == resource.Username, cancellationToken);

        if (user == null)
            throw new Exception("Username or password did not match.");

        bool isValidPassword = PasswordHasher.VerifyPassword(resource.Password, user.PasswordHash, user.PasswordSalt, _pepper, _iteration);
        if (!isValidPassword)
            throw new Exception("Username or password did not match.");
        
        return new UserResource(user.Id, user.Username, user.Email);
    }
}
    