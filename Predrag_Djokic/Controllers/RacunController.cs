using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Predrag_Djokic.Repository;
using Predrag_Djokic.Models;

namespace Predrag_Djokic.Controllers
{
    public class RacunController : Controller
    {
        private IRepository<Racun> racunRepo = new RacunRepository();
        private IRepository<Uplatnica> uplatnicaRepo = new UplatnicaRepository();
        // GET: Racun
        public ActionResult Index()
        {
            var racuni = racunRepo.GetAll();
            return View(racuni);

        }

        //ToggleStatus
        public ActionResult ToggleStatus(int id)
        {
            var racun = racunRepo.GetById(id);
            racun.Aktivan = !racun.Aktivan;
            racunRepo.Update(racun);

            return RedirectToAction("Index");
        }

        //rad sa Racunima (= otvori link)

        public ActionResult Details(int id)
        {
            var racun = racunRepo.GetById(id);
            return View(racun);
        }

        // GET: Racun/Create
        public ActionResult Create()
        {
            return View("_Create", new Racun());
        }

        // POST: Racun/Create
        [HttpPost]
        public ActionResult Create(Racun racun)
        {
            if (ModelState.IsValid)
            {
                if (racunRepo.Create(racun))
                {
                   
                    return RedirectToAction("Index");
                } 
            }
            return View(racun);
        }

        // GET: Racun/Edit/5
        public ActionResult Edit(int id)
        {
            var racun = racunRepo.GetById(id);
            return View(racun);
        }

        // POST: Racun/Edit/5
        [HttpPost]
        public ActionResult Edit(Racun racun)
        {
            if (ModelState.IsValid)
            {
                racunRepo.Update(racun);
                return RedirectToAction("Index");
            }
            return View(racun);
        }

        //// GET: Racun/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // Trebalom bi da se brisanje radi sa HttPost ali nemam vremena
        //da ubacujem form umesto Http.ActionLink, jer komplikuje dalje Razor ispis
        // u datoj situaciji

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var racun = racunRepo.GetById(id);

            if (TryValidateModel(racun))
            //if (ModelState.IsValid)
            {
                racunRepo.Delete(racun.Id);
                return RedirectToAction("Index");
            }

            return View(racun);
        }
    }
}
