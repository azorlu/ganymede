using Ganymede.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ganymede.Data
{
    public class DbSeeder
    {
        public DbSeeder()
        {

        }

        public static void Seed(ApplicationDbContext dbContext)
        {
            // Create default Users (if there are none)
            if (!dbContext.Users.Any())
                CreateUsers(dbContext);
            // Create default Quizzes (if there are none) together with their
            // set of Q & A
            if (!dbContext.Quizzes.Any())
                CreateQuizzes(dbContext);
        }

        private static void CreateUsers(ApplicationDbContext dbContext)
        {
            // local variables
            DateTime createdDate = new DateTime(2016, 03, 01, 12, 30, 00);
            DateTime lastModifiedDate = DateTime.Now;
            // Create the "Admin" ApplicationUser account (if it doesn't exist
            // already)
            var user_Admin = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Admin",
                Email = "admin@ganymede.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };
            // Insert the Admin user into the Database
            dbContext.Users.Add(user_Admin);
#if DEBUG
            // Create some sample registered user accounts (if they don't exist
            //already)
            var user_01 = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "User01",
                Email = "user01@ganymede.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };
            var user_02 = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "User02",
                Email = "user02@ganymede.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };
            var user_03 = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "User03",
                Email = "user03@ganymede.com",
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            };
            // Insert sample registered users into the Database
            dbContext.Users.AddRange(user_01, user_02, user_03);
#endif

            dbContext.SaveChanges();
        }

        private static void CreateQuizzes(ApplicationDbContext dbContext)
        {
            // local variables
            DateTime createdDate = new DateTime(2016, 03, 01, 12, 30, 00);
            DateTime lastModifiedDate = DateTime.Now;
            // retrieve the admin user, which we'll use as default author.
            var authorId = dbContext.Users
            .Where(u => u.UserName == "Admin")
            .FirstOrDefault()
            .Id;
#if DEBUG
            // create 47 sample quizzes with auto-generated data
            // (including questions, answers & results)
            var num = 47;
            for (int i = 1; i <= num; i++)
            {
                CreateSampleQuiz(
                dbContext,
                i,
                authorId,
                num - i,
                3,
                3,
                3,
                createdDate.AddDays(-num));
            }
#endif
            // create 3 more quizzes with better descriptive data
            // (we'll add the questions, answers & results later on)
            EntityEntry<Quiz> e1 = dbContext.Quizzes.Add(new Quiz()
            {
                UserId = authorId,
                Title = "Quiz 01",
                Description = "Description of Quiz 01",
                Text = @"Text of Quiz 01",
                ViewCount = 2343,
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            });
            EntityEntry<Quiz> e2 = dbContext.Quizzes.Add(new Quiz()
            {
                UserId = authorId,
                Title = "Quiz 02",
                Description = "Description of Quiz 02",
                Text = @"Text of Quiz 02",
                ViewCount = 4180,
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            });
            EntityEntry<Quiz> e3 = dbContext.Quizzes.Add(new Quiz()
            {
                UserId = authorId,
                Title = "Quiz 03",
                Description = "Description of Quiz 03",
                Text = @"Text of Quiz 03",
                ViewCount = 5203,
                CreatedDate = createdDate,
                LastModifiedDate = lastModifiedDate
            });
            // persist the changes on the Database

            dbContext.SaveChanges();
        }

        /// <summary>
        /// Creates a sample quiz and add it to the Database
        /// together with a sample set of questions, answers & results.
        /// </summary>
        /// <param name="userId">the author ID</param>
        /// <param name="id">the quiz ID</param>
        /// <param name="createdDate">the quiz CreatedDate</param>
        private static void CreateSampleQuiz(
        ApplicationDbContext dbContext,
        int num,
        string authorId,
        int viewCount,
        int numberOfQuestions,
        int numberOfAnswersPerQuestion,
        int numberOfResults,
        DateTime createdDate)
        {
            var quiz = new Quiz()
            {
                UserId = authorId,
                Title = String.Format("Quiz {0} Title", num),
                Description = String.Format("This is a sample description for quiz {0}.", num),
                Text = "This is a sample quiz.",
                ViewCount = viewCount,
                CreatedDate = createdDate,
                LastModifiedDate = createdDate
            };
            dbContext.Quizzes.Add(quiz);
            dbContext.SaveChanges();
            for (int i = 0; i < numberOfQuestions; i++)
            {
                var question = new Question()
                {
                    QuizId = quiz.Id,
                    Text = "This is a sample question.",
                    CreatedDate = createdDate,
                    LastModifiedDate = createdDate
                };
                dbContext.Questions.Add(question);
                dbContext.SaveChanges();
                for (int i2 = 0; i2 < numberOfAnswersPerQuestion; i2++)
                {
                    var e2 = dbContext.Answers.Add(new Answer()
                    {
                        QuestionId = question.Id,
                        Text = "This is a sample answer.",
                        Value = i2,
                        CreatedDate = createdDate,
                        LastModifiedDate = createdDate
                    });
                }
            }
            for (int i = 0; i < numberOfResults; i++)
            {
                dbContext.Results.Add(new Result()
                {
                    QuizId = quiz.Id,
                    Text = "This is a sample result. ",
                    MinValue = 0,
                    // max value should be equal to answers number * max answer value
                    MaxValue = numberOfAnswersPerQuestion * 2,
                    CreatedDate = createdDate,
                    LastModifiedDate = createdDate
                });
            }
            dbContext.SaveChanges();
        }

    }
}
