using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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


        //HTTP Get : All Cuisines
        public async Task<IActionResult> Index()
        {
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

            if (cuisine == null)
            {
                return NotFound();
            }


            return View(cuisine);

        }


        //Get Create

        public IActionResult Create()
        {
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
                    Description = model.Description,
                    Photo = SaveFile(model.Photo)
                };

                _context.Add(cuisine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            return View(model);
        }

        public static string SaveFile(IFormFile file)
        {

        }

    }
}
