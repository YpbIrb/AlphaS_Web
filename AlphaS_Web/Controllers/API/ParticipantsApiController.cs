using AlphaS_Web.Contexts;
using AlphaS_Web.Models;
using AlphaS_Web.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaS_Web.Controllers.API
{
    [Route("api/Participants")]
    [ApiController]
    public class ParticipantsApiController : Controller
    {

        private readonly ParticipantContext _context;

        public ParticipantsApiController(ParticipantContext participantService)
        {
            _context = participantService;
        }

        //https://localhost:5001/api/Participants

        [HttpGet]
        public ActionResult<List<Participant>> GetAll() => _context.Get();

        [HttpGet("{id}", Name = "GetParticipant")]
        public ActionResult<Participant> Find(int id)
        {
            var res = _context.Find(id);

            if (res == null)
            {
                return NotFound();
            }

            return res;
        }

        [HttpPost("Create")]
        public ActionResult<Participant> Create([FromBody] ParticipantCreateRequest participantCR)
        {

            Participant res = _context.Create(participantCR);
            return res;
        }


    }
}
