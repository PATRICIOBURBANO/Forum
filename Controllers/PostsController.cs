using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    public class PostsController : Controller
    {
        public ActionResult Create()
        {
            return View();
        }
    }
}
