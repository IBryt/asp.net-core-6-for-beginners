using Core.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers;

public class AccountController : Controller
{

    private readonly SignInManager<IdentityUser> _singInManager;
    private UserManager<IdentityUser> _userManager;
    public AccountController(SignInManager<IdentityUser> singInManager, UserManager<IdentityUser> userManager)
    {
        _singInManager = singInManager;
        _userManager = userManager;
    }

    public IActionResult Login(string? returnUrl) => View(new LoginViewModel { ReturnUrl = returnUrl});

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _singInManager.PasswordSignInAsync(
            model.UserName,
            model.Password,
            false,
            false);

        if (result.Succeeded)
        {
            return Redirect(model.ReturnUrl ?? "/");
        }

        ModelState.AddModelError("", "Invalid username or password");

        return View(model);
    }

    //public IActionResult Details() => View(new AuthDetailsViewModel 
    //{ 
    //    Cookie = Request.Cookies[".AspNetCore.Identity.Application"] 
    //});

    public async Task<IActionResult> Details()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            IdentityUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            return View(new AuthDetailsViewModel { Cookie = Request.Cookies[".AspNetCore.Identity.Application"], User = user });
        }

        return View(new AuthDetailsViewModel());
    }

    public async Task<RedirectResult> Logout(string returnUrl = "/")
    {
        await _singInManager.SignOutAsync();

        return Redirect(returnUrl);
    }

    [Authorize]
    public string AllRoles() => "AllRoles";

    [Authorize(Roles="Admin")]
    public string AdminOnly() => "AdminOnly";

    [Authorize(Roles = "Manager")]
    public string ManagerOnly() => "ManagerOnly";

    public string AccessDenied() => "AccessDenied";
}
