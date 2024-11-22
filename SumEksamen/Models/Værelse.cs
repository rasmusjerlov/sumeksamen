namespace SumEksamen.Models;

public class Værelse
{
    public static int VærelsesId = 1;
    public List<Elev> ElevListe {get; set;}
    public int AntalPladser {get; set;}
    
    public Værelse(int antalPladser)
    {
        ElevListe = new List<Elev>(antalPladser);
        AntalPladser = antalPladser;
        VærelsesId++;
    }
    
    public void AddElev(Elev elev)
    {
        ElevListe.Add(elev);
    }
    
    public void RemoveElev(Elev elev)
    {
        ElevListe.Remove(elev);
    }

    public List<Elev> HentVærelse()
    {
        return ElevListe;
    }
    
    public int VærelelsesId
    {
        get => VærelsesId;
        set => VærelsesId = value;
    }
    
    
}