using SumEksamen.Models;
using Xunit;

namespace SumEksamen.Tests;

public class VentelisteTest
{
    [Fact]
    public void TC1_opretVenteliste()
    {
        //Arrange
        Venteliste vl = new Venteliste("24/25", new DateTime(2024, 10, 01));

        //Act
        string aargang = vl.Aargang;
        DateTime oprettelsesDato = vl.OprettelsesDato;
        DateTime expectedDatetime = new DateTime(2024, 10, 01);

        //Assert
        Assert.Equal("24/25", aargang);
        Assert.Equal(expectedDatetime, oprettelsesDato);
    }

    [Fact]
    public void TC2_opretVentelisteFail()
    {
        //Arrange
        List<Venteliste> ventelister = new List<Venteliste>();
        Venteliste vl = new Venteliste("24/25", new DateTime(2024, 10, 01));
        ventelister.Add(vl);

        //Act & Assert
        Assert.Throws<ArgumentException>(() => new Venteliste("24/25", new DateTime(2024, 10, 01)));
        DateTime expectedDatetime = new DateTime(2024, 10, 01);
        Assert.Equal(expectedDatetime, vl.OprettelsesDato);
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

        //Assert
        Assert.True(vl.hentElever().Contains(e1));
        Assert.Throws<ArgumentException>(() => vl.tilfojElev(e3));
    }
}