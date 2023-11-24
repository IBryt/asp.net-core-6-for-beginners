using Core.Infrastructure;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Core.Components;

public class ProductListingViewComponent : ViewComponent
{
    private DataContext _dataContext;

    public IEnumerable<Product> Products;

    public ProductListingViewComponent(DataContext context)
    {
        _dataContext = context;
    }

    public string Invoke()
    {
        return $"There are {_dataContext.Products.Count()} products";
    }
}