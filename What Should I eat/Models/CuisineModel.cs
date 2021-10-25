using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace What_Should_I_eat.Models
{
    public class CuisineModel:BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Photo { get; set; }
    }
}
