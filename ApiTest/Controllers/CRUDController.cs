using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUDController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            List<Person> list = new List<Person>();
            using (var context = new DatabaseContext())
            {
                
                list= context.Person.ToList();                
            }
            return Ok(list);
        }
    }
}
