using Ganymede.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ganymede.Controllers
{
    [Route("api/[controller]")]
    public class BaseApiController : Controller
    {
        protected ApplicationDbContext DbContext { get; private set; }
        protected JsonSerializerSettings JsonSettings { get; private set; }

        public BaseApiController(ApplicationDbContext context)
        {            
           // Instantiate the ApplicationDbContext through DI
           DbContext = context;
            // Instantiate a single JsonSerializerSettings object
            // that can be reused multiple times.
            JsonSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            };
        }

    }
}
