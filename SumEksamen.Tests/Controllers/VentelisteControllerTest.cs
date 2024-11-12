using System;
using Microsoft.AspNetCore.Http.HttpResults;
using SumEksamen.Controllers;
using Xunit;

namespace SumEksamen.Tests;

public class OpretVentelisteControllerTest
{

    [Fact]
    public void TC1_opretVenteliste()
    {
        VentelisteController vc = new VentelisteController();
        vc.CreateVenteliste("24/25");
        
        Assert.Contains(vc.HentVentelister(), v => v.Aargang == "24/25");
    }

    [Fact]
    public void TC2_opretVentelisteFejl()
    {
        VentelisteController vc = new VentelisteController();
        vc.CreateVenteliste("25/26");
        
        Assert.Throws<ArgumentException>(() => vc.CreateVenteliste("25/26"));
    }
}