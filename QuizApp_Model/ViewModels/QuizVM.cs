using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp_Model.ViewModels
{
    public class QuizVM
    {
        public Exam Exam { get; set; }
        public List<QuestionAnswers> QuestionAnswers { get; set; }
    }

    public class QuestionAnswers
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
    }
}