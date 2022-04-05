using Forum.Data;
using Forum.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Forum.Controllers
{
    public class ForumController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public ForumController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _db = context;

        }
        // GET: ForumController
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult AddTag()
        {
            ViewBag.Tags = new SelectList(_db.Tag.ToList(), "Id", "Name");
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> AddTag(int tagId)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            Tag tag = _db.Tag.First(u => u.Id == tagId);

            UserTag newUserTag = new UserTag { Tag = tag, TagId = tag.Id, User = user, UserId = user.Id };
            tag.UserTags.Add(newUserTag);
            user.UserTags.Add(newUserTag);

            await _userManager.UpdateAsync(user);

            return RedirectToAction("MyTags");
        }

        public IActionResult MyTags()
        {
            ApplicationUser user = _db.Users.Include(t => t.UserTags).ThenInclude(s => s.TagId).First(t => t.Email == User.Identity.Name);

            return View(user);
        }


        // GET: ForumController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ForumController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ForumController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ForumController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ForumController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ForumController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ForumController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
