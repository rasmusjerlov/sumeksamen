namespace SumEksamen.Models;

public class Elev
{
    private string _navn;
    private int _alder;
    private Status _status;
    private string _køn;
    
    public Elev(string navn, int alder, Status status, string køn)
    {
        _navn = navn;
        _alder = alder;
        _status = status;
        _køn = køn;
    }

    public Elev(string navn, int alder)
    {
        _navn = navn;
        _alder = alder;
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
    
    public override string ToString()
    {
        return $"Navn: {_navn}, Alder: {_alder}, Status: {_status}";
    }
    
}