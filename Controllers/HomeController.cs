using Forum.Data;
using Forum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ForumPagination;

namespace Forum.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _db = context;

        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(

            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["AnswerSortParm"] = sortOrder == "Answer" ? "Answer_desc" : "Answer";


            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var questions = from s in _db.Question.Include(c => c.Answers)
                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                questions = _db.Question.Include(c => c.Answers).Where(s => s.TitleQuestion.Contains(searchString) || s.BodyQuestion.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Date":
                    questions = questions.OrderBy(s => s.DateQuestion);
                    break;
                case "date_desc":
                    questions = questions.OrderByDescending(s => s.DateQuestion);
                    break;
                case "Answer_desc":
                    questions = questions.OrderByDescending(s => s.Answers.Count);
                    break;
                case "Answer":
                    questions = questions.OrderBy(s => s.Answers.Count);
                    break;

                default:
                    questions = questions.OrderBy(s => s.DateQuestion);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<Question>.CreateAsync(questions.AsNoTracking(), pageNumber ?? 1, pageSize));

        }

        [Authorize(Roles = "Admin")]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string newRoleName)
        {
            await _roleManager.CreateAsync(new IdentityRole(newRoleName));

            _db.SaveChanges();
            string currentUserName = User.Identity.Name;
            var getuserTask = _userManager.FindByNameAsync(currentUserName);
            ApplicationUser awaitedUser = await getuserTask;
            if (await _roleManager.RoleExistsAsync(newRoleName))
            {
                if (!await _userManager.IsInRoleAsync(awaitedUser, newRoleName))
                {
                    await _userManager.AddToRoleAsync(awaitedUser, newRoleName);
                    _db.SaveChanges();
                }
            }

            return View();
        }

        public IActionResult NewQuestion(string questionTag)
        {

            ViewBag.TagsQuestion = new List<SelectListItem> {
                new SelectListItem("Software","Software"),
                new SelectListItem("Hardware","Hardware"),
                new SelectListItem("Network","Network")
               };
            ViewBag.Question = questionTag;
            return View();
        }
        [HttpPost]
        public IActionResult NewQuestion(string questionTitle, string questionBody, string questionTag)
        {


            string userName = User.Identity.Name;

            try
            {
                ApplicationUser user = _db.Users.First(u => u.Email == userName);
                if (user != null)
                {
                    Question newQuestion = new Question
                    {
                        UserName = user.UserName,
                        TitleQuestion = questionTitle,
                        BodyQuestion = questionBody,
                        DateQuestion = DateTime.Now,
                        User = user,
                        UserId = user.Id,
                        VoteQuestion = 0,
                        Topic = questionTag
                    };

                    _db.Question.Add(newQuestion);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return RedirectToAction("Index");
        }

        public IActionResult RecentQuestions(string? sort) //int? page)
        {
            ViewBag.Sort = new List<SelectListItem>
            {
                new SelectListItem("Order By Date", "date"),
                new SelectListItem("Order By Most Answered", "answer"),
            };
            List<Question> recentQuestions = _db.Question.Include(a => a.Answers).ToList();

            if (sort != null)
            {
                if (sort == "date")
                {
                    recentQuestions = recentQuestions.OrderByDescending(a => a.DateQuestion).ToList();

                }
                else if (sort == "answer")
                {
                    recentQuestions = recentQuestions.OrderByDescending(a => a.VoteQuestion).ToList();

                }

            }

            return View(recentQuestions);
        }

        public IActionResult NewAnswer(int questionId)
        {

            ViewBag.QuestionId = questionId;
            return View();
        }

        [HttpPost]
        public IActionResult NewAnswer(int questionId, string answerBody)
        {
            string userName = User.Identity.Name;

            try
            {
                ApplicationUser user = _db.Users.First(u => u.Email == userName);
                if (user != null)
                {
                    Answer newAnswer = new Answer
                    {
                        UserName = user.UserName,
                        QuestionId = questionId,
                        BodyAnswer = answerBody,
                        DateAnswer = DateTime.Now,
                        User = user,
                        UserId = user.Id,
                        VoteAnswer = 0,
                        IsItMostCorrect = false,
                    };
                    _db.Answer.Add(newAnswer);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return RedirectToAction("DetailsQuestion", new {questionId = questionId});
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AllQuestions()
        {
            var allQuestions = _db.Question.ToList();

            return View(allQuestions);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteQuestion(int questionId)
        {
            Question questionSelected = _db.Question.First(a => a.Id == questionId);
            var answers = _db.Answer.ToList();
            var answersRelated = _db.Answer.Where(a => a.QuestionId == questionId).ToList();

            if (answersRelated != null)
            {
                _db.Question.Remove(questionSelected);
                _db.SaveChanges();
            }
            return RedirectToAction("AllQuestions");




            return RedirectToAction("DetailsQuestion", new { questionId = questionId });
        }

        public IActionResult DetailsQuestion(int questionId)
        {
            var answerList = _db.Question.Where(b => b.Id == questionId).Include(c => c.Answers).ToList();
            return View(answerList);

        }
        public IActionResult AllQuestionsByTag(string questionTag)
        {
            var questionsByTag = _db.Question.Where(a => a.Topic == questionTag).ToList();

            return View(questionsByTag);
        }

        public IActionResult AllUsers()
        {
            string userName = User.Identity.Name;
            ApplicationUser userFromDb = _db.Users.First(u => u.UserName == userName);

            List<ApplicationUser> users = _db.Users.ToList();

            return View(users);
        }


        public IActionResult MostCorrectAnswer(int answerId, int questionId)
        {
            Answer answerSelected = _db.Answer.First(a => a.Id == answerId);

            if (answerSelected.IsItMostCorrect == true)
            {
                answerSelected.IsItMostCorrect = false;
                _db.SaveChanges();


            }
            else if (_db.Answer.Where(a => a.QuestionId == questionId).Any(b => b.IsItMostCorrect == true))
            {
                return RedirectToAction("DetailsQuestion", new { questionId = questionId });
            }
            else
            {
                answerSelected.IsItMostCorrect = true;
                _db.SaveChanges();
            }
            return RedirectToAction("DetailsQuestion", new { questionId = questionId });
        }

        public IActionResult GetVoteUp(int answerId, int questionId)
        {

            Answer answerSelected = _db.Answer.First(a => a.Id == answerId);
            //Question userToMark = _db.Question.First(a => a.UserId);

            answerSelected.VoteAnswer += 1;
            //userToMark.Reputation += 5;
          
            _db.SaveChanges();

            return RedirectToAction("DetailsQuestion", new { questionId = questionId });
        }

        public IActionResult GetVoteDown(int answerId, int questionId)
        {

            Answer answerSelected = _db.Answer.First(a => a.Id == answerId);
            ApplicationUser userToMark = answerSelected.User;


            answerSelected.VoteAnswer -= 1;
            //userToMark.Reputation -= 5;
            
            _db.SaveChanges();


            return RedirectToAction("DetailsQuestion", new { questionId = questionId });
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}