using GainsTrackerAPI.Db;
using GainsTrackerAPI.Security.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GainsTrackerAPI.Gains.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class GainsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;

    public GainsController(UserManager<User> userManager, AppDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        List<User> eggs = await _context.Users.ToListAsync();
        return Ok(eggs);
    }

    #region private methods

    private async Task<User> FindUserByUserName(string userName)
    {
        if (userName.Contains('@'))
            return await _userManager.FindByEmailAsync(userName);

        return await _userManager.FindByNameAsync(userName);
    }

    #endregion
}
