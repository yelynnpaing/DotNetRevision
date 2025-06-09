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
        [Route("list")]
        public ActionResult Get()
        {
            List<Person> list = new List<Person>();
            using (var context = new DatabaseContext())
            {                
                list = context.Person.ToList();                
            }
            return Ok(list);
        }

        [HttpGet]
        [Route("get/{id:int}")]
        public ActionResult Get(int id)
        {
            using (var context = new DatabaseContext())
            {
                var person = context.Person.Find(id);
                if (person is null) return NotFound();
                return Ok(person);
            }
        }

        [HttpPost]
        [Route("add")]
        public ActionResult Add(Person p)
        {
            using(var context = new DatabaseContext())
            {
                var person = new Person()
                {
                    Name = p.Name,
                    Nrc = p.Nrc,
                    Nationality = p.Nationality,
                };

                context.Person.Add(person);
                context.SaveChanges();
                return Ok(person);
            }
        }

        [HttpPut]
        [Route("update/{id:int}")]
        public ActionResult Update(int id, Person p)
        {
            using (var context = new DatabaseContext())
            {
                var person = context.Person.FirstOrDefault(x => x.Id == id);
                if (person is null) return NotFound();
                person.Name = p.Name;
                person.Nrc = p.Nrc;
                person.Nationality = p.Nationality;
                
                context.SaveChanges();
                return Ok(person);
            }
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            using(var context = new DatabaseContext())
            {
                var person = context.Person.Find(id);
                if (person is null) return NotFound();
                context.Person.Remove(person);
                context.SaveChanges();
                return Ok(person);
            }
        }
    }
}
