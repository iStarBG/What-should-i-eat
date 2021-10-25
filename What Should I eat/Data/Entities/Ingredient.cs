using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace What_Should_I_eat.Data.Entities
{
    public class Ingredient:BaseEntity
    {

        public Ingredient()
        {
            this.DishIngredients = new HashSet<DishIngredient>();
        }

        public string Name { get; set; }
        public string Photo { get; set; }
        public virtual ICollection<DishIngredient> DishIngredients { get; set; }
    }
}
