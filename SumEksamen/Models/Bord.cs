﻿namespace SumEksamen.Models;

public class Bord
{
    private static int currentBordNr { get; set; } = 1;
    private int bordNr { get; set; }
    private int antalPladser { get; set; }
    private List<Elev> elever { get; set; }
    
    public Bord()
    {
        this.bordNr = currentBordNr++;
        this.antalPladser = 8;
    }
    
    public Bord(int antalPladser)
    {
        this.bordNr = currentBordNr++;
        this.antalPladser = antalPladser;
    }
    
    


}