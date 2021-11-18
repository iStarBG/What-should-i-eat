using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Internal.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using What_Should_I_eat.Data;
using What_Should_I_eat.Data.Entities;
using What_Should_I_eat.Models;

namespace What_Should_I_eat.Controllers
{
    public class IngredientController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IngredientController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async  Task<IActionResult> Index()
        {
            return View(await _context.Ingredients.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(i=>i.Id == id);

            if (ingredient == null)
            {
                return NotFound();
            }

            IngredientModel model = new IngredientModel()
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
            };

            return View(model);
        }

        // Get: Create
        public IActionResult Create()
        {
            return View();
        }
        //Post
        [HttpPost]
        public async Task<IActionResult> Create(IngredientModel model)
        {
            if (ModelState.IsValid)
            {
                Ingredient ingredient = new Ingredient()
                    {
                        Name = model.Name,
                        Photo = SaveFile(model.Photo)
                    };

                    _context.Add(ingredient);
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

            var ingredient = await _context.Ingredients.FindAsync(id);

            if (ingredient==null)
            {
                return NotFound();
            }

            IngredientModel model = new IngredientModel()
            {   Id = ingredient.Id,
                Name = ingredient.Name,
                Photo = null
            };

            return View(model);
        }

        //Post
        [HttpPost]
        public async Task<IActionResult> Edit(IngredientModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Ingredient ingredient = new Ingredient()
                    {   Id = model.Id,
                        Name = model.Name,
                        Photo = SaveFile(model.Photo)
                    };

                    _context.Update(ingredient);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }

            return View(model);
        }

        //Get
        public async Task<IActionResult> Delete(int? id)
        {
            if (id ==null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingredients.FindAsync(id);

            if (ingredient==null)
            {
                return NotFound();
            }

            return View(ingredient);
        }

        //Post
        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }

            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient==null)
            {
                return NotFound();
            }

            _context.Remove(ingredient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
