using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace E_Commerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly int _maxAllowedPosterSize = 1048576;
        private readonly List<string> _allowedExtenstions = new() { ".jpg", ".png" };


        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task< IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CreateProductViewModel model = new()
            {
                Categories = await _context.Categories.ToListAsync()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories =await _context.Categories.ToListAsync();
                return View(model);
            }
                

            var files = Request.Form.Files;

            if (!files.Any())
            {
                model.Categories = await _context.Categories.ToListAsync();
                ModelState.AddModelError("Image", "Please select Product Image!");
                return View(model);
            }

            var image = files.FirstOrDefault();

            if (!CkeckFilesExtentions(image))
            {
                model.Categories = await _context.Categories.ToListAsync();
                ModelState.AddModelError("Image", "Only .PNG, .JPG images are allowed!");
                return View( model);
            }

            if (image.Length > _maxAllowedPosterSize)
            {
                model.Categories = await _context.Categories.ToListAsync();
                ModelState.AddModelError("Image", "Image cannot be more than 1 MB!");
                return View( model);
            }
            using var dataStream = new MemoryStream();
            await image.CopyToAsync(dataStream);
            Product product = new()
            {
                Name = model.Name,
                CategoryId = model.CategoryId,
                Brand = model.Brand,
                Description = model.Description,
                Price = model.Price,
                ProductId = model.ProductId,
                Image = dataStream.ToArray(),
            };

            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
                return BadRequest();

            Product product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();



            CreateProductViewModel model = new()
            {

                Name = product.Name,
                CategoryId = product.CategoryId,
                Brand = product.Brand,
                Description = product.Description,
                Price = product.Price,
                ProductId = product.ProductId,
                Categories = _context.Categories.ToList()


            };
            return View(model);

        }

        public IActionResult Delete(int id)
        {
            if (id == null)
                return NotFound();

            Product product = _context.Products.Find(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        } 

        public IActionResult Details(int id)
        {
            if (id == null)
                return NotFound();
            Product product = _context.Products.Find(id);

            CreateProductViewModel model = new()
            {

                Name = product.Name,
                CategoryId = product.CategoryId,
                Brand = product.Brand,
                Description = product.Description,
                Price = product.Price,
                ProductId = product.ProductId,
                Categories = _context.Categories.ToList()


            };

            return View(model);
        }
        private bool CkeckFilesExtentions(IFormFile file)
        {
            return (_allowedExtenstions.Contains(Path.GetExtension(file.FileName).ToLower()));
        }

        public bool ckeckFileLengthIsAllowed(IFormFile file)
        {
            return (file.Length <= _maxAllowedPosterSize);
        }
    }
}
