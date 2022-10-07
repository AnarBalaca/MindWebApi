using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mind.Business.Dto.Authentication;
using Mind.Business.Dto.User;
using Mind.Business.Token.Interface;
using Mind.Data.DAL;
using Mind.Entity.Entities.Enum;
using Mind.Entity.Identity;

namespace Mind.WebApi.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly AppDbContext _context;
    private readonly IJwtService _jwtService;

    public AuthController(UserManager<AppUser> userManager, AppDbContext context, IJwtService jwtService)
    {
        _userManager = userManager;
        _context = context;
        _jwtService = jwtService;
    }


    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] Register register)
    {
        AppUser isEmailExsist = await _userManager.FindByEmailAsync(register.Email);
        if (isEmailExsist != null)
        {
            return StatusCode(StatusCodes.Status403Forbidden, new { status = "error", message = "Email is already exisit" });
        }
        AppUser isExsist = await _userManager.FindByNameAsync(register.Username);
        if (isExsist != null)
        {
            return StatusCode(StatusCodes.Status403Forbidden, new { status = "error", message = "Username is already exisit" });
        }
        AppUser user = new AppUser
        {
            Firstname = register.Firstname,
            Lastname = register.Lastname,
            Email = register.Email,
            UserName = register.Username,
        };
        IdentityResult result = await _userManager.CreateAsync(user, register.Password);
        if (!result.Succeeded)
        {
            foreach (IdentityError error in result.Errors)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { status = error.Code, message = error.Description });
            }
        };
        await _userManager.AddToRoleAsync(user, Roles.Member.ToString());
        return Ok(new { statsu = "Success", message = "Confirmation email sent" });
    }
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] Login login)
    {
        AppUser user = await _userManager.FindByNameAsync(login.Username);
        if (user == null) return NotFound();
        if (!await _userManager.CheckPasswordAsync(user, login.Password)) return Unauthorized();
        var roles = _userManager.GetRolesAsync(user).Result;
        var jwtToken = _jwtService.GetJwt(user, roles);
        var userData = new UserData
        {
            Id = user.Id,
            Username = login.Username,
            Email = user.Email,
        };
        return Ok(new
        {
            token = jwtToken,
            user = userData,
        });
    }

    #region CreateRoles
    //[HttpPost("createroles")]
    //public async Task CreateRoles()
    //{
    //    foreach (var item in Enum.GetValues(typeof(Roles)))
    //    {
    //        if (!(await _roleManager.RoleExistsAsync(item.ToString())))
    //        {
    //            await _roleManager.CreateAsync(new IdentityRole
    //            {
    //                Name = item.ToString()
    //            });
    //        }
    //    }
    //}
    #endregion
}
