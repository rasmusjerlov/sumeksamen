using System;
using SumEksamen.Models;
using Xunit;

namespace SumEksamen.Tests;

public class VentelisteTest
{
    private Elev e1;
    private Elev e2;
    private Elev e3;
    private Elev e4;
    private Elev e5;
    private Elev e6;
    private Venteliste el1;
    private Venteliste el2;
    private Venteliste el3;
    private Venteliste el4;
    private Venteliste el5;

    public VentelisteTest()
    {
        e1 = new Elev("Rasmus", 14, Køn.dreng);
        e2 = new Elev("Abukar", 12, Køn.dreng);
        e3 = new Elev("Mikkel", 18, Køn.dreng);
        
        e4 = new Elev(06, "Rasmus", Køn.dreng, Status.Aktiv);
        e5 = new Elev(12, "Abukar", Køn.dreng, Status.Aktiv);
        e6 = new Elev(27, "Mikkel", Køn.dreng, Status.Inaktiv);
        
        el1 = new Venteliste("24/25", DateTime.Now);
        el2 = new Venteliste("25/26", DateTime.Now);
        el3 = new Venteliste("26/27", DateTime.Now);
        el4 = new Venteliste("26/27", DateTime.Now);
        el5 = new Venteliste("27/28", DateTime.Now);
    }

    [Fact]
    public void TC1_tilfojElev()
    {
        //Act
        el1.tilfojElev(e1);
        el1.tilfojElev(e2);
        
        //Assert
        Assert.True(el1.hentElever().Contains(e1));
        Assert.True(el1.hentElever().Contains(e2));
        
    }

    [Fact]
    public void TC2_tilfojElevFejl()
    {
        //Act
        el1.tilfojElev(e1);
        
        //Assert
        Assert.True(el1.hentElever().Contains(e1));
        Assert.Throws<ArgumentException>(() => el1.tilfojElev(e3));
    }
     
    [Fact]
    public void TC1_sletElev()
    {
        
        //Act
        el2.tilfojElev(e4);
        el2.tilfojElev(e5);
        el2.tilfojElev(e6);

        el2.sletElev(e5.ElevNr);
        
        //Assert
        Assert.True(el2.hentElever().Contains(e4));
        Assert.False(el2.hentElever().Contains(e5));
        Assert.True(el2.hentElever().Contains(e6));
        
    }
    
    [Fact]
    public void TC2_sletElevFejl()
    {
        //Act
        el2.tilfojElev(e4);
        el2.tilfojElev(e5);
        el2.tilfojElev(e6);

        el2.sletElev(e6.ElevNr);
        
        //Assert
        Assert.Throws<ArgumentException>(() => el2.sletElev(e6.ElevNr));
        Assert.True(el2.hentElever().Contains(e4));
        Assert.True(el2.hentElever().Contains(e5));
        Assert.False(el2.hentElever().Contains(e6));
        

    }

    [Fact]
    public void TC1_findElev()
    {
        //Act
        el3.tilfojElev(e4);
        el3.tilfojElev(e5);
        el3.tilfojElev(e6);

        //Assert
        Assert.Equal(e4, el3.findElev(e4.ElevNr));
        Assert.Equal(e5, el3.findElev(e5.ElevNr));
        Assert.Equal(e6, el3.findElev(e6.ElevNr));
    }
    
    [Fact]
    public void TC2_findElevFejl()
    {
        //Act
        el3.tilfojElev(e4);
        el3.tilfojElev(e5);
        el3.tilfojElev(e6);

        //Assert
        Assert.Equal(e4, el3.findElev(e4.ElevNr));
        Assert.Equal(e5, el3.findElev(e5.ElevNr));
        Assert.Equal(e6, el3.findElev(e6.ElevNr));
        Assert.Throws<ArgumentException>(() => el3.findElev(10));
        
    }

    [Fact]
    public void TC1_updateElev()
    {
        //Act
        el4.tilfojElev(e4);
        el4.tilfojElev(e5);
        el4.tilfojElev(e6);
        
        el4.updateElev(e4.ElevNr, "Rasmus er en god elev");
        el4.updateElev(e4.ElevNr, "Rasmus er en dårlig elev");
        el4.updateElev(e5.ElevNr, "Abukar er en god elev");

        //Assert
        Assert.Equal("Rasmus er en god elev", e4.Bemærkninger[0].Tekst);
        Assert.Equal("Rasmus er en dårlig elev", e4.Bemærkninger[1].Tekst);
    }
    
}