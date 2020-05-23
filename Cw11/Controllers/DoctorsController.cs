using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw11.Models;
using Cw11.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cw11.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorsDb _context;

        public DoctorsController(IDoctorsDb context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetDoctors()
        {
            return Ok(_context.GetDoctors());
        }
        [HttpPost]
        public IActionResult AddDoctor(Doctor doctor)
        {
            return Ok(_context.AddDoctor(doctor));
        }
        [HttpPut]
        public IActionResult ModifyDoctor(Doctor doc)
        {
            return Ok(_context.ModifyDoctor(doc));
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(int id)
        {
            _context.DeleteDoctor(id);
            return Ok();
        }

        [HttpPost("/seed")]
        public IActionResult Seed()
        {
            _context.Seed();
            return Ok();
        }
    }
}