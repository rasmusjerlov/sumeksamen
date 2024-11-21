using JetBrains.Annotations;
using SumEksamen.Controllers;
using SumEksamen.Models;
using Xunit;

namespace SumEksamen.Tests.Controllers;

[TestSubject(typeof(VærelseController))]
public class VærelseControllerTest
{
    private VærelseController værelseController;
    
    public VærelseControllerTest()
    {
        værelseController = new VærelseController(new VentelisteController());
        Værelse værelse = new Værelse(4);
        værelseController.TilføjVærelse(værelse);
        Elev e1 = new Elev("mikkel", Køn.dreng);
        Elev e2 = new Elev("julian", Køn.dreng);
        Elev e3 = new Elev("mads", Køn.dreng);
        Elev e4 = new Elev("Martin", Køn.dreng);
        værelseController.TilføjElev(værelse, e1);
        værelseController.TilføjElev(værelse, e2);
        værelseController.TilføjElev(værelse, e3);
        værelseController.TilføjElev(værelse, e4);
    }
    

    [Fact]
    public void TC1_FordelEleverPåVærelser()
    {
        
        
        
    }
}


