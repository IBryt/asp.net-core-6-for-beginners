using Core.Infrastructure;
using Core.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;

namespace Core.Components;

public class ProductListingViewComponent : ViewComponent
{
    private DataContext _dataContext;

    public IEnumerable<Product> Products;

    public ProductListingViewComponent(DataContext context)
    {
        _dataContext = context;
    }

    public IViewComponentResult Invoke(string className = "primary")
    {
        ViewBag.Class = className;
        return View(_dataContext.Products.Include(p => p.Category).ToList());
    }
}