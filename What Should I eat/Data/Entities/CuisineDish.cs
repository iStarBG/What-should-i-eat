using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace What_Should_I_eat.Data.Entities
{
    public class CuisineDish:BaseEntity
    {
        public int CuisineId { get; set; }
        public virtual Cuisine Cuisine { get; set; }

        public int DishId { get; set; }
        public virtual Dish Dish { get; set; }

    }
}
