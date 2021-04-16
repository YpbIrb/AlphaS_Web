using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaS_Web.Controllers
{
    public class ModuleTypesController : Controller
    {
        // GET: ModuleTypeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ModuleTypeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ModuleTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ModuleTypeController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: ModuleTypeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ModuleTypeController/Edit/5
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

        // GET: ModuleTypeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ModuleTypeController/Delete/5
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
    }
}
