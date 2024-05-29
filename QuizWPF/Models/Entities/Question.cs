    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace QuizWPF.Models.Entities
    {
        public class Question
        {
            public int Id { get; set; }
            public string Value { get; set; } = null!;
            public Quiz Quiz { get; set; } = null!;
            public int QuizId { get; set; }
            public List<Answer> Answers { get; set; } = [];
        }
    }
