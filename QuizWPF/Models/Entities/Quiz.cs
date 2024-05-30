using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWPF.Models.Entities
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Category Category { get; set; }
        public List<Question> Questions { get; set; } = [];
    }

    public enum Category
    {
        Games,
        Anime,
        Animals,
        Math,
        Geography,
        Physics,
        Biology,
        Chemistry,
        English,       
    }
}
