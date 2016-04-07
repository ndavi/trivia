using System;
using System.Dynamic;

public class Player
{
    public string Name { get; private set; }

    public int Purses { get; private set; }

    public int Places = 0;

    public bool PenalityBox = false;

    public Player(string nom)
    {
        Name = nom;
    }

    public void WinOnePurse()
    {
        this.Purses++;
    }
}
