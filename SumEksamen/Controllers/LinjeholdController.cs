using Microsoft.AspNetCore.Mvc;
using SumEksamen.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;

namespace SumEksamen.Controllers
{
    public class LinjeholdController : Controller
    {
        private static List<Elev> elevListe = new List<Elev>();
        private static List<Linjehold> linjeholdListe = new List<Linjehold>();
        private readonly VentelisteController _ventelisteController;
        
        public LinjeholdController(VentelisteController ventelisteController)
        {
            _ventelisteController = ventelisteController;
        }
        
        // GET: LinjeholdController
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("linjehold")]
        public IActionResult Opretlinjehold()
        {
            return View();
        }
        
        [HttpGet]
        [Route("linjehold/opret")]
        public IActionResult OpretLinjehold(string type, int kapacitet, string køn)
        {

            if (string.IsNullOrWhiteSpace(type) || kapacitet == 0)
            {
                return BadRequest("Udfyld venligst alle felter.");
            }
            
            var kønEnum = (Køn)Enum.Parse(typeof(Køn), køn, true);
            var linjehold = new Linjehold(type, kapacitet, kønEnum);
            

            
            linjeholdListe.Add(linjehold);
            
            return RedirectToAction("LinjeholdOversigt");
        }
        
        //linjehold oversigt
        
        [HttpGet]
        [Route("linjehold/oversigt")]
        public IActionResult LinjeholdOversigt()
        {
            var aargangList = _ventelisteController.HentVentelister().Select(v => v.Aargang).ToList();
            ViewBag.AargangList = aargangList;
            ViewData["Elever"] = elevListe;
            return View(linjeholdListe);
        }

        [HttpGet]
        [Route("linjehold/tilføj")]
        public IActionResult FordelElevPaaLinjehold(string elevNavn, string linjeholdType)
        {
            
            var elev = elevListe.FirstOrDefault(e => e.Navn.Equals(elevNavn, StringComparison.OrdinalIgnoreCase));
            if (elev == null)
                return NotFound("Elev ikke fundet.");

            
            var linjehold = linjeholdListe.FirstOrDefault(l => l.type == linjeholdType);
            if (linjehold == null)
                return NotFound("Linjehold ikke fundet.");

            
            if (linjehold.hentElever().Count >= linjehold.kapacitet)
                return BadRequest("Linjeholdets kapacitet er nået.");

            
            if (linjehold.køn != Køn.blandet && linjehold.køn != elev.Køn)
                return BadRequest("Elevens køn passer ikke til linjeholdets køn.");
            
            try
            {
                linjehold.tilfojElev(elev);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            
            return RedirectToAction("LinjeholdOversigt");
        }
        
        public void ElevlisteFraVenteliste(string aargang)
        {
            
            elevListe = _ventelisteController.VentelisteTilElevliste(aargang);
        }
        
        [HttpGet]
        [Route("linjehold/elevliste")]
        public IActionResult OpretElevlisteFraVenteliste(string aargang)
        {
           
            ElevlisteFraVenteliste(aargang);

            
            var aargangList = _ventelisteController.HentVentelister().Select(v => v.Aargang).ToList();
            ViewBag.AargangList = aargangList;

            //return View("OpretElevlisteFraVenteliste");
            return RedirectToAction("LinjeholdOversigt");
        }

        
       
    }
}