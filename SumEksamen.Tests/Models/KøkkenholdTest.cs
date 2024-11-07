using System;
using JetBrains.Annotations;
using SumEksamen.Models;
using Xunit;

namespace SumEksamen.Tests.Models;

[TestSubject(typeof(Køkkenhold))]
public class KøkkenholdTest
{

    [Fact]
    public void KøkkenholdConstructorMedFireElever()
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

    [Fact]
    public void TilføjElevTilFuldtKøkkenhold()
    {
        //Arrange
        Elev e1 = new Elev("Mikkel", 13);
        Elev e2 = new Elev("Mazza", 16);
        Elev e3 = new Elev("Tully", 13);
        Elev e4 = new Elev("Jens", 13);
        Elev e5 = new Elev("Abukar", 15);
        Køkkenhold køkkenhold = new Køkkenhold(e1, e2, e3, e4);
        
        //Act & Assert
        //Kaster fejl, hvis hold bliver over 4 elever
        Assert.Throws<InvalidOperationException>(() => køkkenhold.AddElev(e5)); 
    }

    [Fact]
    public void KøkkenholdConstructorMedOverFireElever()
    {
        //Arrange
        Elev e1 = new Elev("Mikkel", 13);
        Elev e2 = new Elev("Mazza", 16);
        Elev e3 = new Elev("Tully", 13);
        Elev e4 = new Elev("Jens", 13);
        Elev e5 = new Elev("Abukar", 15);
        
        //Act & Assert
        //Kaster fejl, hvis hold bliver lavet med mere end 4 elever
        Assert.Throws<ArgumentException>(() => new Køkkenhold(e1, e2, e3, e4, e5)); 
    }

    [Fact]
    public void KøkkenholdConstructorMedFærreEndFireElever()
    {
        Elev e1 = new Elev("Mikkel", 13);
        Elev e2 = new Elev("Mazza", 16);
        Elev e3 = new Elev("Tully", 13);
        
        //Act & Assert
        //Kaster fejl, hvis hold bliver lavet med mindre end 4 elever
        Assert.Throws<ArgumentException>(() => new Køkkenhold(e1, e2, e3));
    }
    
}