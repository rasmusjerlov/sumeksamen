namespace SumEksamen.Models;

public class Venteliste
{
    public string Aargang { get; set; }
    public DateTime OprettelsesDato { get; set; }
    private List<Elev> elever;

    public Venteliste(string aargang, DateTime oprettelsesDato)
    {
        Aargang = aargang;
        OprettelsesDato = oprettelsesDato;
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
            throw new ArgumentException();
        }
    }

    public List<Elev> hentElever()
    {
        return elever;
    }
}

