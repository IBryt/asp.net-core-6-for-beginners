using Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
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
    }
}
