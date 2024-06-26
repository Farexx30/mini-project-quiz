﻿using QuizWPF.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuizWPF.Models.Dtos
{
    public class QuestionDto
    {
        public string Value { get; set; } = string.Empty;
        public int QuizId { get; set; }
        public List<AnswerDto> Answers { get; set; } = [new AnswerDto(), new AnswerDto(), new AnswerDto(), new AnswerDto()];

        public override string ToString() => Value;
    }
}
