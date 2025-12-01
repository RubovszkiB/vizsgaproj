using Microsoft.AspNetCore.Mvc;
using VizsgaAPI.Data;
using VizsgaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace VizsgaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Send(ContactMessage msg)
        {
            _context.ContactMessages.Add(msg);
            await _context.SaveChangesAsync();
            return Ok(msg);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _context.ContactMessages.ToListAsync());
    }
}
