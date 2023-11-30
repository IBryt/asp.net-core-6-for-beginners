using Core.Models;
using Core.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Core.Areas.Admin.Controllers;

[Area("Admin")]
public class RolesController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public RolesController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public IActionResult Index() => View(_roleManager.Roles);
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create([MinLength(2), Required] string name)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var result = await _roleManager.CreateAsync(new IdentityRole(name));

        if (result.Succeeded)
        {
            return RedirectToAction("Index");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }

        return View();
    }


    public async Task<IActionResult> Edit(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        var members = await _userManager.GetUsersInRoleAsync(role.Name);
        var nonMembers = _userManager.Users.ToList().Except(members);

        return View(new RoleViewModel
        {
            Role = role,
            NonMembers = nonMembers,
            Members = members
        });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(RoleViewModel model)
    {
        foreach (var id in model.AddIds ?? Array.Empty<string>())
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.AddToRoleAsync(user, model.RoleName);
        }

        foreach (var id in model.DeleteIds ?? Array.Empty<string>())
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.RemoveFromRoleAsync(user, model.RoleName);
        }

        return Redirect(Request.Headers["Referer"].ToString());
    }
}
