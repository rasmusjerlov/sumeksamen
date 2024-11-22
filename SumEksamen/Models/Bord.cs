﻿namespace SumEksamen.Models;

public class Bord
{
    public static int currentBordNr { get; set; } = 1;
    public int bordNr { get; set; }
    public int antalPladser { get; set; }
    public List<Elev> elever { get; set; } = new List<Elev>();

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

    public void TilføjElevTilBord(Elev elev)
    {
        elever.Add(elev);
    }
}