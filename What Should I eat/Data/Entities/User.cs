using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace What_Should_I_eat.Data.Entities
{
    public class User:BaseEntity
    {
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
