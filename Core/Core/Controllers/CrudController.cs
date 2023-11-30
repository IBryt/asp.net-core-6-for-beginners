using Core.Infrastructure;
using Core.Models.ViewModels;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.Controllers;

public class CrudController : Controller
{

    private readonly DataContext _dataContext;

    public CrudController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public IActionResult Index() => View(_dataContext.Products.Include(p => p.Category));

    public async Task<IActionResult> Details(long id)
    {
        var product = await _dataContext.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        var model = ViewModelFactory.Details(product);
        return View("ProductEditor", model);
    }

    public IActionResult Create() =>
        View("ProductEditor", ViewModelFactory.Create(new Product(), _dataContext.Categories));

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] Product product)
    {
        if (!ModelState.IsValid)
        {
            return View("ProductEditor", ViewModelFactory.Create(product, _dataContext.Categories));
        }

        _dataContext.Products.Add(product);
        await _dataContext.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(long id)
    {
        var product = await _dataContext.Products.FindAsync(id);
        if (product == null) 
        {
            View("ProductEditor", ViewModelFactory.Create(new Product(), _dataContext.Categories));
        }
        var model = ViewModelFactory.Edit(product, _dataContext.Categories);
        return View("ProductEditor", model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromForm] Product product)
    {
        if (!ModelState.IsValid)
        {
            return View("ProductEditor", ViewModelFactory.Edit(product, _dataContext.Categories));
        }

        _dataContext.Products.Update(product);
        await _dataContext.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(Product product)
    {
        _dataContext.Products.Remove(product);
        await _dataContext.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}
