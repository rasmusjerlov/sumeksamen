using SumEksamen.Models;
using Xunit;

namespace SumEksamen.Tests;

public class VentelisteTest
{
    
    [Fact]
    public void opretVenteliste()
    {
        //Arrange
        Venteliste vl = new Venteliste("24/25", DateTime.Now);
        
        //Act
        string aargang = vl.opretVenteliste();
        
        //Assert
        Assert.Equal("24/25", aargang);
    }

    [Fact]
    public void opretVentelisteFail()
    {
        //Arrange
        List<Venteliste> ventelister = new List<Venteliste>();
        Venteliste vl = new Venteliste("24/25", DateTime.Now);
        ventelister.Add(vl);
        
        //Act
        Venteliste vl2 = new Venteliste("24/25", DateTime.Now);
        ventelister.Add(vl2);
        
        //Assert
        Assert.True(ventelister.Count == 1);
        
    }
    

    [Fact]
    public void tilfojElever()
    {
        //Arrange
        Venteliste vl = new Venteliste("24/25", DateTime.Now);
        Elev e1 = new Elev("Rasmus", 24);
        Elev e2 = new Elev("Abu", 18);
        
        //Act
        vl.tilfojElev(e1);
        vl.tilfojElev(e2);
        
        //Assert
        Assert.True(vl.hentElever().Contains(e1));
        Assert.True(vl.hentElever().Contains(e2));
        
    }

    [Fact]
    public void tilfojEleverFejl()
    {
        //Arrange
        Venteliste vl = new Venteliste("24/25", DateTime.Now);
        Elev e1 = new Elev("Rasmus", 24);
        Elev e2 = new Elev("Abukar", 18);
        
        //Act
        vl.tilfojElev(e1);
        
        //Assert
        Assert.False(vl.hentElever().Contains(e2));
    }
}