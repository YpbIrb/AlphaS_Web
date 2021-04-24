
using AlphaS_Web.Contexts;
using AlphaS_Web.Models;
using AlphaS_Web.Models.Utils;
using AlphaS_Web.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaS_Web.Controllers
{
    
    public class ExperimentPresetsController : Controller
    {
        ExperimentPresetContext _context;
        ModuleContext _modules;

        public ExperimentPresetsController(ExperimentPresetContext experimenPresetContext, ModuleContext modules)
        {
            _context = experimenPresetContext;
            _modules = modules;
        }




        // GET: ExperimentPresetsController
        public ActionResult Index()
        {
            return View(_context.GetAll());
        }

        // GET: ExperimentPresetsController/Details/5
        public ActionResult Details(string id)
        {
            return View(_context.Find(id));
        }

        // GET: ExperimentPresetsController/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Modules = new SelectList(_modules.GetAll(), "ModuleId", "ModuleName");
            return View();
        }

        // POST: ExperimentPresetsController/Create
        [HttpPost]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind("PresetName, Modules")] ExperimentPresetViewModel experimentPresetViewModel)
        {
            try
            {
                ExperimentPreset experimentPreset = ViewModelConverter.PresetFromViewModel(experimentPresetViewModel, _modules);
                _context.Create(experimentPreset);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //todo нормальный отлов ошибки, и предупреждение о том, что не правильно
                ViewBag.Modules = new SelectList(_modules.GetAll(), "ModuleId", "ModuleName");
                return View();
            }
        }

        // GET: ExperimentPresetsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ExperimentPresetsController/Edit/5
        [HttpPost]
        [Authorize]
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

        // GET: ExperimentPresetsController/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ExperimentPresetsController/Delete/5
        [HttpPost]
        [Authorize]
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
        public async Task<ActionResult> AddModule(int id, [Bind("Modules")] ExperimentPresetViewModel experimentPresetViewModel)
        {
            Console.WriteLine("IN ADD MODULE");

            int new_module_id = id;
            Module module = _modules.Find(new_module_id);
            string new_ModuleName = module.ModuleName;
            List<ModuleVariable> new_input_variables = module.InputVariables;
            List<ModuleVariable> new_output_variables = module.OutputVariables;
            List<MyKeyValuePair> new_input_pairs = new List<MyKeyValuePair>();
            List<MyKeyValuePair> new_output_pairs = new List<MyKeyValuePair>();
            foreach (ModuleVariable e in new_input_variables)
            {
                new_input_pairs.Add(new MyKeyValuePair(e.VariableName, e.VariableDefaultValue));
            }
            foreach (ModuleVariable e in new_output_variables)
            {
                new_output_pairs.Add(new MyKeyValuePair(e.VariableName, e.VariableDefaultValue));
            }
            ModuleInExperimentViewModel new_moduleViewModel = new ModuleInExperimentViewModel(new_ModuleName, experimentPresetViewModel.Modules.Count + 1, new_input_pairs, new_output_pairs);

            //ModuleInExperiment new_module = new ModuleInExperiment(new_ModuleName, experiment.Modules.Count + 1, new_OutputValues, new_InputValues);
            experimentPresetViewModel.Modules.Add(new_moduleViewModel);
            Console.WriteLine("experiment.Modules count = " + experimentPresetViewModel.Modules.Count);
            return PartialView("Modules", experimentPresetViewModel);
        }

    }
}
