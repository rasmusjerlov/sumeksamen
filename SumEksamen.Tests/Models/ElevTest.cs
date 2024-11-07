using JetBrains.Annotations;
using SumEksamen.Models;
using Xunit;

namespace SumEksamen.Tests.Models;

[TestSubject(typeof(Elev))]
public class ElevTest
{

    [Fact]
    public void OpretElev()
    {
        //Arrange & Act
        Elev elev = new Elev("Mikkel", 16, Status.Aktiv, Køn.dreng);
        
        //Assert
        Assert.Equal("Mikkel", elev.Navn);
        Assert.Equal(Køn.dreng, elev.Køn);
    }
}