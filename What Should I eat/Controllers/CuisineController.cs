using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using What_Should_I_eat.Data;
using What_Should_I_eat.Data.Entities;
using What_Should_I_eat.Models;

namespace What_Should_I_eat.Controllers
{
    public class CuisineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CuisineController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Continents = _context.Continents
                .Select(x => new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }
                ).ToList();
            return View( await _context.Cuisines.ToListAsync());
        }

        //Get Cuisine
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var cuisine = await _context.Cuisines.FirstOrDefaultAsync(c => c.Id == id);

            CuisineModel model = new CuisineModel()
            {
                Id = cuisine.Id,
                Name = cuisine.Name,
                ContinentId = cuisine.ContinentId,
                Description = cuisine.Description,
                CuisineDishes = cuisine.CuisineDishes,
                Dishes = _context.Dishes.ToList()
            };

            if (cuisine == null)
            {
                return NotFound();
            }


            return View(model);

        }


        //Get Create
        public IActionResult Create()
        {
            ViewBag.Continents = _context.Continents
                .Select(x => new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }
                ).ToList();
            return View();
        }

        //Post Create
        [HttpPost]
        public async Task<IActionResult> Create(CuisineModel model)
        {
            
            if (ModelState.IsValid)
            {
                Cuisine cuisine = new Cuisine()
                {
                    Name = model.Name,
                    ContinentId = model.ContinentId,
                    Description = model.Description,
                    Photo = SaveFile(model.Photo)
                };

                _context.Add(cuisine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            return View(model);
        }

        private static string SaveFile(IFormFile file)
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

        // Get: Edit

        public async Task<IActionResult> Edit(int? id)
        {
            if (id ==null)
            {
                return NotFound();
            }

            var cuisine = await _context.Cuisines.FindAsync(id);

            if (cuisine==null)
            {
                return NotFound();
            }


            CuisineModel model = new CuisineModel()
            {
                Id = cuisine.Id,
                ContinentId = cuisine.ContinentId,
                Name = cuisine.Name,
                Description = cuisine.Description,
                Photo = null
            };


            return View(model);
        }

        // Post: Edit
        [HttpPost]

        public async Task<IActionResult> Edit(CuisineModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    Cuisine cuisine = new Cuisine()
                    {
                        Id=model.Id,
                        Name = model.Name,
                        ContinentId = model.ContinentId,
                        Description = model.Description,
                        Photo = SaveFile(model.Photo)
                    };
                    _context.Update(cuisine);
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


        //Get Delete
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var cuisine = await _context.Cuisines.FirstOrDefaultAsync(c =>c.Id ==id);

            if (cuisine==null)
            {
                return NotFound();
            }

            return View(cuisine);

        }

        //Post Delete
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {

            var cuisine = await _context.Cuisines.FindAsync(id);

            _context.Remove(cuisine);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> AssignDish(int cuisineId, int dishId) 
        {
            var cuisine = await _context.Cuisines.FindAsync(cuisineId);

            if (cuisine != null)
            {
                var dish = await _context.Dishes.FindAsync(dishId);

                if (dish!=null)
                {
                    if (cuisine.CuisineDishes.
                        Where(item => item.CuisineId==cuisineId && item.DishId==dishId).
                        Count() ==0)
                    {
                        cuisine.CuisineDishes.Add(new CuisineDish()
                        {
                            CuisineId = cuisineId,
                            DishId = dishId
                        });

                        await _context.SaveChangesAsync();
                    }
                }

            }

            return RedirectToAction("Index");
        }


    }
}
