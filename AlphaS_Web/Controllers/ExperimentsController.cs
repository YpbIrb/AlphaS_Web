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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaS_Web.Controllers
{
    public class ExperimentsController : Controller
    {
        ExperimentContext _context;
        ModuleContext _modules;
        ExperimentPresetContext _presets;
        ParticipantContext _participants;
        private readonly UserManager<ApplicationUser> _userManager;


        public ExperimentsController(ExperimentContext experimenContext, ModuleContext modules, ExperimentPresetContext presets, ParticipantContext participants, UserManager<ApplicationUser> userManager)
        {
            _context = experimenContext;
            _modules = modules;
            _presets = presets;
            _userManager = userManager;
            _participants = participants;
        }

        // GET: ExperimentsController
        public ActionResult Index()
        {
            ViewBag.Presets = new SelectList(_presets.GetAll(), "PresetName", "PresetName");
            return View(_context.GetAll());
        }

        public ActionResult FilterByPreset(string id)
        {
            ViewBag.PresetName = id;
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

                Experiment exp = _context.Create(experiment);

                return RedirectToAction("Details", new { id = exp.ExperimentId });
            }
            catch
            {
                ViewBag.Presets = new SelectList(_presets.GetAll(), "PresetName", "PresetName");
                ViewBag.Modules = new SelectList(_modules.GetAll(), "ModuleId", "ModuleName");
                return View();
            }
        }

        [HttpGet]
        public ActionResult DownloadCsvByPreset(string id)
        {

            ExperimentPreset preset = _presets.Find(id);
            List<Experiment> experiments = _context.GetAll().Where(exp => exp.PresetName == id).ToList();

            byte[] byteArr = Encoding.UTF8.GetBytes(CSVConverter.GetCSVForPresetExperiments(preset, experiments));
            string mimeType = "text/plain";
            return new FileContentResult(byteArr, mimeType)
            {
                FileDownloadName = id + ".txt"
            };
        }


        [HttpGet]
        public ActionResult DownloadCsvWithParticipantsByPreset(string id)
        {


            ExperimentPreset preset = _presets.Find(id);
            List<Experiment> experiments = _context.GetAll().Where(exp => exp.PresetName == id).ToList();
            List<Participant> participants = _participants.Get();

            List<ExperimentWithParticipantsInfo> experimentWithParticipants = new List<ExperimentWithParticipantsInfo>();

            foreach (Experiment experiment in experiments)
            {
                ExperimentWithParticipantsInfo experimentWithParticipantsInfo = new ExperimentWithParticipantsInfo();
                experimentWithParticipantsInfo.experiment = experiment;
                experimentWithParticipantsInfo.firstParticipant = participants.Find(part => part.ParticipantId == experiment.FirstParticipant.ParticipantId);
                experimentWithParticipantsInfo.secondParticipant = participants.Find(part => part.ParticipantId == experiment.SecondParticipant.ParticipantId);
                experimentWithParticipants.Add(experimentWithParticipantsInfo);
            }

            byte[] byteArr = Encoding.UTF8.GetBytes(CSVConverter.GetCSVWithParticipantsForPresetExperiments(preset, experimentWithParticipants));
            string mimeType = "text/plain";
            return new FileContentResult(byteArr, mimeType)
            {
                FileDownloadName = id + "_WithParticipants.txt"
            };
        }

        // GET: ExperimentsController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            return View(_context.Find(id));
        }

        // POST: ExperimentsController/Edit/5
        [HttpPost]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("ExperimentId, OperatorId, StartTime, FinishTime, FirstParticipant, SecondParticipant, PresetName, Modules")] Experiment experiment)
        {
            Console.WriteLine("In Edit Experiment. id = " + id);
            try
            {
                _context.Update(id, experiment);
                return RedirectToAction("Details", new { id = id });
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
