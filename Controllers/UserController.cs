using Microsoft.AspNetCore.Mvc;
using secure_api.Resources;
using secure_api.Services;

namespace PasswordHashExample.WebAPI.Controllers;

[ApiController]
[Route("user")] // Todas as requisições serão feitas depois de /user/
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        // Instancia a classe UserService, trazendo as funções de login e register.
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterResource resource, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _userService.Register(resource, cancellationToken);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(new { ErrorMessage = e.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginResource resource, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _userService.Login(resource, cancellationToken);
            return Ok(new { message = "Success!!"} );
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(new { ErrorMessage = e.Message });
        }
    }
}