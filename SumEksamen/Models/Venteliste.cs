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
        elever.Add(elev);
    }

    public List<Elev> hentElever()
    {
        return elever;
    }
}

