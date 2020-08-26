using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class DataController : Controller
    {
        ChildRepository repository = new ChildRepository();

        [HttpGet]
        public Child[] Children()
        {
            return repository.GetChildren().Result;
        }

        [HttpPost]
        public void Post([FromBody]Child child)
        {
            int res = repository.AddChild(child).Result;
        }

        [HttpDelete("{id}")]
        public void  Delete(int id)
        {
            int res = repository.RemoveChild(id).Result;
        }

        public class Child
        {
            public int Id { get; set; }
            public string Lastname { get; set; }
            public string Firstname { get; set; }
            public string Gender { get; set; }
            public DateTime Birthdate { get; set; }
        }
    }
}
