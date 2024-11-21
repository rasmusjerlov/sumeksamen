using Microsoft.AspNetCore.Mvc;
using SumEksamen.Models;
using SumEksamen.Services;

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
    
    public void TilføjVærelse(Værelse værelse)
    {
        Storage.TilføjVærelse(værelse);
    }
    


    public void OpretVærelse(int antalPladser)
    {
        Storage.TilføjVærelse(new Værelse(antalPladser));
    }

    public void TilføjElev(Værelse værelse, Elev elev)
    {
        if (Storage.FindVærelse(værelse.VærelelsesId).ElevListe.Contains(elev))
        {
            throw new Exception("Elev er allerede tilføjet");
        }
        værelse.AddElev(elev);
    }

    public void FjernElev(Værelse værelse, Elev elev)
    {
        if (!Storage.FindVærelse(værelse.VærelelsesId).HentVærelse().Contains(elev))
        {
            throw new Exception("Elev er ikke på værelset");
        }
        værelse.RemoveElev(elev);
    }
    
    public List<Værelse> HentVærelser()
    {
        return Storage.HentVærelser();
    }

    public void FordelEleverPåVærelser()
    {
        var drenge = elevListe.Where(e => e.Køn == Køn.dreng).ToList();
        var piger = elevListe.Where(e => e.Køn == Køn.pige).ToList();
        
       
    }
    
    
    
    
    
}