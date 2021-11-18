using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Http;

namespace What_Should_I_eat.Models
{
    public class IngredientModel:BaseModel
    {

        public string Name { get; set; }

        public IFormFile Photo { get; set; }




    }
}
