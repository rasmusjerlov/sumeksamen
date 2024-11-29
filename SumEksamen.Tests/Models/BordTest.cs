using System.Collections.Generic;
using JetBrains.Annotations;
using SumEksamen.Controllers;
using SumEksamen.Models;
using SumEksamen.Services;
using Xunit;
using System.Linq;
using System;

namespace SumEksamen.Models;

[TestSubject(typeof(Bord))]
public class BordTest
{
    private Bord b1;
    private Bord b2;
    private Bord b3;
    private Bord b4;

    public BordTest()
    {
        b1 = new Bord(12);
        b2 = new Bord(12);
        b3 = new Bord(8);
        b4 = new Bord(8);
    }

    [Fact]
    public void TC1_OpretBord_withSpecificPladser_shouldInitializeBordCorrectly()
    {
        //Arrange & Act
        Bord bord = new Bord(12);

        //Assert
        Assert.Equal(12, bord.antalPladser);
        Assert.Equal(5, bord.bordNr);
    }

    [Fact]
    public void TC2_AendreAntalPladser_onExistingBord_shouldUpdatePladserCorrectly()
    {
        //Arrange & Act
        b1.antalPladser = 10;

        //Assert
        Assert.Equal(10, b1.antalPladser);
    }

    [Fact]
    public void TC3_TilfojElevTilBord_withElev_shouldAddElevToBord()
    {
        // Arrange
        var bord1 = new Bord(4);
        var bord2 = new Bord(4);
        var borde = new List<Bord> { bord1, bord2 };

        var venteliste = new List<Elev>
        {
            new Elev("Rasmus", Køn.dreng),
            new Elev("Mikkel", Køn.dreng),
            new Elev("Hanne", Køn.pige),
            new Elev("Martina", Køn.pige),
            new Elev("Abu", Køn.dreng),
            new Elev("Jens", Køn.dreng),
            new Elev("Ludvig", Køn.pige),
            new Elev("Morten", Køn.dreng)
        };

        var aargang = "2023/2024";

        // Add the Venteliste for the specified year
        var ventelisteController = new VentelisteController();
        var ventelisteForAargang = new Venteliste(aargang);
        foreach (var elev in venteliste)
        {
            ventelisteForAargang.tilfojElev(elev);
        }
        Storage.TilføjVenteliste(ventelisteForAargang);

        // Act
        Storage.VentelisteTilElevliste(aargang);
        var controller = new BordController(ventelisteController);
        controller.TilfojElevTilBordFraVenteliste(aargang);

        // Assert
        Assert.Equal(2, bord1.elever.Count(e => e.Køn == Køn.pige));
        Assert.Contains(bord1.elever, e => e.Navn == "Ludvig");
    }

    [Fact]
    public void TC4_TilfojElevTilBordPladsMindst2piger()
    {
        // Arrange
        var bord1 = new Bord(4);
        var bord2 = new Bord(4);
        var borde = new List<Bord> { bord1, bord2 };

        var venteliste = new List<Elev>
        {
            new Elev("Hanne", Køn.pige),
            new Elev("Martina", Køn.pige),
            new Elev("Ludvig", Køn.pige),
            new Elev("Rasmus", Køn.dreng),
            new Elev("Mikkel", Køn.dreng),
            new Elev("Abu", Køn.dreng)
        };

        var aargang = "2023/2024";

        // Add the Venteliste for the specified year
        var ventelisteController = new VentelisteController();
        var ventelisteForAargang = new Venteliste(aargang);
        foreach (var elev in venteliste)
        {
            ventelisteForAargang.tilfojElev(elev);
        }
        Storage.TilføjVenteliste(ventelisteForAargang);

        // Act
        Storage.VentelisteTilElevliste(aargang);
        var controller = new BordController(ventelisteController);
        controller.TilfojElevTilBordFraVenteliste(aargang);

        // Assert
        Assert.Equal(2, bord1.elever.Count(e => e.Køn == Køn.pige));
        Assert.Equal(2, bord2.elever.Count(e => e.Køn == Køn.pige));
    }

    [Fact]
    public void TC5_TilfojElevTilBordPladsException()
    {
        // Arrange
        var bord1 = new Bord(4);
        var bord2 = new Bord(4);
        var borde = new List<Bord> { bord1, bord2 };

        var venteliste = new List<Elev>
        {
            new Elev("Hanne", Køn.pige)
        };

        var aargang = "2023/2024";

        // Add the Venteliste for the specified year
        var ventelisteController = new VentelisteController();
        var ventelisteForAargang = new Venteliste(aargang);
        foreach (var elev in venteliste)
        {
            ventelisteForAargang.tilfojElev(elev);
        }
        Storage.TilføjVenteliste(ventelisteForAargang);

        // Act & Assert
        Storage.VentelisteTilElevliste(aargang);
        var controller = new BordController(ventelisteController);

        Assert.Throws<InvalidOperationException>(() => controller.TilfojElevTilBordFraVenteliste(aargang));
    }
}