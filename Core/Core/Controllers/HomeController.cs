using Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.Controllers;

public class HomeController : Controller
{
    private readonly DataContext _dataContext;

    public HomeController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<IActionResult> Index(long id)
    {
        return View(await _dataContext.Products.FindAsync(id));
    }

    public IActionResult Common(long id)
    {
        return View("/Views/Shared/Common.cshtml");
    }

    public async Task<IActionResult> List()
    {
        ViewBag.AveragePrice = await _dataContext.Products.AverageAsync(p => p.Price);

        return View(await _dataContext.Products.ToListAsync());
    }
}
