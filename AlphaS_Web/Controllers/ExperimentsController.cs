using AlphaS_Web.Contexts;
using AlphaS_Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaS_Web.Controllers
{
    public class ExperimentsController : Controller
    {
        ExperimentContext _context;
        ModuleContext _modules;

        public ExperimentsController(ExperimentContext experimenContext, ModuleContext modules)
        {
            _context = experimenContext;
            _modules = modules;
        }

        // GET: ExperimentsController
        public ActionResult Index()
        {

            return View(_context.GetAll());
        }

        // GET: ExperimentsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ExperimentsController/Create
        public ActionResult Create()
        {
            ViewBag.Modules = new SelectList(_modules.GetAll(), "ModuleName", "ModuleName");
            return View();
        }

        // POST: ExperimentsController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind("OperatorId, StartTime, FinishTime, Modules")] Experiment experiment)
        {
            try
            {
                _context.Create(experiment);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ExperimentsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ExperimentsController/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ExperimentsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ExperimentsController/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> AddModule([Bind("Modules")] Experiment experiment)
        {
            Console.WriteLine("IN ADD MODULE");
            ViewBag.Modules = new SelectList(_modules.GetAll(), "ModuleName", "ModuleName");

            ModuleInExperiment new_module = new ModuleInExperiment(experiment.Modules.Count + 1);
            experiment.Modules.Add(new_module);
            //experiment.Modules.Add(new ModuleInExperiment());
            Console.WriteLine("experiment.Modules count = " + experiment.Modules.Count);
            return PartialView("Modules", experiment);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> OpenVariables([Bind("ModuleName, InputVariables")] ModuleInExperiment moduleInExperiment)
        {
            Console.WriteLine("IN OpenVariables");
            Module module = _modules.Find(moduleInExperiment.ModuleName);

            List<string> variableNames = module.InputVariables.Keys.AsEnumerable().ToList();
            foreach(string e in variableNames)
            {
                moduleInExperiment.InputValues.Add(e, "");
            }
            
            //ViewBag.InputVariables = new SelectList(variableNames, "InputVariable", "InputVariable");

            //moduleInExperiment.InputValues.Add(new ModuleInExperiment());
            return PartialView("InputValues", moduleInExperiment);
        }


    }
}
