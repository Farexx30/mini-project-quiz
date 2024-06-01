using QuizWPF.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWPF.Models.Dtos
{
    public class QuizDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Category Category { get; set; }
        public List<QuestionDto> Questions { get; set; } = [];

        public override string ToString() => Name;
    }
}
