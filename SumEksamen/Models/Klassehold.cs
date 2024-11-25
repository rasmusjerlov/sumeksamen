namespace SumEksamen.Models;

public class Klassehold
{
    private string fag;
    private List<Elev> _elever { get; set; }
    private string lokale;

    public Klassehold(string fag, string lokale)
    {
        this.fag = fag;
        this.lokale = lokale;
    }

    public void tilfojElev(Elev elev)
    {
        if (!_elever.Contains(elev))
        {
            _elever.Add(elev);
        }
        else
        {
            Console.WriteLine("Eleven er allerede tildelt klasseholdet.");
        }
    }

    public void fjernElev(Elev elev)
    {
        if (findElev(elev) != null)
        {
            _elever.Remove(findElev(elev));
        }
        else
        {
            throw new ArgumentException("Elev findes ikke.");
        };
    }

    public Elev findElev(Elev elev)
    {
        Elev returElev = _elever.Find(e => e == elev);
        if (returElev != null)
        {
            return returElev;
        }
        else
        {
            throw new ArgumentException("Elev findes ikke.");
        }
    }

    public string Fag => fag;

    public string Lokale => lokale;

    public List<Elev> hentElever()
    {
        return _elever;
    }
}