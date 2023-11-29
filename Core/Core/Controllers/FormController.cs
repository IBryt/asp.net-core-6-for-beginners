using Core.Infrastructure;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

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
    //public async Task<IActionResult> Index([FromQuery]long id = 1) //http://localhost:3000/Form/index/1?id=5
    public async Task<IActionResult> Index(long id = 1)
    {
        ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "Name");
        return View(await _dataContext.Products.Include(p => p.Category).FirstAsync(p => p.Id == id));
    }

    //[HttpPost]
    ////[IgnoreAntiforgeryToken]
    //public  IActionResult SubmitForm(string name, decimal price)
    //{
    //    TempData["name param"] = name;
    //    TempData["price param"] = price.ToString();

    //    return RedirectToAction("Results");
    //}

    [HttpPost]
    //[IgnoreAntiforgeryToken]
    //public IActionResult SubmitForm([Bind("Name")]Product product)
    public IActionResult SubmitForm(Product product)
    {
        TempData["product"] = System.Text.Json.JsonSerializer.Serialize(product);
        return RedirectToAction("Results");
    }

    public IActionResult Results()
    {
        return View();
    }

    public string Header([FromHeader(Name = "Accept-Language")] string accept)
    {
        return $"Header:{accept}";
    }

    [HttpPost]
    [IgnoreAntiforgeryToken]
    public Product Body([FromBody] Product model)
    {
        return model;
    }

}
