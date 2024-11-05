using SumEksamen.Models;
using Xunit;

namespace SumEksamen.Tests;

public class VentelisteTest
{

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
}