using Microsoft.AspNetCore.Mvc;
using SumEksamen.Models;

namespace SumEksamen.Controllers;

public class OpretVentelisteController
{
    private List<Venteliste> ventelister = new List<Venteliste>();

    public void Create(string aargang, DateTime dato)
    {
        if (ventelister.Any(v => v.Aargang == aargang))
        {
            throw new ArgumentException("Denne venteliste med" + aargang + "eksisterer allerede");
        }

        ventelister.Add(new Venteliste(aargang, dato));
    }

    public List<Venteliste> hentVenteLister()
    {
        return ventelister;
    }
}

