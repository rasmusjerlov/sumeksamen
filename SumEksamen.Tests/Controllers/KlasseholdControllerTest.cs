using SumEksamen.Controllers;
using SumEksamen.Models;
using SumEksamen.Services;
using Xunit;

namespace SumEksamen.Tests.Controllers;

public class KlasseholdControllerTest
{
    private KlasseholdController khc;

    public KlasseholdControllerTest()
    {
        khc = new KlasseholdController();
    }

    [Fact]

    public void TC1_OpretKlassehold_withValidFag_shouldAddKlasseholdToStorage()
    {
        //Act
        khc.OpretKlassehold("Dansk", "Lokale 1");

        //Assert
        Assert.Contains(Storage.HentKlassehold(), k => k.Fag.Equals("Dansk"));
    }

    [Fact]
    public void TC2_OpretKlassehold_withUnvalidFag_shouldFail()
    {
        khc.OpretKlassehold("", "Lokale 2");
        
        Assert.DoesNotContain(Storage.HentKlassehold(), k => k.Fag.Equals(""));
    }
}