using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using What_Should_I_eat.Data.Entities;

namespace What_Should_I_eat.Models
{
    public class CuisineModel:BaseModel
    {
        public CuisineModel()
        {
            CuisineDishes = new HashSet<CuisineDish>();
            Dishes = new HashSet<Dish>();
        }
        public string Name { get; set; }

        public string Description { get; set; }

        public IFormFile Photo { get; set; }

        public Continent Continent { get; set; }

        public ICollection<CuisineDish> CuisineDishes { get; set; }
        public ICollection<Dish> Dishes { get; set; }
    }
}
