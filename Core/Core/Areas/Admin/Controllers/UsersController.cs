using Core.Infrastructure;
using Core.Models.ViewModels;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Core.Areas.Admin.Controllers;


[Area("Admin")]
public class UsersController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public UsersController(UserManager<IdentityUser> userManager)
    {
        this._userManager = userManager;
    }

    public IActionResult Index() => View(_userManager.Users.ToList());
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {
        if (!ModelState.IsValid)
        {
            return View(user);
        }

        IdentityUser newUser = new IdentityUser
        {
            UserName = user.UserName,
            Email = user.Email,
        };

        IdentityResult result = await _userManager.CreateAsync(newUser);

        if (result.Succeeded)
        {
            return RedirectToAction("Index");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }

        return View(user);
    }

}
