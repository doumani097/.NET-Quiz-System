using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp_Model.ViewModels
{
    public class ManageQuestionVM
    {
        public Exam Exam { get; set; }
        public IList<Question> Questions { get; set; }
    }
}
