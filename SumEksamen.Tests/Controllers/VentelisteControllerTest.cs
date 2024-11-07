using System;
using Microsoft.AspNetCore.Http.HttpResults;
using SumEksamen.Controllers;
using Xunit;

namespace SumEksamen.Tests;

public class VentelisteControllerTest
{

    [Fact]
    public void TC1_opretVenteliste()
    {
        VentelisteController vc = new VentelisteController();
        vc.Create("24/25", new DateTime(2024, 10, 01));
        
        Assert.Contains(vc.hentVenteLister(), v => v.Aargang == "24/25");
    }

    [Fact]
    public void TC2_opretVentelisteFejl()
    {
        VentelisteController vc = new VentelisteController();
        vc.Create("24/25", new DateTime(2024, 10, 01));
        
        Assert.Throws<ArgumentException>(() => vc.Create("24/25", new DateTime(2024, 10, 01)));
    }
}