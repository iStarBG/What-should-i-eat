using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using What_Should_I_eat.Data;
using What_Should_I_eat.Data.Entities;
using What_Should_I_eat.Models;
using What_Should_I_eat.Services.Global;

namespace What_Should_I_eat.Services.CuisineCRUD
{
    public interface IEditService
    {   
        Task<IActionResult> Edit(int? id);
        Task<IActionResult> Edit(CuisineModel model);

    }


    class EditService : Controller, IEditService
    { 
        private readonly ApplicationDbContext _context;
        private readonly ISaveFileService _saveFile;

        public EditService(ApplicationDbContext context, ISaveFileService saveFile)
        {
            _context = context;
            _saveFile = saveFile;
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuisine = await _context.Cuisines.FindAsync(id);

            if (cuisine == null)
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


        public async Task<IActionResult> Edit(CuisineModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Cuisine cuisine = new Cuisine()
                    {
                        Id = model.Id,
                        Name = model.Name,
                        ContinentId = model.ContinentId,
                        Description = model.Description,
                        Photo = _saveFile.SaveFile(model.Photo)
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
    }


}
