using SumEksamen.Models;
using Xunit;

namespace SumEksamen.Tests;

public class VentelisteTest
{
    private Elev e1;
    private Elev e2;
    private Elev e3;
    private Venteliste vl;

    public VentelisteTest()
    {
        e1 = new Elev("Rasmus", 14);
        e2 = new Elev("Abukar", 12);
        e3 = new Elev("Mikkel", 18);
        vl = new Venteliste("24/25", DateTime.Now);
    }
    
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
        //Act
        vl.tilfojElev(e1);
        
        //Assert
        Assert.True(vl.hentElever().Contains(e1));
        Assert.Throws<ArgumentException>(() => vl.tilfojElev(e3));
    }
}