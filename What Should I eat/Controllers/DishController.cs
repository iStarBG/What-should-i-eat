using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using What_Should_I_eat.Data;
using What_Should_I_eat.Data.Entities;
using What_Should_I_eat.Models;

namespace What_Should_I_eat.Controllers
{
    public class DishController : Controller
    {

        private readonly ApplicationDbContext _context;

        public DishController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Dishes.ToListAsync());
        }

        //Get Details

        public async Task<IActionResult> Details(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes
                .FirstOrDefaultAsync(d => d.Id == id);

            DishModel model = new DishModel()
            {
                Id = dish.Id,
                Name = dish.Name,
                DishIngredients = dish.DishIngredients,
                Ingredients = _context.Ingredients.ToList()
            };

            if (dish == null)
            {
                return NotFound();
            }

            return View(model);

        }

       


        //Get Create
        public IActionResult Create()
        {
            return View();
        }

        //Post: Create
        [HttpPost]
        public async Task<IActionResult> Create( DishModel model)
        {
            if (ModelState.IsValid)
            {
                Dish dish = new Dish()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Photo = SaveFile(model.Photo)
                };
                _context.Add(dish);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public static string SaveFile(IFormFile file)
        {
            var fileName = System.IO.Path.GetFileName(file.FileName);
            var extension = fileName.Split('.').Last();
            var fileNameWithoutExtension = string.Join("", fileName.Split('.').Take(fileName.Length - 1));

            var newFileName = "wwwroot/images/" + String.Format("{0}-{1:ddMMYYYHHmmss}.{2}",
                fileNameWithoutExtension,
                DateTime.Now,
                extension);
            if (!Directory.Exists(Path.GetDirectoryName(newFileName)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(newFileName));
            }

            using (var localFile = System.IO.File.OpenWrite(newFileName))
            {
                using (var uploadedFile = file.OpenReadStream())
                {
                    uploadedFile.CopyTo(localFile);
                }
            }

            return newFileName;
        }

        //Get: Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes.FindAsync(id);

            if (dish == null)
            {
                return NotFound();
            }

            DishModel model = new DishModel()
            {
                Id = dish.Id,
                Name = dish.Name,
                Photo = null
            };

            return View(model);

        }

        //Post:Edit
        [HttpPost]
        public async Task<IActionResult> Edit(DishModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Dish dish = new Dish()
                    {
                        Id= model.Id,
                        Name = model.Name,
                        Photo = SaveFile(model.Photo)
                    };
                    _context.Update(dish);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        //Get: Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes.FindAsync(id);


            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        //Post:Delete
        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            var dish = await _context.Dishes.FindAsync(id);

            _context.Remove(dish);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
