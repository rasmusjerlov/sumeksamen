namespace SumEksamen.Models;

public class Venteliste
{
    public List<String> VentelisteAargange = new List<string>();
    public string Aargang { get; set; }
    public DateTime OprettelsesDato { get; set; }
    private List<Elev> elever;

    public Venteliste(string aargang, DateTime oprettelsesDato)
    {
        if (VentelisteAargange.Any(a => a == aargang))
        {
            throw new ArgumentException("Venteliste med denne aargang eksisterer allerede");
        }        
        Aargang = aargang;
        OprettelsesDato = oprettelsesDato;
        elever = new List<Elev>();
        VentelisteAargange.Add(aargang);
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

    public void sletElev(Elev elev)
    {
        if (findElev(elev) != null)
        {
            elever.Remove(findElev(elev));
        }
        else
        {
            throw new ArgumentException("Elev findes ikke.");
        };
    }

    public Elev findElev(Elev elev)
    {
        Elev returElev = elever.Find(e => e == elev);
        if (returElev != null)
        {
            return returElev;
        }
        else
        {
            throw new ArgumentException("Elev findes ikke.");
        }
    }

    public void updateElev(Elev elev, string str)
    {
        Elev returElev = findElev(elev);
        if (returElev != null)
        {
            Bemærkning bemærkning = new Bemærkning(DateTime.Now, str);
            elev.tilfojBemærkning(bemærkning);
        }
        else
        {
            throw new ArgumentException("Elev findes ikke.");
        }

    }

    public List<Elev> hentElever()
    {
        return elever;
    }
}

