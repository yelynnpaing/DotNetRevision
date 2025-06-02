using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentRegApi.Data;
using StudentRegApi.Model;
using StudentRegApi.Model.Entities;

namespace StudentRegApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentRegController : ControllerBase
    {
        private readonly AppDbContext _context;
        public StudentRegController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult StudentGet()
        {
            var students = _context.Students.ToList();
            return Ok(students);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult StudentGet(Guid id)
        {
            var student = _context.Students.Find(id);
            if (student is null) return NotFound("No student found.");
            return Ok(student);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult StudentUpdate(Guid id, StudentDto studentDto)
        {
            var student = _context.Students.FirstOrDefault(x => x.Id == id);
            if (student is null) return NotFound();
            student.Name = studentDto.Name;
            student.FatherName = studentDto.FatherName;
            student.Grade = studentDto.Grade;
            student.Address = studentDto.Address;

            _context.SaveChanges();
            return Ok(student);
        }

        [HttpPost]
        public IActionResult StudentAdd(StudentDto studentDto)
        {
            var studentEntity = new Student()
            {
                Name = studentDto.Name,
                FatherName = studentDto.FatherName,
                Grade = studentDto.Grade,
                Address = studentDto.Address,
            };

            _context.Students.Add(studentEntity);
            _context.SaveChanges();
            return Ok("new student adding is success.");
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult StudentDelete(Guid id)
        {
            var student = _context.Students.FirstOrDefault(x => x.Id == id);
            if (student is null) return NotFound();
            _context.Students.Remove(student);
            _context.SaveChanges();
            return Ok("Student is delete successfully.");
        }
    }
}
