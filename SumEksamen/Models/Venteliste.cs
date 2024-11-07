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
            throw new ArgumentException();
        }
    }

    public List<Elev> hentElever()
    {
        return elever;
    }
}

