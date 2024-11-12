namespace SumEksamen.Controllers;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SumEksamen.Models;

public class BordController : Controller
{
    public static List<Bord> borde = new List<Bord>();
    
    
    //henter bordene
    public List<Bord> HentBorde()
    {
        return borde;
    }
    
}