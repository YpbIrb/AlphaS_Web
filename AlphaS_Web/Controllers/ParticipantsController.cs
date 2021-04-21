using AlphaS_Web.Contexts;
using AlphaS_Web.Models;
using AlphaS_Web.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaS_Web.Controllers
{
    public class ParticipantsController : Controller
    {

        private readonly ParticipantContext _context;
        private SelectList Genders;

        public ParticipantsController (ParticipantContext participantService)
        {
            _context = participantService;
            Genders = new SelectList(new[] { "М", "Ж" });
        }


        // GET: ParticipantController
        public ActionResult Index()
        {
            return View(_context.Get());
        }

        // GET: ParticipantController/Details/5
        public ActionResult Details(int id)
        {
            Participant part = _context.Find(id);
            return View(part);
        }

        // GET: ParticipantController/Create
        public ActionResult Create()
        {
            
            ViewBag.Genders = Genders;
            return View();
        }

        // POST: ParticipantController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Birth_Date, Gender, Nationality, AdditionalInfo")] ParticipantCreateRequest participantCR)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Create(participantCR);
                } 
                else
                {
                    ViewBag.Genders = Genders;
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Genders = Genders;
                return View();
            }
        }

        // GET: ParticipantController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Genders = Genders;
            Participant part = _context.Find(id);
            return View(part);
        }

        // POST: ParticipantController/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Birth_Date, Gender, Nationality, AdditionalInfo")] ParticipantCreateRequest participantCR)
        {

            Console.WriteLine("In Edit with ParticipantCreateRequest. id = " + id);

            try
            {
                //Participant part = 
                Console.WriteLine("Additional : " + participantCR.AdditionalInfo); 
                _context.Update(id, participantCR);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                ViewBag.Genders = Genders;
                Console.WriteLine(e.Message);
                return View();
            }
        }

        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Birth_Date, Gender, Nationality, AdditionalInfo")] Participant participant)
        {

            Console.WriteLine("In Edit with Participant. id = " + id);

            try
            {
                ParticipantCreateRequest participantCreateRequest = new ParticipantCreateRequest(participant.Birth_Date, participant.Gender, participant.Nationality, participant.AddtitionalInfo);
                Participant part = _context.Update(id, participantCreateRequest);

                return View(id);
            }
            catch
            {
                return View();
            }
        }
        */


        // GET: ParticipantController/Delete/5
        public ActionResult Delete(int id)
        {
            
            return View(_context.Find(id));
        }

        // POST: ParticipantController/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _context.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
