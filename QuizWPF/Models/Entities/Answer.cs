using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWPF.Models.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public string Value { get; set; } = null!;
        public bool IsCorrect { get; set; }
        public Question Question { get; set; } = null!;
        public int QuestionId { get; set; }
    }
}
