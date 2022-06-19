using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp_Model
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The Number Should be Greater Than 1")]
        public int Number { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "The Question Mark Should be Between 1 and 5")]
        public double Mark { get; set; }

        [Required]
        public DateTime Created_at { get; set; }

        [Required]
        public int ExamId { get; set; }

        [ForeignKey("ExamId")]
        public Exam Exam { get; set; }

        public IList<Answer> Answers { get; set; }
    }
}
