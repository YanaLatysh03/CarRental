using Microsoft.AspNetCore.Mvc;

namespace Car.Rental.UI.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Errors(int statusCode)
        {
            if (statusCode == 401)
            {
                return View("UnauthorizedUser");
            }

            return View("UnauthorizedUser");
        }
    }
}
