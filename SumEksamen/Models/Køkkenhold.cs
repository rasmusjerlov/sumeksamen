namespace SumEksamen.Models;

public class Køkkenhold
{
    private List<Elev> holdListe = new List<Elev>(4);
    private DateTime startDato;
    private DateTime slutDato;

    public Køkkenhold(params Elev[] elever)
    {
        if (elever.Length != 4)
        {
            throw new ArgumentException("Der skal være 4 elever");
        }
        holdListe.AddRange(elever);
    }

    public List<Elev> GetElevListe()
    {
        return holdListe;
    }
    
    
}