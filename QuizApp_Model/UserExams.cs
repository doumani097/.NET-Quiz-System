using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp_Model
{
    public class UserExams
    {
        public int Id { get; set; }
        
        [Required]
        public string UserId { get; set; }
    
        [Required]
        public int ExamId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
        
        [ForeignKey("ExamId")]
        public Exam Exam { get; set; }

        public IEnumerable<UserAnswers> UserAnswers { get; set; }
    }
}
