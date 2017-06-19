using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Predrag_Djokic.Models;
using Predrag_Djokic.Repository;
namespace Predrag_Djokic.Controllers
{
    public class UplatnicaController : Controller
    {
        private IRepository<Racun> racunRepo = new RacunRepository();
        private IRepository<Uplatnica> uplatnicaRepo = new UplatnicaRepository();

        // GET: Uplatnica
        public ActionResult Index()
        {
            var uplatnice = uplatnicaRepo.GetAll();
            return View(uplatnice);
        }

        // GET: Uplatnica/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Uplatnica/Create
        public ActionResult Create( /* opet VM objekat fali*/)
        {

            /*
             vm.uplatnica = uplatnicaRepo.GetById();
            vm.racun = racunRepo.Get
             */
            return View();
        }

        // POST: Uplatnica/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Uplatnica/Edit/5
        public ActionResult Edit(int id)
        {
            var uplatnica = uplatnicaRepo.GetById(id);
            return View(uplatnica);
        }

        // POST: Uplatnica/Edit/5
        [HttpPost]
        public ActionResult Edit(/* ovde dodje ViewModel koji nisam napravio :) */)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Uplatnica/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Uplatnica/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
