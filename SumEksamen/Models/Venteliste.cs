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

    public void sletElev(int elevNr)
    {
        if (findElev(elevNr) != 0)
        {
            elever.RemoveAt(findElev(elevNr));
        }
        else
        {
            throw new ArgumentException("Elev findes ikke.");
        };
    }

    public int findElev(int elevNr)
    {
        foreach (var elev in elever)
        {
            if (elev.ElevNr == elevNr)
            {
                return elever.IndexOf(elev);
            }
        }

        return 0;
    }

    public List<Elev> hentElever()
    {
        return elever;
    }
}

