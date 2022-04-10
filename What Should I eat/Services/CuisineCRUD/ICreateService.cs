using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public interface ICreateService
    {
        IActionResult Create();
        Task<IActionResult> Create(CuisineModel model);
    }

    public class CreateService : Controller, ICreateService
    {

        private ApplicationDbContext _context;
        private readonly ISaveFileService _saveFile;


        public CreateService(ApplicationDbContext context, ISaveFileService saveFile)
        {
            _context = context;
            _saveFile = saveFile;
        }

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

        public async Task<IActionResult> Create(CuisineModel model)
        {

            if (ModelState.IsValid)
            {
                Cuisine cuisine = new Cuisine()
                {
                    Name = model.Name,
                    ContinentId = model.ContinentId,
                    Description = model.Description,
                    Photo = _saveFile.SaveFile(model.Photo)
                };

                _context.Add(cuisine);
                await _context.SaveChangesAsync();
                
            }


            return View(model);
        }

    }
}
