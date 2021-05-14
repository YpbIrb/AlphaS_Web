using AlphaS_Web.Contexts;
using AlphaS_Web.Models;
using AlphaS_Web.Models.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
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
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ModulesController/Create
        [HttpPost]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ModuleName, ModuleTypeName, PathToExe, Description, InputVariables, OutputVariables")] Module module)
        {
            try
            {
                //Module module = GetModuleFromModuleFromPage(moduleFromPage);


                Console.WriteLine("Adding Module ModuleName : " + module.ModuleName);

                Module new_module = _context.Create(module);
                return RedirectToAction("Details", new { id = new_module.ModuleId});
            }
            catch(MongoWriteException e)
            {
                Console.WriteLine(e.Message);
                if (e.WriteError.Category == ServerErrorCategory.DuplicateKey)
                {
                    if (e.WriteError.Message.Contains("Module_Name"))
                    {
                        ModelState.AddModelError("", "Модуль с таким названием уже существует. Используйте другое.");
                    }
                    if (e.WriteError.Message.Contains("Path_To_Exe"))
                    {
                        ModelState.AddModelError("", "Модуль с таким названием файла уже существует. Используйте другое");
                    }
                }
                return View(module);
            }
        }

        /*

        [Authorize]
        // GET: ModulesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ModulesController/Edit/5
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


        // GET: ModulesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ModulesController/Delete/5
        [HttpPost]
        [Authorize]
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
        */
       
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

    }
}
