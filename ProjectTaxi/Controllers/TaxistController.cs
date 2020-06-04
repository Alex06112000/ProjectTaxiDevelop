using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnionApp.Domain.Core;
using ProjectTaxi.Repositories;


namespace ProjectTaxi.Controllers
{
    
    public class TaxistController : Controller
    {
        UnitOfWork unitOfWork;

        public TaxistController()
        {
            unitOfWork = new UnitOfWork();
        }

        public ActionResult Index()
        {
            return View(unitOfWork.Taxists.GetList());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Taxist taxist)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Taxists.Create(taxist);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(taxist);
        }

        public ActionResult Edit(int id)
        {
            Taxist taxist = unitOfWork.Taxists.Get(id);
            return View(taxist);
        }
        [HttpPost]
        public ActionResult Edit(Taxist taxist)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Taxists.Update(taxist);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(taxist);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Taxist b = unitOfWork.Taxists.Get(id);
            return View(b);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            unitOfWork.Taxists.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
