using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ExtIntegration.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CalendarController: Controller
    {
        [HttpGet]
        public IList<Event> Get()
        {
            return new List<Event>{
                new Event{
                    Id=100,
                    Title="Test Event",
                    Date=DateTime.Now
                }
            };
        }
    }
}
