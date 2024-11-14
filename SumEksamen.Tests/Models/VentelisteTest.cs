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
    private Venteliste vl;
    private Venteliste el;

    public VentelisteTest()
    {
        e1 = new Elev("Rasmus", 14, Køn.dreng);
        e2 = new Elev("Abukar", 12, Køn.dreng);
        e3 = new Elev("Mikkel", 18, Køn.dreng);
        
        e4 = new Elev(06, "Rasmus", Køn.dreng, Status.Aktiv);
        e5 = new Elev(12, "Abukar", Køn.dreng, Status.Aktiv);
        e6 = new Elev(27, "Mikkel", Køn.dreng, Status.Inaktiv);
        
        vl = new Venteliste("24/25");
        el = new Venteliste("25/26");
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
     
    [Fact]
    public void TC1_sletElev()
    {
        //Act
        el.tilfojElev(e4);
        el.tilfojElev(e5);
        el.tilfojElev(e6);

        el.sletElev(e5.ElevNr);
        
        //Assert
        Assert.True(el.hentElever().Contains(e4));
        Assert.False(el.hentElever().Contains(e5));
        
    }
    
    [Fact]
    public void TC1_sletElevFejl()
    {
        //Act
        el.tilfojElev(e4);
        el.tilfojElev(e5);
        el.tilfojElev(e6);

        el.sletElev(e6.ElevNr);
        
        //Assert
        Assert.Throws<ArgumentException>(() => el.sletElev(e6.ElevNr));

    }
}