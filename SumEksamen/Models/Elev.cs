namespace SumEksamen.Models;

public class Elev
{
    private string _navn;
    private int _alder;
    private Status _status;
    private Køn _køn;
    
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
    
    public override string ToString()
    {
        return $"Navn: {_navn}, Alder: {_alder}, Status: {_status}";
    }
    
}