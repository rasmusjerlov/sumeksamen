using Microsoft.AspNetCore.Mvc;
using SumEksamen.Models;
using SumEksamen.Services;

namespace SumEksamen.Controllers;

public class KlasseholdController : Controller
{
    public IActionResult Klassehold()
    {
        return View(Storage.HentKlassehold());
    }
    
    [HttpGet]
    [Route("klassehold/opret")]
    public IActionResult OpretKlassehold()
    {
        return View();
    }

    [HttpPost]
    [Route("klassehold/opret")]
    public IActionResult OpretKlasse(string fag, string lokale)
    {
        if (string.IsNullOrWhiteSpace(fag))
        {
            ModelState.AddModelError("Fag", "Angivelse af fag er påkrævet.");
            return View();
        }

        var klassehold = new Klassehold(fag, lokale);
        
        Storage.TilføjKlassehold(klassehold);

        return RedirectToAction("KlasseholdDetaljer", new { fag = fag });
    }
}