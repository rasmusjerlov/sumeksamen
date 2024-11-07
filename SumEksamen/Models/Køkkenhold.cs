namespace SumEksamen.Models;

public class Køkkenhold
{
    private static int _currentUgeNr = 33;
    private List<Elev> _holdListe = new List<Elev>(4);
    private int _ugeNr;
    
    public Køkkenhold(params Elev[] elever)
    {
        if (elever.Length != 4)
        {
            throw new ArgumentException("Der skal være 4 elever");
        }

        _ugeNr = _currentUgeNr++;
        _holdListe.AddRange(elever);
    }

    public void AddElev(Elev elev)
    {
        if (_holdListe.Count >= 4)
        {
            throw new InvalidOperationException("Kan ikke tilføje flere end 4 elever");
        }
        _holdListe.Add(elev);
    }

    public List<Elev> GetElevListe()
    {
        return _holdListe;
    }
}