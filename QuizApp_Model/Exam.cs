using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp_Model
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string ShortDescription { get; set; }

        [Required]
        [MinLength(50)]
        [MaxLength(500)]
        public string Description { get; set; }

        public string Image { get; set; }

        [Required]
        public DateTime Created_at { get; set; }

        public IList<Question> Questions { get; set; }
    }
}
