using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using SumEksamen.Controllers;
using SumEksamen.Models;
using Xunit;

namespace SumEksamen.Tests.Controllers;

[TestSubject(typeof(VentelisteController))]
public class VentelisteControllerTest
{
    
    private VentelisteController vlController;
    
    public VentelisteControllerTest()
    {
        VentelisteController.ResetVenteliste();
        vlController = new VentelisteController();
        vlController.CreateVenteliste("2025/2026");
        vlController.TilfoejElev("2025/2026", "mikkel", "dreng");
    }
    
    [Fact]
    public void TC1_opretVenteliste()
    {
        vlController.CreateVenteliste("24/25");
        Assert.Contains(vlController.HentVentelister(), v => v.Aargang == "24/25");
    }

    [Fact]
    public void TC2_opretVentelisteFejl()
    {
        VentelisteController vc = new VentelisteController();
        vc.CreateVenteliste("25/26");
        
        Assert.Throws<ArgumentException>(() => vc.CreateVenteliste("25/26"));
    }

    
    [Fact]
    public void TC1_HentVenteliste()
    {
        //act
        var result = vlController.HentVenteliste("2025/2026");
        
        //Assert
        Assert.Equal("2025/2026", result.Aargang);
        Assert.Equal(1, result.hentElever().Count);
        Assert.Equal("mikkel", result.hentElever()[0].Navn);
            
    }

    [Fact]
    public void TC2_HentVentelisteKasterFejl()
    {
        Assert.Throws<ArgumentException>(() => vlController.HentVenteliste("2027/2028"));
    }
    
}