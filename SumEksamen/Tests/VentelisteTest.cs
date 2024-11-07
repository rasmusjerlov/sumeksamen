using SumEksamen.Models;
using Xunit;

namespace SumEksamen.Tests;

public class VentelisteTest
{
    
    [Fact]
    public void opretVenteliste()
    {
        //Arrange
        Venteliste vl = new Venteliste("24/25", new DateTime(2024,10,01));
        
        //Act
        string aargang = vl.Aargang;
        DateTime oprettelsesDato = vl.OprettelsesDato;
        DateTime expectedDatetime = new DateTime(2024,10,01);
        
        //Assert
        Assert.Equal("24/25", aargang);
        Assert.Equal(expectedDatetime, oprettelsesDato);
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
    public void TC1_tilfojElev()
    {
        //Arrange
        Venteliste vl = new Venteliste("24/25", DateTime.Now);
        Elev e1 = new Elev("Rasmus", 14);
        Elev e2 = new Elev("Abu", 12);
        
        //Act
        vl.tilfojElev(e1);
        vl.tilfojElev(e2);
        
        //Assert
        Assert.True(vl.hentElever().Contains(e1));
        Assert.True(vl.hentElever().Contains(e2));
        
    }

    [Fact]
    public void TC2_tilfojElevFejl()
    {
        //Arrange
        Venteliste vl = new Venteliste("24/25", DateTime.Now);
        Elev e1 = new Elev("Rasmus", 14);
        Elev e2 = new Elev("Abukar", 12);
        Elev e3 = new Elev("Mikkel", 18);
        
        //Act
        vl.tilfojElev(e1);
        vl.tilfojElev(e3);
        
        //Assert
        Assert.True(vl.hentElever().Contains(e1));
        Assert.False(vl.hentElever().Contains(e3));
    }
}