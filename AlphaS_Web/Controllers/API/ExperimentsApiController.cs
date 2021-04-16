using AlphaS_Web.Contexts;
using AlphaS_Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaS_Web.Controllers.API
{
    [Route("api/Experiments")]
    [ApiController]
    public class ExperimentsApiController
    {
        private readonly ExperimentContext _context;

        public ExperimentsApiController(ExperimentContext experimentContext)
        {
            _context = experimentContext;
        }

        [HttpGet]
        public ActionResult<List<Experiment>> GetAll() => _context.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Experiment> Find(int id)
        {
            var res = _context.Find(id);

            if (res == null)
            {
                return new NotFoundResult();
            }

            return res;
        }

        [HttpPost("Create")]
        public ActionResult<Experiment> Create([FromBody] Experiment experiment)
        {

            Experiment res = _context.Create(experiment);
            return res;
        }

        [HttpPost("Update/{id}")]
        public ActionResult<Experiment> Update(int id, [FromBody] Experiment experiment)
        {

            Experiment OldExp = _context.Find(id);
            

            if (OldExp == null)
            {
                return new NotFoundResult();
            }
            else
            {
                int old_id = OldExp.ExperimentId;
                experiment.ExperimentId = old_id;
                Experiment res = _context.Update(id, experiment);
                return res;
            }
            
            
        }



    }
}
