using Ganymede.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ganymede.Controllers
{
    [Route("api/[controller]")]
    public class QuizController : Controller
    {
        // GET api/quiz/latest
        [HttpGet("latest/{num}")]
        public IActionResult Latest(int num = 10)
        {
            var sampleQuizzes = new List<QuizViewModel>();
            
            for (int i = 1; i <= num; i++)
            {
                sampleQuizzes.Add(new QuizViewModel()
                {
                    Id = i,
                    Title = String.Format("Sample Quiz {0}", i),
                    Description = "This is a sample quiz",
                    CreatedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                });
            }
            // output the result in JSON format
            return new JsonResult(
            sampleQuizzes,
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
            var sampleQuizzes = ((JsonResult)Latest(num)).Value
            as List<QuizViewModel>;
            return new JsonResult(
            sampleQuizzes.OrderBy(t => t.Title),
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
            var sampleQuizzes = ((JsonResult)Latest(num)).Value
            as List<QuizViewModel>;
            return new JsonResult(
            sampleQuizzes.OrderBy(t => Guid.NewGuid()),
            new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
        });
}
}
}
