using System;
using Microsoft.AspNetCore.Http.HttpResults;
using SumEksamen.Controllers;
using Xunit;

namespace SumEksamen.Tests;

public class OpretVentelisteControllerTest
{

    [Fact]
    public void TC1_opretVenteliste()
    {
<<<<<<<< HEAD:SumEksamen.Tests/Models/OpretVentelisteControllerTest.cs
        VentelisteController vc = new VentelisteController();
        vc.CreateVenteliste("24/25");
========
        OpretVentelisteController vc = new OpretVentelisteController();
        vc.Create("24/25", new DateTime(2024, 10, 01));
>>>>>>>> main:SumEksamen.Tests/Controllers/VentelisteControllerTest.cs
        
        Assert.Contains(vc.HentVentelister(), v => v.Aargang == "24/25");
    }

    [Fact]
    public void TC2_opretVentelisteFejl()
    {
<<<<<<<< HEAD:SumEksamen.Tests/Models/OpretVentelisteControllerTest.cs
        VentelisteController vc = new VentelisteController();
        vc.CreateVenteliste("25/26");
========
        OpretVentelisteController vc = new OpretVentelisteController();
        vc.Create("24/25", new DateTime(2024, 10, 01));
>>>>>>>> main:SumEksamen.Tests/Controllers/VentelisteControllerTest.cs
        
        Assert.Throws<ArgumentException>(() => vc.CreateVenteliste("25/26"));
        //
    }
}