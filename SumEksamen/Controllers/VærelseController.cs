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
        
       
    }
    
    
    
    
    
}