using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp_Model
{
    public class UserAnswers
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public DateTime Created_at { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int AnswerId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("AnswerId")]
        public Answer Answer { get; set; }

        public int? UserExamId { get; set; }
        [ForeignKey("UserExamId")]
        public UserExams UserExams { get; set; }
    }
}