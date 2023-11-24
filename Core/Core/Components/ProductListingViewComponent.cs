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

    public IViewComponentResult Invoke()
    {
        var content = "This is a <h3><i>string</i></h3>";
        //return Content(content);
        return new HtmlContentViewComponentResult(new HtmlString(content));
    }
}