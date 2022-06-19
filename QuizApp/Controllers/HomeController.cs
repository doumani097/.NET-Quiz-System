using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp_Data;
using QuizApp_Model;
using QuizApp_Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace QuizApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var exams = _db.Exams.Include(e => e.Questions);
            return View(exams);
        }

        public IActionResult Details(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var exam = _db.Exams.Include(e => e.Questions).FirstOrDefault(e => e.Id == id);
            
            if(exam == null)
            {
                return NotFound();
            }
            
            return View(exam);
        }

        [HttpGet]
        public IActionResult Quiz(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var quizVM = new QuizVM
            {
                Exam = _db.Exams.Include(e => e.Questions).ThenInclude(q => q.Answers).FirstOrDefault(e => e.Id == id),
            };

            return View(quizVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult QuizResult(QuizVM quizVM)
        {
            ////// add the exam to user
            // get user authenticated id
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
            {
                return NotFound();
            }

            var userExams = new UserExams
            {
                UserId = claim.Value,
                ExamId = quizVM.Exam.Id
            };
            _db.UserExams.Add(userExams);
            _db.SaveChanges();


            ////// compare the user answers with the correct answers to add result
            //add user answers to database
            var userAnswers = new List<UserAnswers>();
            foreach (var userAnswer in quizVM.QuestionAnswers)
            {
                userAnswers.Add(new UserAnswers
                {
                    UserId = claim.Value,
                    AnswerId = userAnswer.AnswerId,
                    Created_at = DateTime.Now,
                    UserExamId = userExams.Id
                });
            }

            _db.UserAnswers.AddRange(userAnswers);
            _db.SaveChanges();

            // get correct answers for this exam
            var correct_answers = _db.Answers.Where(ca => ca.IsCorrect).Where(e => e.Question.Exam.Id == quizVM.Exam.Id).Include(ca => ca.Question);

            double totalResult = 0.0;

            foreach (var user_answers in quizVM.QuestionAnswers)
            {
                foreach (var correct_answer in correct_answers)
                {
                    if (user_answers.QuestionId == correct_answer.QuestionId)
                    {
                        if (user_answers.AnswerId == correct_answer.Id)
                        {
                            totalResult += correct_answer.Question.Mark;
                        }
                    }
                }
            }

            var quizResult = new QuizResultVM
            {
                FinalResult = totalResult,
                quizVM = quizVM,
            };

            return View(quizResult);
        }
        
    }
}