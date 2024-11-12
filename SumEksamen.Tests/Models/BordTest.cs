using JetBrains.Annotations;
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
        b1 = new Bord();
        b2 = new Bord();
        b3 = new Bord();
        b4 = new Bord();
    }
    
    [Fact]
    public void TC1_OpretBordTest()
    {
        
        
    }
}