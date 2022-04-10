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
        }


        public string Name { get; set; }
        public string Photo { get; set; }

        public virtual ICollection<CuisineDish> CuisineDishes { get; set; }



    }
}
