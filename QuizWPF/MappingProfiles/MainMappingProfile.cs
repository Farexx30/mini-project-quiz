using AutoMapper;
using QuizWPF.Models.Dtos;
using QuizWPF.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWPF.MappingProfiles
{
    public class MainMappingProfile : Profile
    {
        public MainMappingProfile()
        {
            //Quiz mappers:
            CreateMap<Quiz, QuizDto>();
            CreateMap<QuizDto, Quiz>();

            //Question mappers:
            CreateMap<Question, QuestionDto>();
            CreateMap<QuestionDto, Question>();

            //Answer mappers:
            CreateMap<Answer, AnswerDto>();
            CreateMap<AnswerDto, Answer>();
        }
      
    }
}
