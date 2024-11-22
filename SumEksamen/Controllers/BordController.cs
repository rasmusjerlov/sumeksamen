namespace SumEksamen.Controllers;

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SumEksamen.Models;
using System;
using System.Linq;
using System.Collections.Generic;

public class BordController : Controller
{
    private static List<Bord> borde = new List<Bord>();
    private readonly VentelisteController _ventelisteController;
    private static List<Elev> elevListe = new List<Elev>();

    public BordController(VentelisteController ventelisteController)
    {
        _ventelisteController = ventelisteController;
    }

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

        // Fetch the list of years from VentelisteController
        var aargangList = _ventelisteController.HentVentelister().Select(v => v.Aargang).ToList();
        ViewBag.AargangList = aargangList;

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

    [HttpPost]
    public IActionResult OpretBord(int bordNr, int antalPladser)
    {
        try
        {
            if (antalPladser > 12)
            {
                throw new ArgumentException("Et bord kan maksimalt have 12 pladser.");
            }

            foreach (var bord in borde)
            {
                if (bord.bordNr == bordNr)
                {
                    throw new ArgumentException("Et bord med dette ID eksisterer allerede.");
                }
            }

            // Find the smallest available bordNr if the provided one already exists
            int newBordNr = bordNr;
            while (borde.Any(b => b.bordNr == newBordNr))
            {
                newBordNr++;
            }

            borde.Add(new Bord { bordNr = newBordNr, antalPladser = antalPladser });

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
        var bord = borde.FirstOrDefault(b => b.bordNr == bordNr);
        if (bord != null)
        {
            borde.Remove(bord);
        }
        else
        {
            throw new ArgumentException("Et bord med dette ID findes ikke.");
        }

        return RedirectToAction("Bordopsætning");
    }

    public void ElevListeFraVenteliste(string aargang)
    {
        elevListe = _ventelisteController.VentelisteTilElevliste(aargang);
    }

    [HttpPost]
    public IActionResult TilfojElevTilBordFraVenteliste(string aargang)
    {
        // Retrieve students from the waiting list for the specified year
        ElevListeFraVenteliste(aargang);

        var piger = elevListe.Where(e => e.Køn == Køn.pige).OrderBy(e => Guid.NewGuid()).ToList();
        var drenge = elevListe.Where(e => e.Køn == Køn.dreng).OrderBy(e => Guid.NewGuid()).ToList();

        foreach (var bord in borde)
        {
            if (bord.elever == null)
            {
                bord.elever = new List<Elev>();
            }

            // Ensure at least 2 girls at each table
            for (int i = 0; i < 2 && piger.Count > 0; i++)
            {
                bord.elever.Add(piger[0]);
                piger.RemoveAt(0);
            }

            // Distribute remaining seats randomly between boys and girls
            var remainingStudents = piger.Concat(drenge).OrderBy(e => Guid.NewGuid()).ToList();
            while (bord.elever.Count < bord.antalPladser && remainingStudents.Count > 0)
            {
                bord.elever.Add(remainingStudents[0]);
                remainingStudents.RemoveAt(0);
            }
        }

        return RedirectToAction("Bordopsætning");
    }
}