using AlphaS_Web.Areas.Identity.Data;
using AlphaS_Web.Contexts;
using AlphaS_Web.Models;
using AlphaS_Web.Models.Utils;
using AlphaS_Web.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        ExperimentPresetContext _presets;
        private readonly UserManager<ApplicationUser> _userManager;


        public ExperimentsController(ExperimentContext experimenContext, ModuleContext modules, ExperimentPresetContext presets, UserManager<ApplicationUser> userManager)
        {
            _context = experimenContext;
            _modules = modules;
            _presets = presets;
            _userManager = userManager;
        }

        // GET: ExperimentsController
        public ActionResult Index()
        {
            ViewBag.Presets = new SelectList(_presets.GetAll(), "PresetName", "PresetName");
            return View(_context.GetAll());
        }

        public ActionResult FilterByPreset(string id)
        {

            return View(_context.GetAll().Where(exp => exp.PresetName == id));
        }


        // GET: ExperimentsController/Details/5
        public ActionResult Details(int id)
        {

            return View(_context.Find(id));
        }

        // GET: ExperimentsController/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Modules = new SelectList(_modules.GetAll(), "ModuleId", "ModuleName");
            ViewBag.Presets = new SelectList(_presets.GetAll(), "PresetName", "PresetName");
            return View();
        }
        
        // POST: ExperimentsController/Create
        [HttpPost]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind("PresetName, Modules")] ExperimentViewModel experimentViewModel)
        {
            try
            {
                int user_id = _userManager.GetUserAsync(HttpContext.User).Result.UserId;

                Experiment experiment = ViewModelConverter.ExperimentFromViewModel(experimentViewModel, _modules);

                experiment.OperatorId = user_id;

                _context.Create(experiment);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Presets = new SelectList(_presets.GetAll(), "PresetName", "PresetName");
                ViewBag.Modules = new SelectList(_modules.GetAll(), "ModuleId", "ModuleName");
                return View();
            }
        }

        // GET: ExperimentsController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ExperimentsController/Edit/5
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

        // GET: ExperimentsController/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            return View(_context.Find(id));
        }

        // POST: ExperimentsController/Delete/5
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
        public async Task<ActionResult> AddModule(int id ,[Bind("Modules")] ExperimentViewModel experimentViewModel)
        {
            Console.WriteLine("IN AddModule");

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
            ModuleInExperimentViewModel new_moduleViewModel = new ModuleInExperimentViewModel(new_ModuleName, experimentViewModel.Modules.Count + 1, new_input_pairs, new_output_pairs);

            //ModuleInExperiment new_module = new ModuleInExperiment(new_ModuleName, experiment.Modules.Count + 1, new_OutputValues, new_InputValues);
            experimentViewModel.Modules.Add(new_moduleViewModel);
            Console.WriteLine("experiment.Modules count = " + experimentViewModel.Modules.Count);
            return PartialView("Modules", experimentViewModel);
        }

        public async Task<ActionResult> AddPreset(string id, [Bind("Modules")] ExperimentViewModel experimentViewModel)
        {
            Console.WriteLine("IN AddPreset");

            experimentViewModel.PresetName = id;
            ExperimentPreset preset = _presets.Find(id);

            foreach(ModuleInExperiment module in preset.Modules)
            {

                experimentViewModel.Modules.Add(ViewModelConverter.ViewModelToModuleInExperiment(module));
            }

            Console.WriteLine("experiment.Modules count = " + experimentViewModel.Modules.Count);
            return PartialView("Modules", experimentViewModel);
        }
    }
}
