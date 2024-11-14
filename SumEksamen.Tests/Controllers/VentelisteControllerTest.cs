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
        vlController.Opretventeliste("2025/2026");
        vlController.TilfoejElev("2025/2026", "mikkel", "dreng");
    }
    
    [Fact]
    public void TC1_opretVenteliste()
    {
        vlController.Opretventeliste("24/25");
        Assert.Contains(vlController.HentVentelister(), v => v.Aargang == "24/25");
    }

    [Fact]
    public void TC2_opretVentelisteFejl()
    {
        // Arrange
        var controller = new VentelisteController();
        controller.Opretventeliste("25/26");

        // Act
        controller.Opretventeliste("25/26");

        // Assert
        Assert.True(controller.ModelState.ContainsKey("Aargang"));
        Assert.Equal("En venteliste med denne årgang eksisterer allerede.", controller.ModelState["Aargang"].Errors[0].ErrorMessage);
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