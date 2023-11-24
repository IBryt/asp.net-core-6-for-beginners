using Microsoft.AspNetCore.Mvc;

namespace Core.Components;

[ViewComponent]
public class PageSizeViewComponent :ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    { 
        HttpClient client = new ();
        var response = await client.GetAsync("http://google.com");
        return View(response.Content.Headers.ContentLength);
    }

}
