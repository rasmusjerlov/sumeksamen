using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using SumEksamen.Controllers;
using SumEksamen.Models;
using SumEksamen.Services;
using Xunit;

namespace SumEksamen.Tests.Controllers;

[TestSubject(typeof(VentelisteController))]
public class VentelisteControllerTest
{
    
    private VentelisteController vlController;
    
    public VentelisteControllerTest() //Fungerer som en setup metode
    {
        vlController = new VentelisteController();
        
    }
    
    [Fact]
    public void TC1_opretVenteliste()
    {
        //Act
        vlController.Opretventeliste("2025/2026");
        //vlController.TilfoejElev("2025/2026", "mikkel", "dreng");
        
        //Assert
        Assert.Contains(Storage.HentVentelister(), v => v.Aargang == "2025/2026");
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
        
        //Arrange
        VentelisteController controller = new VentelisteController();
        var venteliste = controller.Opretventeliste("2025/2026");
        //act
        var result = Storage.FindVenteliste("2025/2026");
        
        //Assert
        Assert.Equal("2025/2026", result.Aargang);
        Assert.Equal(0, result.hentElever().Count);
            
    }

    [Fact]
    public void TC2_HentVentelisteSomIkkeEksisterer()
    {
        //Tjekker om der kastes en ArgumentException, hvis der forsøges at hente en venteliste, som ikke eksisterer
        Assert.Throws<ArgumentException>(() => Storage.FindVenteliste("2027/2028"));
    }
    
}