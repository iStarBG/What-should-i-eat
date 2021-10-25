using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace What_Should_I_eat.Data.Entities
{
    public class DishIngredient
    {
        public int DishId { get; set; }
        public virtual Dish Dish { get; set; }


        public int IngredientId { get; set; }
        public virtual Ingredient Ingredient  { get; set; }

    }
}
