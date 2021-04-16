using AlphaS_Web.Contexts;
using AlphaS_Web.Models;
using AlphaS_Web.Models.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaS_Web.Controllers
{
    public class ModulesController : Controller
    {

        private readonly ModuleContext _context;

        public ModulesController(ModuleContext moduleContext)
        {
            _context = moduleContext;
        }

        // GET: ModulesController
        public ActionResult Index()
        {
            
            return View(_context.GetAll());
        }

        // GET: ModulesController/Details/5
        public ActionResult Details(int id)
        {
            return View(_context.Find(id));
        }

        // GET: ModulesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ModulesController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ModuleName, ModuleTypeName, PathToExe, Description, InputVariables, OutputVariables")] Module module)
        {
            try
            {
                //Module module = GetModuleFromModuleFromPage(moduleFromPage);


                Console.WriteLine("Adding Module ModuleName : " + module.ModuleName);

                _context.Create(module);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ModulesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ModulesController/Edit/5
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

        // GET: ModulesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ModulesController/Delete/5
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
        public async Task<ActionResult> AddInputVariable([Bind("InputVariables")] Module module)
        {
            module.InputVariables.Add(new ModuleVariable());
            Console.WriteLine("module.InputVariables count = " + module.InputVariables.Count);
            return PartialView("InputVariables", module);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> AddOutputVariable([Bind("OutputVariables")] Module module)
        {
            module.OutputVariables.Add(new ModuleVariable());
            return PartialView("OutputVariables", module);
        }

        /*
        private Module GetModuleFromModuleFromPage(ModuleFromPage moduleFromPage)
        {
            Module res = new Module(moduleFromPage.ModuleId, moduleFromPage.ModuleName, 
                                    moduleFromPage.ModuleTypeName, moduleFromPage.PathToExe, moduleFromPage.Description);


            foreach(MyKeyValuePair pair in moduleFromPage.InputVariables)
            {
                res.InputVariables.Add(pair.Key, pair.Value);
            }

            foreach (MyKeyValuePair pair in moduleFromPage.OutputVariables)
            {
                res.OutputVariables.Add(pair.Key, pair.Value);
            }

            return res;
        }
        */
    }
}
