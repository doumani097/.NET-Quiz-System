using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp_Model.ViewModels
{
    public class ManageAnswerVM
    {
        public Question Question { get; set; }
        public IList<Answer> Answers { get; set; }
    }
}
