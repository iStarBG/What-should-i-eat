using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using What_Should_I_eat.Data.Entities;

namespace What_Should_I_eat.Models
{
    public class DishModel: BaseModel
    {
        public string Name { get; set; }
        public IFormFile Photo { get; set; }

    }
}
