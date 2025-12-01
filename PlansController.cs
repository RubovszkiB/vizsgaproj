using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VizsgaAPI.Data;
using VizsgaAPI.Models;

namespace VizsgaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlansController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlansController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _context.Plans.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var plan = await _context.Plans.FindAsync(id);
            return plan == null ? NotFound() : Ok(plan);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Plan plan)
        {
            _context.Plans.Add(plan);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = plan.Id }, plan);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, Plan updated)
        {
            var plan = await _context.Plans.FindAsync(id);
            if (plan == null) return NotFound();

            plan.Title = updated.Title;
            plan.Description = updated.Description;
            plan.Price = updated.Price;
            plan.ImageUrl = updated.ImageUrl;

            await _context.SaveChangesAsync();

            return Ok(plan);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var plan = await _context.Plans.FindAsync(id);
            if (plan == null) return NotFound();

            _context.Plans.Remove(plan);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
