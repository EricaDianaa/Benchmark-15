using Benchmark_15.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Benchmark_15.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }
        //Create
        public ActionResult CreateCliente()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCliente(Clienti c)
        {
            Clienti.Insert(c, ViewBag.Message = "Errore nel compilamento");
            return RedirectToAction("Index", "Home");
        }
        public ActionResult CreateCamera()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCamera(Camere c)
        {
            Camere.Insert(c, ViewBag.Message = "Errore nel compilamento");
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public ActionResult CreatePrenotazione()
        {
            Prenotazione.ListCamere.Clear();
            Prenotazione.SelectCamera();
            Prenotazione.Dropdown1();
            ViewBag.drop2 = Prenotazione.DropdownCamere;
            Prenotazione.ListClienti.Clear();
            Prenotazione.DropdownClienti.Clear();
            Prenotazione.Dropdown();
            ViewBag.drop1 = Prenotazione.DropdownClienti;
            return View();
        }
        [HttpPost]
        public ActionResult CreatePrenotazione(Prenotazione p, int Cliente,int Camera)
        {
            Prenotazione.ListCamere.Clear();
            Prenotazione.SelectCamera();
            Prenotazione.Dropdown1();
            ViewBag.drop2 = Prenotazione.DropdownCamere;
            Prenotazione.ListClienti.Clear();
            Prenotazione.DropdownClienti.Clear();
            Prenotazione.Dropdown();
            ViewBag.drop1 = Prenotazione.DropdownClienti;
            if (ModelState.IsValid)
            {
                Prenotazione.Insert(p, ViewBag.Message = "Errore nel compilamento",Cliente,Camera);
            }
                return RedirectToAction("Index", "Home");
        }
        public ActionResult CreateServizi()
        {
            Servizi.ListPrenotazione.Clear();
            Servizi.DropdownTipoServizio.Clear();
            Servizi.SelectTipoServizio();
            Servizi.Dropdown();
            ViewBag.drop3 = Servizi.DropdownTipoServizio;
            return View();
        }
        [HttpPost]
        public ActionResult CreateServizi(Servizi s, int TipoServizio)
        {

            Servizi.ListPrenotazione.Clear();
            Servizi.DropdownTipoServizio.Clear();
            Servizi.SelectTipoServizio();
            Servizi.Dropdown();
            ViewBag.drop3 = Servizi.DropdownTipoServizio;
            if (ModelState.IsValid)
            {
                Servizi.Insert(s, ViewBag.Message = "Errore nel compilamento", TipoServizio);
            }
            return RedirectToAction("Index", "Home");
        }

        //Select
        public ActionResult SelectClienti()
        {
            Clienti.ListClienti.Clear();
            Clienti.Select();
            return View(Clienti.ListClienti);
        }
        public ActionResult SelectCamere()
        {
            Camere.ListCamere.Clear();
            Camere.Select();
            return View(Camere.ListCamere);
        }
        public ActionResult SelectPrenotazione()
        {
            Prenotazione.ListPrenotazione.Clear();
            Prenotazione.Select();
            return View(Prenotazione.ListPrenotazione);
        }
        public ActionResult SelectServizi()
        {
            Servizi.ListServizi.Clear();
            Servizi.Select();
            return View(Servizi.ListServizi);
        }

        //Edit
        public ActionResult EditCliente( int Id)
        {
            Clienti prod = new Clienti();
            Clienti.Select();
            foreach (Clienti item in Clienti.ListClienti)
            {
                if (item.IdCliente == Id)
                {
                    prod = item;
                    break;
                }
            }
            return View(prod);
        }
        [HttpPost]
        public ActionResult EditCliente(Clienti c, int Id)
        {
            Clienti.Select();
            foreach (Clienti item in Clienti.ListClienti)
            {
                if (item.IdCliente == Id)
                {
                    Clienti.Modifica(c);
                }
            }
            return RedirectToAction("SelectClienti", "Admin");
        }
        public ActionResult EditCamera(int Id)
        {
            Camere prod = new Camere();
            Camere.Select();
            foreach (Camere item in Camere.ListCamere)
            {
                if (item.IdCamera == Id)
                {
                    prod = item;
                    break;
                }
            }
            return View(prod);
        }
        [HttpPost]
        public ActionResult EditCamera(Camere c, int Id)
        {
            Camere.Select();
            foreach (Camere item in Camere.ListCamere)
            {
                if (item.IdCamera == Id)
                {
                    Camere.Modifica(c);
                }
            }
            return RedirectToAction("SelectCamere", "Admin");
        }
        public ActionResult EditPrenotazioni(int Id)
        {
            Prenotazione.ListClienti.Clear();
            Prenotazione.DropdownClienti.Clear();
            Prenotazione.SelectNomeId(Id);
            Prenotazione.DropdownId(Id);
            ViewBag.drop1 = Prenotazione.DropdownClienti;
            Prenotazione prod = new Prenotazione();
            Prenotazione.Select();
            foreach (Prenotazione item in Prenotazione.ListPrenotazione)
            {
                if (item.IdPrenotazione == Id)
                {
                    prod = item;
                    break;
                }
            }
            return View(prod);
        }
        [HttpPost]
        public ActionResult EditPrenotazioni(Prenotazione c, int Id,int Cliente)
        {
            Prenotazione.ListClienti.Clear();
            Prenotazione.DropdownClienti.Clear();
            Prenotazione.SelectNomeId(Id);
            Prenotazione.Dropdown1();
            Prenotazione.Select();
            foreach (Prenotazione item in Prenotazione.ListPrenotazione)
            {
                if (item.IdCamera == Id)
                {
                    Prenotazione.Modifica(c,Cliente);
                }
            }
            return RedirectToAction("SelectPrenotazione", "Admin");
        }

        public ActionResult Editservizio(int Id)
        {
            Servizi.ListPrenotazione.Clear();
            Servizi.DropdownTipoServizio.Clear();
            Servizi.SelectTipoServizio();
            Servizi.Dropdown();
            ViewBag.drop3 = Servizi.DropdownTipoServizio;
            Servizi prod = new Servizi();
            Servizi.Select();
            foreach (Servizi item in Servizi.ListServizi)
            {
                if (item.IdServizio == Id)
                {
                    prod = item;
                    break;
                }
            }
            return View(prod);
        }
        [HttpPost]
        public ActionResult EditServizio(Servizi c, int Id,int TipoServizio)
        {
            Servizi.ListPrenotazione.Clear();
            Servizi.DropdownTipoServizio.Clear();
            Servizi.SelectTipoServizio();
            Servizi.Dropdown();
            ViewBag.drop3 = Servizi.DropdownTipoServizio;
            Servizi.Select();
            foreach (Servizi item in Servizi.ListServizi)
            {
                if (item.IdServizio == Id)
                {
                    Servizi.Modifica(c,TipoServizio);
                }
            }
            return RedirectToAction("SelectCamere", "Admin");
        }

    }
}