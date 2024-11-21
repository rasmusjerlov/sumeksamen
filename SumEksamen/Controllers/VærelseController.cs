using Microsoft.AspNetCore.Mvc;
using SumEksamen.Models;

namespace SumEksamen.Controllers;

public class VærelseController : Controller
{
    private List<Værelse> værelseListe = new List<Værelse>();
    private List<Elev> elevListe = new List<Elev>();
    private VentelisteController _ventelisteController;


    public VærelseController(VentelisteController ventelisteController)
    {
        _ventelisteController = ventelisteController;
    }

    public void ElevlisteFraVenteliste(string aargang)
    {
        elevListe = _ventelisteController.VentelisteTilElevliste(aargang);
    }
    

    public void OpretVærelse(int antalPladser)
    {
        Værelse værelse = new Værelse(antalPladser);
        værelseListe.Add(værelse);
    }

    public void TilføjElev(Værelse værelse, Elev elev)
    {
        if (værelse.HentVærelse().Contains(elev))
        {
            throw new Exception("Elev er allerede tilføjet");
        }
        værelse.AddElev(elev);
    }

    public void FjernElev(Værelse værelse, Elev elev)
    {
        if (!værelse.HentVærelse().Contains(elev))
        {
            throw new Exception("Elev er ikke tilføjet");
        }
        værelse.RemoveElev(elev);
    }
    
    public List<Værelse> HentVærelser()
    {
        return værelseListe;
    }

    
    
    public void FordelEleverPåVærelser()
    {
        var drenge = elevListe.Where(e => e.Køn == Køn.dreng).ToList();
        var piger = elevListe.Where(e => e.Køn == Køn.pige).ToList();

        // Fordel drenge på værelser
        FordelElever(drenge, new Dictionary<int , int>
        {
            { 2, 2 },
            { 3, 0 },
            { 4, 17 },
            { 5, 1 }
        });

        // Fordel piger på værelser
        FordelElever(piger, new Dictionary<int, int>
        {
            { 2, 0 },
            { 3, 0 },
            { 4, 15 }
        });
    }

    private void FordelElever(List<Elev> elever, Dictionary<int, int> værelseTyper)
    {
        foreach (var værelseType in værelseTyper)
        {
            int antalPladser = værelseType.Key;
            int antalVærelser = værelseType.Value;

            for (int i = 0; i < antalVærelser; i++)
            {
                if (elever.Count == 0) return;

                Værelse værelse = new Værelse(antalPladser);
                værelseListe.Add(værelse);
                

                for (int j = 0; j < antalPladser && elever.Count > 0; j++)
                {
                    værelse.AddElev(elever[0]);
                    elever.RemoveAt(0);
                }
            }
        }
    }
    
    [HttpGet]
    public IActionResult FordelEleverDropDown()
    {
        var aargangList = _ventelisteController.HentVentelister()
            .Select(v => v.Aargang)
            .ToList();

        ViewBag.AargangList = aargangList;
        return View();
    }
    
    [HttpPost]
    public IActionResult FordelEleverFraVenteliste(string valgtAargang)
    {
        if (string.IsNullOrEmpty(valgtAargang))
        {
            ViewBag.Error = "Du skal vælge en årgang.";
            return RedirectToAction("FordelEleverDropdown");
        }

        // Hent elevlisten for den valgte årgang
        ElevlisteFraVenteliste(valgtAargang);
        // Fordel eleverne på værelser
        FordelEleverPåVærelser();

        // Vis værelserne
        var værelser = HentVærelser();
        return View("FordelElever", værelser);
    }
    
    
    
}