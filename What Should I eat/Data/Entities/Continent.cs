using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace What_Should_I_eat.Data.Entities
{
    public class Continent:BaseEntity
    {

        public Continent()
        {
            this.Cuisines = new HashSet<Cuisine>();
        }

        public string Name { get; set; }
        public string Photo { get; set; }

        public virtual  ICollection<Cuisine> Cuisines { get; set; }
    }
}
