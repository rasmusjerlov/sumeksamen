namespace SumEksamen.Models;

public class Venteliste
{
   
    public string Aargang { get; set; }
 
    private List<Elev> elever;

    public Venteliste(string aargang)
    {
        Aargang = aargang;
        elever = new List<Elev>();
    }

    public void tilfojElev(Elev elev)
    {
        if (!elever.Contains(elev) && elev.Alder < 18)
        {
            elever.Add(elev);
        }
        else
        {
            throw new ArgumentException("Elev skal være under 18 år.");
        }
    }

    public List<Elev> hentElever()
    {
        return elever;
    }
}

