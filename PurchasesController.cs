using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VizsgaAPI.Data;
using VizsgaAPI.Models;

namespace VizsgaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchasesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PurchasesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(long userId)
        {
            return Ok(await _context.Purchases
                .Where(p => p.UserId == userId)
                .ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Purchase purchase)
        {
            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();
            return Ok(purchase);
        }
    }
}
