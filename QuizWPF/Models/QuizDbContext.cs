using Microsoft.EntityFrameworkCore;
using QuizWPF.Models.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizWPF.Models
{
    public class QuizDbContext(DbContextOptions<QuizDbContext> options) : DbContext(options)
    {
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Quiz)
                .WithMany(q => q.Questions)
                .HasForeignKey(q => q.QuizId);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithOne(a => a.Question)
                .HasForeignKey(a => a.QuestionId);

            //Tutaj jeszcze pare ograniczeń itd. bedzie mozna dodac, ale sama struktura git, i ZAKAZ ruszania zawartosci folderu "Migrations", no chyba, ze wie co sie robi, bo to wtedy moze rozwalić przyszłe ewentualne zmiany na bazie.
        }
    }
}
