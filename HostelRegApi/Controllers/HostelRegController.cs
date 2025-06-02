using HostelRegApi.Data;
using HostelRegApi.Models;
using HostelRegApi.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HostelRegApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostelRegController : ControllerBase
    {
        private readonly AppDbContext _context;
        public HostelRegController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ResidentGet()
        {
            var residents = _context.Residents.ToList();
            return Ok(residents);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult ResidentGet(Guid id)
        {
            var resident = _context.Residents.FirstOrDefault(x => x.Id == id);
            if (resident is null) return NotFound("No Resident found.");
            return Ok(resident);
        }

        [HttpPost]
        public IActionResult ResidentAdd(ResidentDto residentDto)
        {
            var resident = new Resident()
            {
                Name = residentDto.Name,
                PhoneNumber = residentDto.PhoneNumber,
                Address = residentDto.Address,
            };
            _context.Residents.Add(resident);
            _context.SaveChanges();
            return Ok(resident);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult ResidentUpdate(Guid id, ResidentDto residentDto)
        {
            var resident = _context.Residents.FirstOrDefault(x => x.Id ==id);
            if (resident is null) return NotFound();
            resident.Name = residentDto.Name;
            resident.PhoneNumber = residentDto.PhoneNumber;
            resident.Address = residentDto.Address;

            _context.SaveChanges();
            return Ok(resident);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult ResidentDelete(Guid id)
        {
            var resident = _context.Residents.FirstOrDefault(x => x.Id == id);
            if (resident is null) return NotFound();
            _context.Residents.Remove(resident);
            _context.SaveChanges();
            return Ok("Resident was successfully delete.");
        }
    }
}
