namespace SumEksamen.Models;

public class Elev
{
    private string _navn;
    private int _alder;
    private Status _status;
    private Køn _køn;
    private int _elevNr; 
    private List<Bemærkning> bemærkninger = new List<Bemærkning>();
    
    
    public Elev(int elevNr, string navn, Køn køn, Status status)
    {
        _navn = navn;
        _status = status;
        _køn = køn;
        _elevNr = elevNr;
    }
    public Elev(string navn, int alder, Status status, Køn køn)
    {
        _navn = navn;
        _alder = alder;
        _status = status;
        _køn = køn;
    }

    public Elev(string navn, int alder, Køn køn)
    {
        _navn = navn;
        _alder = alder;
        _køn = køn;
    }
    
    public Elev(string navn, Køn køn)
    {
        _navn = navn;
        _køn = køn;
    }
    
    
    public string Navn
    {
        get => _navn;
        set => _navn = value;
    }
    
    public int Alder
    {
        get => _alder;
        set => _alder = value;
    }
    
    public Status Status
    {
        get => _status;
        set => _status = value;
    }
    
    public Køn Køn
    {
        get => _køn;
        set => _køn = value;
    }
    
    public int ElevNr
    {
        get => _elevNr;
        set => _elevNr = value;
    }

    public void tilfojBemærkning(Bemærkning bemærkning)
    {
        bemærkninger.Add(bemærkning);
    }
    public List<Bemærkning> Bemærkninger
    {
        get => bemærkninger;
        set => bemærkninger = value;
    }
    
    
    public string hentBemærkninger()
    {
        return bemærkninger.Any() 
            ? string.Join(", ", bemærkninger.Select(b => b.ToString())) 
            : "Ingen bemærkninger.";
    }


    public override string ToString()
    {
        return $"ElevNr: {_elevNr}, Navn: {_navn}, Alder: {_alder}, Køn: {_køn}, Status: {_status}, Bemærkninger: {bemærkninger}";
    }
    
}