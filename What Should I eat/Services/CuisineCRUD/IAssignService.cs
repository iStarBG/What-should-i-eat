using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using What_Should_I_eat.Data;
using What_Should_I_eat.Data.Entities;

namespace What_Should_I_eat.Services.CuisineCRUD
{
    public interface IAssignService
    {
        Task<IActionResult> AssignDish(int cuisineId, int dichId);
    }


    public class AssignService : Controller, IAssignService
    {

        private readonly ApplicationDbContext _context;

        public AssignService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AssignDish(int cuisineId, int dishId)
        {
            var cuisine = await _context.Cuisines.FindAsync(cuisineId);

            if (cuisine != null)
            {
                var dish = await _context.Dishes.FindAsync(dishId);

                if (dish != null)
                {
                    if (cuisine.CuisineDishes.
                        Where(item => item.CuisineId == cuisineId && item.DishId == dishId).
                        Count() == 0)
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
