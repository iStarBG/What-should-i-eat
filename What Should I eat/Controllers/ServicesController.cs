using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using What_Should_I_eat.Data;

namespace What_Should_I_eat.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext _context;


        public ServicesController(ApplicationDbContext context)
        {
            _context = context;
        }





    }
}
