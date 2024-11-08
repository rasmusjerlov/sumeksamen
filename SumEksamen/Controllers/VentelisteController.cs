using Microsoft.AspNetCore.Mvc;
using SumEksamen.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SumEksamen.Controllers
{
    public class VentelisteController : Controller
    {
        // Fælles liste til at gemme ventelister
        private static List<Venteliste> ventelister = new List<Venteliste>();

        // GET: VentelisteController/Opretventeliste
        [HttpGet]
        public IActionResult Opretventeliste()
        {
            return View();
        }

        // POST: VentelisteController/Opretventeliste
        [HttpPost]
        public IActionResult Opretventeliste(string aargang)
        {
            if (string.IsNullOrWhiteSpace(aargang))
            {
                ModelState.AddModelError("Aargang", "Årgang er påkrævet.");
                return View();
            }

            try
            {
                // Kald til metoden, der tilføjer en ny venteliste
                CreateVenteliste(aargang);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("Aargang", ex.Message);
                return View();
            }

            return RedirectToAction("Index");
        }

        // Opretter en ny venteliste
        public void CreateVenteliste(string aargang)
        {
            // Tjek for dubletter
            if (ventelister.Any(v => v.Aargang == aargang))
            {
                throw new ArgumentException("En venteliste med denne årgang eksisterer allerede.");
            }

            // Tilføj nyt ventelisteelement
            ventelister.Add(new Venteliste(aargang, DateTime.Now));
            
        }

        // GET: VentelisteController/Index
        public IActionResult Index()
        {
            return View(ventelister);
        }
        // Henter alle ventelister
        public List<Venteliste> HentVentelister()
        {
            return ventelister;
        }
    }
}