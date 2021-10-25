using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace What_Should_I_eat.Data.Entities
{
    public class Dish:BaseEntity
    {
        public Dish()
        {
            this.CuisineDishes = new HashSet<CuisineDish>();
            this.DishIngredients = new HashSet<DishIngredient>();
        }


        public string Name { get; set; }
        public string Photo { get; set; }

        public virtual ICollection<CuisineDish> CuisineDishes { get; set; }

        public virtual ICollection<DishIngredient> DishIngredients { get; set; }


    }
}
