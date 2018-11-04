using System;
using StackExchange.Redis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace KuboTest.Controllers
{
    [Route("api/[controller]")]
    public class ThingsController : ControllerBase
    {
        private IConfiguration config;
        public ThingsController(IConfiguration config)
        {
            this.config = config;
        }

        // GET api/values/5
        [HttpGet()]
        public ActionResult<string> Get()
        {            
            string redisHost = config["REDIS-SERVICE"];
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(redisHost);
            IDatabase db = redis.GetDatabase(); 
            db.StringIncrement("THING");
            return $"UMBEL: {db.StringGet("THING").ToString()}";
        }
    }
}