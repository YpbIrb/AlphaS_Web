using AlphaS_Web.Contexts;
using AlphaS_Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaS_Web.Controllers.API
{
    [Route("api/Modules")]
    [ApiController]
    public class ModuleApiController : Controller
    {
        private readonly ModuleContext _context;

        public ModuleApiController(ModuleContext experimentContext)
        {
            _context = experimentContext;
        }

        [HttpGet]
        public ActionResult<List<Module>> GetAll() => _context.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Module> Find(string id)
        {
            var res = _context.Find(id);

            if (res == null)
            {
                return new NotFoundResult();
            }

            return res;
        }

    }
}
