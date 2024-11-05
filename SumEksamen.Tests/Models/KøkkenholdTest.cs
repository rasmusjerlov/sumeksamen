using JetBrains.Annotations;
using SumEksamen.Models;
using Xunit;

namespace SumEksamen.Tests.Models;

[TestSubject(typeof(Køkkenhold))]
public class KøkkenholdTest
{

    [Fact]
    public void KøkkenholdManueltOpret()
    {
        //Arrange & Act
        Elev e1 = new Elev("Mikkel", 13);
        Elev e2 = new Elev("Mazza", 16);
        Elev e3 = new Elev("Tully", 13);
        Elev e4 = new Elev("Jens", 13);
        Køkkenhold køkkenhold = new Køkkenhold(e1, e2, e3, e4);

        //Assert
        Assert.NotNull(køkkenhold);
        Assert.NotEmpty(køkkenhold.GetElevListe());
    }
    
}