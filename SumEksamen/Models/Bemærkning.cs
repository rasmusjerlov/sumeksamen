namespace SumEksamen.Models;

public class Bemærkning
{
    private DateTime dato;
    private string tekst;

    public Bemærkning(DateTime dato, string tekst)
    {
        this.dato = dato;
        this.tekst = tekst;
    }

    public DateTime Dato
    {
        get => dato;
        set => dato = value;
    }

    public string Tekst
    {
        get => tekst;
        set => tekst = value;
    }

    public override string ToString()
    {
        return $"{dato}: {tekst}";
    }
}