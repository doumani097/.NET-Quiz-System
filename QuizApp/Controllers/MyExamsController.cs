using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp_Data;
using QuizApp_Model.ViewModels;
using System.Linq;
using System.Security.Claims;

namespace QuizApp.Controllers
{
    public class MyExamsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MyExamsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var UserExams = _db.UserExams.Where(u => u.UserId == claim.Value).Include(u => u.Exam);
            return View(UserExams);
        }

        public IActionResult ExamDetails(int? id, int? eid)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

         
            var UserAnswers = _db.UserAnswers.Where(ua => ua.UserExamId == id).Include(ua => ua.Answer);


            var ExamDetails = _db.Exams.Include(e => e.Questions).FirstOrDefault(e => e.Id == eid);
            foreach (var question in ExamDetails.Questions)
            {
                question.Answers = _db.Answers.Where(a => a.QuestionId == question.Id).ToList();
            }

            var myExamDetailsVM = new MyExamDetailsVM
            {
                Exam = ExamDetails,
                UserAnswers = UserAnswers
            };

            return View(myExamDetailsVM);
        }
    }
}
