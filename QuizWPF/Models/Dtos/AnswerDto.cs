using QuizWPF.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWPF.Models.Dtos
{
    public class AnswerDto
    {
        public string Value { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}
