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
using What_Should_I_eat.Services.CuisineCRUD;
using What_Should_I_eat.Services.Global;

namespace What_Should_I_eat.Controllers
{
    public class CuisineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICreateService _createService;
        private readonly IEditService _editService;
        private readonly IDeleteService _deleteService;
        private readonly IAssignService _assignService;
        public CuisineController(
            ApplicationDbContext context,
            ICreateService createService,
            IEditService editService,
            IDeleteService deleteService,
            IAssignService assignService)
        
        {
            _context = context;
            _createService = createService;
            _editService = editService;
            _deleteService = deleteService;
            _assignService = assignService; 
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


        public IActionResult Create()
        {
           return _createService.Create();
           
        }

        //Post Create
        [HttpPost]
        public async Task<IActionResult> Create(CuisineModel model)
        {
            #region without DI
            //if (ModelState.IsValid)
            //{
            //    Cuisine cuisine = new Cuisine()
            //    {
            //        Name = model.Name,
            //        ContinentId = model.ContinentId,
            //        Description = model.Description,
            //        Photo = SaveFile(model.Photo)
            //    };

            //    _context.Add(cuisine);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}


            //return View(model);
            #endregion

            await _createService.Create(model);

            return RedirectToAction(nameof(Index));

        }

        // Get: Edit

        public async Task<IActionResult> Edit(int? id)
        {
           return await _editService.Edit(id);
        }

        // Post: Edit
        [HttpPost]

        public async Task<IActionResult> Edit(CuisineModel model)
        {
           return await _editService.Edit(model);
        }


        //Get Delete
        public async Task<IActionResult> Delete(int? id)
        {

            return await _deleteService.Delete(id);

        }

        //Post Delete
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {

           return await _deleteService.DeleteConfirmed(id);

        }


        public async Task<IActionResult> AssignDish(int cuisineId, int dishId) 
        {
          return await  _assignService.AssignDish(cuisineId, dishId);
        }


    }
}
