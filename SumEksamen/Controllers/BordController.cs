using SumEksamen.Services;

namespace SumEksamen.Controllers;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SumEksamen.Models;

public class BordController : Controller
{

    public IActionResult Bordopsætning()
    {
        // Check if the list of tables is already populated
        if (Storage.HentBorde().Count == 0)
        {
            for (int i = 0; i <= 5; i++)
            {
                Storage.TilføjBord(new Bord(12));
            }

            for (int i = 0; i <= 7; i++)
            {
                Storage.TilføjBord(new Bord(8));
            }
        }

        // Pass the list of tables to the view
        return View("/Views/Spisesal/Bordopsætning.cshtml", Storage.HentBorde());
    }

    [HttpPost]
    public IActionResult UpdateBord(int bordNr, int antalPladser)
    {
        var bord = Storage.HentBorde().FirstOrDefault(b => b.bordNr == bordNr);
        if (bord != null)
        {
            bord.antalPladser = antalPladser;
        }

        return RedirectToAction("Bordopsætning");
    }

    [HttpPost]
    public IActionResult OpretBord(int bordNr, int antalPladser)
    {
        try
        {
            foreach (var bord in Storage.HentBorde())
            {
                if (bord.bordNr == bordNr)
                {
                    throw new ArgumentException("Et bord med dette ID eksisterer allerede.");
                }
            }

            // Find the smallest available bordNr if the provided one already exists
            int newBordNr = bordNr;
            while (Storage.HentBorde().Any(b => b.bordNr == newBordNr))
            {
                newBordNr++;
            }

            Storage.TilføjBord(new Bord { bordNr = newBordNr, antalPladser = antalPladser });

            return Json(new { success = true });
        }
        catch (ArgumentException ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }
    
    [HttpPost]
    public IActionResult SletBord(int bordNr)
    {
        var bord = Storage.HentBorde().FirstOrDefault(b => b.bordNr == bordNr);
        if (bord != null)
        {
            Storage.SletBord(bord);
        }
        else
        {
            throw new ArgumentException("Et bord med dette ID findes ikke.");
        }

        return RedirectToAction("Bordopsætning");
    }
}