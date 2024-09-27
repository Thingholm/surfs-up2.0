namespace Web.Controllers;
using Microsoft.AspNetCore.Mvc;

public class ProductsController : Controller
{
    [Route("/produkter")]
    public IActionResult Index()
    {
        return View();
    }
}