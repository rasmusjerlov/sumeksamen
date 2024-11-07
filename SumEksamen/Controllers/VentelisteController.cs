using Microsoft.AspNetCore.Mvc;
using SumEksamen.Models;

namespace SumEksamen.Controllers;

public class VentelisteController : Controller
{
    private static List<Venteliste> ventelister = new List<Venteliste>();
    // GET
    public IActionResult Index()
    {
        return View(ventelister);
    }
    [HttpPost]
    public IActionResult Create(string aargang, DateTime oprettelsesDato)
    {
        foreach (var vl in ventelister)
        {
            if (vl.Aargang.Equals(aargang))
            {
                throw new ArgumentException("Ventelisten for denne aargang findes allerede");
            }
        }
        var nyVenteListe = new Venteliste(aargang, oprettelsesDato);
        ventelister.Add(nyVenteListe);
        
        return RedirectToAction("Index");
    }

    public List<Venteliste> hentVenteLister()
    {
        return ventelister;
    }
}