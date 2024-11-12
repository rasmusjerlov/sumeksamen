namespace SumEksamen.Controllers;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SumEksamen.Models;

public class BordController : Controller
{
    private static List<Bord> borde = new List<Bord>();

    public IActionResult Bordopsætning()
    {
        // Check if the list of tables is already populated
        if (borde.Count == 0)
        {
            for (int i = 0; i <= 5; i++)
            {
                borde.Add(new Bord(12));
            }
            for (int i = 0; i <= 7; i++)
            {
                borde.Add(new Bord(8));
            }
        }

        // Pass the list of tables to the view
        return View("/Views/Spisesal/Bordopsætning.cshtml", borde);
    }

    [HttpPost]
    public IActionResult UpdateBord(int bordNr, int antalPladser)
    {
        var bord = borde.FirstOrDefault(b => b.bordNr == bordNr);
        if (bord != null)
        {
            bord.antalPladser = antalPladser;
        }
        return RedirectToAction("Bordopsætning");
    }
}