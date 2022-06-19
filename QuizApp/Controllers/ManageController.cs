using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp_Data;
using QuizApp_Model;
using QuizApp_Model.ViewModels;
using QuizApp_Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QuizApp.Controllers
{
    public class ManageController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ManageController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        




        //Manage Exam Section
        public IActionResult Exams()
        {
            //get all exams from db
            var exams = _db.Exams.Include(e => e.Questions);
            
            return View("Exam/Index",exams);
        }
        
        [ActionName("create-exams")]
        public IActionResult CreateExams(int no = 1)
        {
            if (no == 0)
            {
                return NotFound();
            }
            ViewBag.no_of_exams = no;

            return View("Exam/Create");
        }

        [HttpPost]
        [ActionName("store-exams")]
        [ValidateAntiForgeryToken]
        public IActionResult StoreExams(IList<Exam> exams)
        {
            if (ModelState.IsValid)
            {
                //upload an or multiple images for the exams to the server
                var webRootPath = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                string upload = webRootPath + WC.ImageExamPath;

                if(files.Count() > 0)
                {
                    for (int i = 0; i < exams.Count; i++)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[i].FileName);

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[i].CopyTo(fileStream);
                        }

                        exams[i].Image = fileName + extension;
                        exams[i].Created_at = DateTime.Now;
                    }
                }

                //add the exam or exams to the db
                _db.Exams.AddRange(exams);
                _db.SaveChanges();

                return RedirectToAction(nameof(Exams));
            }
            return View("Exam/Create",exams);
        }

        [ActionName("edit-exam")]
        public IActionResult EditExam(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //get an exam from db
            var exam = _db.Exams.FirstOrDefault(e => e.Id == id);
            
            if (exam == null)
            {
                return NotFound();
            }

            return View("Exam/Edit",exam);
        }

        [HttpPost]
        [ActionName("update-exam")]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateExam(Exam examModel)
        {
            if (ModelState.IsValid)
            {
                //get an exam from db
                var exam = _db.Exams.FirstOrDefault(e => e.Id == examModel.Id);

                var webRootPath = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                //check if there any files uploaded
                if (files.Count > 0)
                {
                    //upload an image for the exam to the server
                    string upload = webRootPath + WC.ImageExamPath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    var oldFile = Path.Combine(upload, exam.Image);
                    if (System.IO.File.Exists(oldFile))
                    {
                        System.IO.File.Delete(oldFile);
                    }

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    exam.Image = fileName + extension;
                }

                //update the exam in db
                exam.Name = examModel.Name;
                exam.ShortDescription = examModel.ShortDescription;
                exam.Description = examModel.Description;
                _db.Exams.Update(exam);
                _db.SaveChanges();

                return RedirectToAction(nameof(Exams));
            }
            return View("Exam/Edit",examModel);
        }

        [HttpPost]
        [ActionName("delete-exam")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteExam(Exam exam)
        {
            if(exam.Id == 0)
            {
                return NotFound();
            }

            //remove image from the server
            var webRootPath = _webHostEnvironment.WebRootPath;
            string upload = webRootPath + WC.ImageExamPath;

            //get an exam from db
            var examInDb = _db.Exams.AsNoTracking().FirstOrDefault(e => e.Id == exam.Id);

            var delFile = Path.Combine(upload, examInDb.Image);
            if (System.IO.File.Exists(delFile))
            {
                System.IO.File.Delete(delFile);
            }

            //remove the exam from db
            _db.Exams.Remove(exam);
            _db.SaveChanges();

            return RedirectToAction(nameof(Exams));
        }





        //Manage Question Section
        public IActionResult Questions(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var questions = _db.Exams.Include(e => e.Questions).FirstOrDefault(e => e.Id == id);

            if (questions == null)
            {
                return NotFound();
            }

            return View("Question/Index", questions);
        }

        [ActionName("create-questions")]
        public IActionResult CreateQuestions(int? id, int no = 1)
        {
            if (id == null || id == 0 || no == 0)
            {
                return NotFound();
            }

            var manageQuestionVM = new ManageQuestionVM
            {
                Exam = _db.Exams.FirstOrDefault(e => e.Id == id),
            };

            if (manageQuestionVM.Exam == null)
            {
                return NotFound();
            }

            ViewBag.no_of_questions = no;

            return View("Question/Create",manageQuestionVM);
        }

        [HttpPost]
        [ActionName("store-questions")]
        [ValidateAntiForgeryToken]
        public IActionResult StoreQuestions(ManageQuestionVM manageQuestionVM)
        {
            if (ModelState.IsValid)
            {
                foreach (var question in manageQuestionVM.Questions)
                {
                    question.Created_at = DateTime.Now;
                }
                _db.Questions.AddRange(manageQuestionVM.Questions);
                _db.SaveChanges();
                return RedirectToAction(nameof(Questions), new { id = manageQuestionVM.Exam.Id });
            }
            return View("Question/Create",manageQuestionVM);
        }

        [ActionName("edit-question")]
        public IActionResult EditQuestion(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var question = _db.Questions.Include(q => q.Exam).Include(q => q.Answers).FirstOrDefault(q => q.Id == id);

            if (question == null)
            {
                return NotFound();
            }
            ModelState.Clear();
            return View("Question/Edit",question);
        }

        [HttpPost]
        [ActionName("update-question")]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateQuestion(Question question)
        {
            if (ModelState.IsValid)
            {
                _db.Questions.Update(question);
                _db.SaveChanges();
                return RedirectToAction(nameof(Questions), new { id = question.ExamId });
            }
            return View("Question/Edit",question);
        }

        [HttpPost]
        [ActionName("delete-question")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteQuestion(Question question)
        {
            _db.Questions.Remove(question);
            _db.SaveChanges();
            return RedirectToAction(nameof(Questions), new { id = question.ExamId });
        }





        //Manage Answer Section
        [ActionName("create-answers")]
        public IActionResult CreateAnswers(int? id, int no = 1)
        {
            if (id == null || id == 0 || no == 0)
            {
                return NotFound();
            }

            var manageAnswerVM = new ManageAnswerVM
            {
                Question = _db.Questions.FirstOrDefault(q => q.Id == id)
            };

            ViewBag.answerNumbers = no;

            return View("Answer/Create",manageAnswerVM);
        }

        [HttpPost]
        [ActionName("store-answers")]
        [ValidateAntiForgeryToken]
        public IActionResult StoreAnswers(ManageAnswerVM manageAnswerVM)
        {
            foreach (var answer in manageAnswerVM.Answers)
            {
                answer.Created_at = DateTime.Now;
            }

            _db.Answers.AddRange(manageAnswerVM.Answers);
            _db.SaveChanges();
            return RedirectToAction("edit-question", new { id = manageAnswerVM.Question.Id });
        }

        [ActionName("edit-answer")]
        public IActionResult EditAnswer(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var answer = _db.Answers.FirstOrDefault(a => a.Id == id);
            return View("Answer/Edit", answer);
        }

        [HttpPost]
        [ActionName("update-answer")]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateAnswer(Answer answer)
        {
            if (ModelState.IsValid)
            {
                _db.Answers.Update(answer);
                _db.SaveChanges();
                return RedirectToAction("edit-question", new { id = answer.QuestionId });
            }
            return View("Answer/Edit",answer);
        }

        [HttpPost]
        [ActionName("delete-answer")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAnswer(Answer answer)
        {
            _db.Answers.Remove(answer);
            _db.SaveChanges();
            return RedirectToAction("edit-question", new { id = answer.QuestionId });
        }
    }
}