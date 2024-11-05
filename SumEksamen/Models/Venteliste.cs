namespace SumEksamen.Models;

public class Venteliste
{
    private List<Elev> elever = new List<Elev>();
    private string _aargang;
    private DateTime _oprettelsDato;

    public Venteliste(string aargang, DateTime oprettelsDato)
    {
        _aargang = aargang;
        _oprettelsDato = oprettelsDato;
    }

    public void tilfojElev(Elev elev)
    {
        if (!elever.Contains(elev))
        {
            elever.Add(elev);
        }
    }

    public List<Elev> hentElever()
    {
        return new List<Elev>(elever);
    }
    
    public string Aargang { get; set; }
    
    public DateTime OprettelsesDato { get; set; }
    
    
}
