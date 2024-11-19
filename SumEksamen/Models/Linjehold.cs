namespace SumEksamen.Models;

public class Linjehold
{
    public string type { get; set; }
    
    public int kapacitet { get; set; }
    
    public Køn køn { get; set; }
    
    private List<Elev> elever;
    
    public Linjehold(string type, int kapacitet, Køn køn)
    {
        this.type = type;
        this.kapacitet = kapacitet;
        this.køn = køn;
        elever = new List<Elev>();
    }
    
    public void tilfojElevTilLinjehold(Elev elev)
    {
        if (!elever.Contains(elev))
        {
            elever.Add(elev);
        }
        else
        {
            throw new ArgumentException("Elev findes allerede.");
        }
    }
    
    public List<Elev> hentEleverLinjehold()
    {
        return elever;
    }
    
    
}