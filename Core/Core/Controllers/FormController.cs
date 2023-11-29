using Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Core.Controllers;

[AutoValidateAntiforgeryToken]
public class FormController : Controller
{
    private readonly DataContext _dataContext;

    public FormController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(long id = 1)
    {
        ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "Name");
        return View(await _dataContext.Products.Include(p => p.Category).FirstAsync(p => p.Id == id));
    }

    [HttpPost]
    //[IgnoreAntiforgeryToken]
    public  IActionResult SubmitForm()
    {
        foreach (string key in Request.Form.Keys)
        {
            TempData[key] = string.Join(",", Request.Form[key]);
        }

        return RedirectToAction("Results");
    }

    public IActionResult Results()
    {
        return View();
    }
}
