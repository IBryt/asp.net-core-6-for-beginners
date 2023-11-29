using Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers;

public class FormController : Controller
{
    private readonly DataContext _dataContext;

    public FormController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<IActionResult> Index(long id = 1)
    { 
        return View(await _dataContext.Products.FindAsync(id));
    }

    [HttpPost]
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
