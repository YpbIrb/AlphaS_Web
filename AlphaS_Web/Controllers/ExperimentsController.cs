using AlphaS_Web.Contexts;
using AlphaS_Web.Models;
using AlphaS_Web.Models.Utils;
using AlphaS_Web.Utils;
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
        ExperimentPresetContext _presets;


        public ExperimentsController(ExperimentContext experimenContext, ModuleContext modules, ExperimentPresetContext presets)
        {
            _context = experimenContext;
            _modules = modules;
            _presets = presets;
        }

        // GET: ExperimentsController
        public ActionResult Index()
        {

            return View(_context.GetAll());
        }

        // GET: ExperimentsController/Details/5
        public ActionResult Details(int id)
        {

            return View(_context.Find(id));
        }

        // GET: ExperimentsController/Create
        public ActionResult Create()
        {
            ViewBag.Modules = new SelectList(_modules.GetAll(), "ModuleId", "ModuleName");
            ViewBag.Presets = new SelectList(_presets.GetAll(), "PresetName", "PresetName");
            return View();
        }

        // POST: ExperimentsController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind("OperatorId, StartTime, FinishTime, Modules")] ExperimentViewModel experimentViewModel)
        {
            try
            {
                Experiment experiment = ViewModelConverter.ExperimentFromViewModel(experimentViewModel, _modules);

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
            return View(_context.Find(id));
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

            
            ExperimentPreset preset = _presets.Find(id);

            foreach(ModuleInExperiment module in preset.Modules)
            {

                experimentViewModel.Modules.Add(ViewModelConverter.ViewModelToModuleInExperiment(module));
            }

            Console.WriteLine("experiment.Modules count = " + experimentViewModel.Modules.Count);
            return PartialView("Modules", experimentViewModel);
        }



        /*
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> OpenVariables(int id)
        {
            int ModuleName = id;

            Console.WriteLine("IN OpenVariables. moduleId = " + ModuleName);
            Module module = _modules.Find(ModuleName);
            ViewBag.InputVariables = new List<string>();
            ModuleInExperiment moduleInExperiment = new ModuleInExperiment();
            List<ModuleVariable> input_variables = module.InputVariables;
            foreach(ModuleVariable e in input_variables)
            {
                Console.WriteLine(e);
                ViewBag.InputVariables.Add(e);
                moduleInExperiment.InputValues.Add(e, "");
            }
            //moduleInExperiment.InputValues.Add(new ModuleInExperiment());
            return PartialView("InputValues", moduleInExperiment);
        }
        */
        /*
        private Experiment ExperimentFromViewModel(ExperimentViewModel experimentViewModel)
        {
            Experiment res = new Experiment();
            res.OperatorId = experimentViewModel.OperatorId;
            List<ModuleInExperiment> modules = new List<ModuleInExperiment>();
            foreach(ModuleInExperimentViewModel e in experimentViewModel.Modules)
            {
                ModuleInExperiment new_module = ModuleinExperimentFromViewModel(e);
                modules.Add(new_module);
            }
            res.Modules = modules;
            return res;
        }


        private ModuleInExperiment ModuleinExperimentFromViewModel(ModuleInExperimentViewModel moduleInExperimentViewModel)
        {
            ModuleInExperiment res = new ModuleInExperiment();
            res.ModuleName = moduleInExperimentViewModel.ModuleName;
            res.ModuleOrder = moduleInExperimentViewModel.ModuleOrder;
            Dictionary<string, string> inputValues = new Dictionary<string, string>();
            Dictionary<string, string> outputValues = new Dictionary<string, string>();
            foreach (MyKeyValuePair pair in moduleInExperimentViewModel.InputValues)
            {
                inputValues.Add(pair.Key, pair.Value);
            }

            List<ModuleVariable> outputVariables = _modules.Find(moduleInExperimentViewModel.ModuleName).OutputVariables;
            foreach (ModuleVariable e in outputVariables)
            {
                outputValues.Add(e.VariableName, e.VariableDefaultValue);
            }

            res.InputValues = inputValues;
            res.OutputValues = outputValues;
            return res;
        }
        */



    }
}
