using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace What_Should_I_eat.Data.Entities
{
    public class Cuisine : BaseEntity
    {
        public Cuisine()
        {
            this.CuisineDishes = new HashSet<CuisineDish>();
        }

        public string Name { get; set; }

        public int ContinentId { get; set; }

        public virtual Continent Continent { get; set; }

        public virtual ICollection<CuisineDish> CuisineDishes { get; set; }

        public string Description { get; set; }

        public string Photo { get; set; }

    }
}
