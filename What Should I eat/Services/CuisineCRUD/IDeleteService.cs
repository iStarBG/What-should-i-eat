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
    public interface IDeleteService
    {

        Task<IActionResult> Delete(int? id);
        Task<IActionResult> DeleteConfirmed(int? id);
    }

    public class DeleteService : Controller, IDeleteService
    {
        private readonly ApplicationDbContext _context;
        private readonly ISaveFileService _saveFile;

        public DeleteService(ApplicationDbContext context, ISaveFileService saveFile)
        {
            _context = context;
            _saveFile = saveFile;
        }


        public async Task<IActionResult> Delete(int? id)
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

        //Post Delete
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {

            var cuisine = await _context.Cuisines.FindAsync(id);

            _context.Remove(cuisine);

            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }




    }


}
