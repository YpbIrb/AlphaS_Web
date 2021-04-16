using AlphaS_Web.Contexts;
using AlphaS_Web.Models;
using AlphaS_Web.Models.Utils;
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
            ViewBag.Modules = new SelectList(_modules.GetAll(), "ModuleId", "ModuleName");
            return View();
        }

        // POST: ExperimentsController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind("OperatorId, StartTime, FinishTime, Modules")] ExperimentViewModel experimentViewModel)
        {
            try
            {
                Experiment experiment = ExperimentFromViewModel(experimentViewModel);

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
        public async Task<ActionResult> AddModule(int id ,[Bind("Modules")] ExperimentViewModel experimentViewModel)
        {
            Console.WriteLine("IN ADD MODULE");

            int new_module_id = id;
            Module module = _modules.Find(new_module_id);
            string new_ModuleName = module.ModuleName;
            Dictionary<string, string> new_input_dictionary = module.InputVariables;
            List<MyKeyValuePair> new_input_pairs = new List<MyKeyValuePair>();
            foreach(KeyValuePair<string, string> e in new_input_dictionary)
            {
                new_input_pairs.Add(new MyKeyValuePair(e.Key, ""));
            }
            ModuleInExperimentViewModel new_moduleViewModel = new ModuleInExperimentViewModel(new_ModuleName, experimentViewModel.Modules.Count + 1, new_input_pairs);

            //ModuleInExperiment new_module = new ModuleInExperiment(new_ModuleName, experiment.Modules.Count + 1, new_OutputValues, new_InputValues);
            experimentViewModel.Modules.Add(new_moduleViewModel);
            Console.WriteLine("experiment.Modules count = " + experimentViewModel.Modules.Count);
            return PartialView("Modules", experimentViewModel);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> OpenVariables(int id)
        {
            int ModuleName = id;

            Console.WriteLine("IN OpenVariables. moduleId = " + ModuleName);
            Module module = _modules.Find(ModuleName);
            ViewBag.InputVariables = new List<string>();
            ModuleInExperiment moduleInExperiment = new ModuleInExperiment();
            List<string> variableNames = module.InputVariables.Keys.AsEnumerable().ToList();
            foreach(string e in variableNames)
            {
                Console.WriteLine(e);
                ViewBag.InputVariables.Add(e);
                moduleInExperiment.InputValues.Add(e, "");
            }
            
            

            //moduleInExperiment.InputValues.Add(new ModuleInExperiment());
            return PartialView("InputValues", moduleInExperiment);
        }

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
            foreach(MyKeyValuePair pair in moduleInExperimentViewModel.InputValues)
            {
                inputValues.Add(pair.Key, pair.Value);
            }
            res.InputValues = inputValues;
            return res;
        }




    }
}
