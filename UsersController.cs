using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VizsgaAPI.Data;
using VizsgaAPI.Models;

namespace VizsgaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _context.Users.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var user = await _context.Users.FindAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, User updated)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            user.Username = updated.Username;
            user.Email = updated.Email;
            user.PasswordHash = updated.PasswordHash;

            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
