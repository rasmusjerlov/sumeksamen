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
        værelseController = new VærelseController();
        værelseController.OpretVærelse("1", 4);
        Elev e1 = new Elev("mikkel", Køn.dreng);
        Elev e2 = new Elev("julian", Køn.dreng);
        Elev e3 = new Elev("mads", Køn.dreng);
        Elev e4 = new Elev("Martin", Køn.dreng);
        
    }
    

    [Fact]
    public void TC1_FordelEleverPåVærelser()
    {
        
        
        
    }
}


