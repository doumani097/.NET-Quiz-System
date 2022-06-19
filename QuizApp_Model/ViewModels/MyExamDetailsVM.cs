using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp_Model.ViewModels
{
    public class MyExamDetailsVM
    {
        public Exam Exam { get; set; }
        public IEnumerable<UserAnswers> UserAnswers { get; set; }
    }
}
