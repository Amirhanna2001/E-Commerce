using E_Commerce.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers
{
    public class Category : Controller
    {
        private readonly ApplicationDbContext _context;
        public Category(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task< IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }
    }
}
