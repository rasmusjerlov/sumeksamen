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
        
        vlController = new VentelisteController();
        vlController.CreateVenteliste("2025/2026");
        vlController.TilfoejElev("2025/2026", "mikkel", "dreng");
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
    
    
    
}