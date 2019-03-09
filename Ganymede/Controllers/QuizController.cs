using Ganymede.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Ganymede.Data;

namespace Ganymede.Controllers
{
    [Route("api/[controller]")]
    public class QuizController : Controller
    {
        private ApplicationDbContext DbContext;

        public QuizController(ApplicationDbContext context)
        {
            // Instantiate the ApplicationDbContext through DI
            DbContext = context;
        }

        /// <summary>
        /// GET: api/quiz/{}id
        /// Retrieves the Quiz with the given {id}
        /// </summary>
        /// <param name="id">The ID of an existing Quiz</param>
        /// <returns>the Quiz with the given {id}</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            var quiz = DbContext.Quizzes.Where(i => i.Id == id).FirstOrDefault();
            
            //var v = new QuizViewModel()
            //{
            //    Id = id,
            //    Title = String.Format("Sample quiz with id {0}", id),
            //    Description = "Not a real quiz: it's just a sample!",
            //    CreatedDate = DateTime.Now,
            //    LastModifiedDate = DateTime.Now
            //};

            return new JsonResult(
            quiz.Adapt<QuizViewModel>(),
            new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            });
        }

        /// <summary>
        /// GET: api/quiz/latest
        /// Retrieves the {num} latest Quizzes
        /// </summary>
        /// <param name="num">the number of quizzes to retrieve</param>
        /// <returns>the {num} latest Quizzes</returns>
        [HttpGet("Latest/{num}")]
        public IActionResult Latest(int num = 10)
        {
            var latest = DbContext.Quizzes
                .OrderByDescending(q => q.CreatedDate)
                .Take(num)
                .ToArray();

            //var sampleQuizzes = new List<QuizViewModel>();
            
            //for (int i = 1; i <= num; i++)
            //{
            //    sampleQuizzes.Add(new QuizViewModel()
            //    {
            //        Id = i,
            //        Title = String.Format("Sample Quiz {0}", i),
            //        Description = "This is a sample quiz",
            //        CreatedDate = DateTime.Now,
            //        LastModifiedDate = DateTime.Now
            //    });
            //}
            
            
            return new JsonResult(
            latest.Adapt<QuizViewModel[]>(),
            new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            });
        }

        /// <summary>
        /// GET: api/quiz/ByTitle
        /// Retrieves the {num} Quizzes sorted by Title (A to Z)
        /// </summary>
        /// <param name="num">the number of quizzes to retrieve</param>
        /// <returns>{num} Quizzes sorted by Title</returns>
        [HttpGet("ByTitle/{num:int?}")]
        public IActionResult ByTitle(int num = 10)
        {
            var byTitle = DbContext.Quizzes
                .OrderBy(q => q.Title)
                .Take(num)
                .ToArray();

            //var sampleQuizzes = ((JsonResult)Latest(num)).Value
            //as List<QuizViewModel>;

            return new JsonResult(
            byTitle.Adapt<QuizViewModel[]>(),
            new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            });
        }

        /// <summary>
        /// GET: api/quiz/mostViewed
        /// Retrieves the {num} random Quizzes
        /// </summary>
        /// <param name="num">the number of quizzes to retrieve</param>
        /// <returns>{num} random Quizzes</returns>
        [HttpGet("Random/{num:int?}")]
        public IActionResult Random(int num = 10)
        {
            var random = DbContext.Quizzes
                .OrderBy(q => Guid.NewGuid())
                .Take(num)
                .ToArray();

            //var sampleQuizzes = ((JsonResult)Latest(num)).Value
            //as List<QuizViewModel>;

            return new JsonResult(
            random.Adapt<QuizViewModel[]>(),
            new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            });
}
}
}
