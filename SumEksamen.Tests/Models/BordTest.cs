using System.Collections.Generic;
using JetBrains.Annotations;
using SumEksamen.Controllers;
using SumEksamen.Models;
using Xunit;

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
    public void TC1_OpretBordTest()
    {
        //Arrange & Act
        Bord bord = new Bord(12);

        //Assert
        Assert.Equal(12, bord.antalPladser);
        Assert.Equal(5, bord.bordNr);
    }

    [Fact]
    public void TC2_AendreAntalPladserTest()
    {
        //Arrange & Act
        b1.antalPladser = 10;

        //Assert
        Assert.Equal(10, b1.antalPladser);
    }
}