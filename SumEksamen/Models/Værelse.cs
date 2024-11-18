namespace SumEksamen.Models;

public class Værelse
{
    public static int væresesId = 1;
    public List<Elev> ElevListe {get; set;}
    public int AntalPladser {get; set;}
    
    public Værelse()
    {
    }
    
    
}