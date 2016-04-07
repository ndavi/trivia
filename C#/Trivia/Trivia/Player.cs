using System;

public class Player
{
    public string Name { get; private set; }

    public int Purses { get; private set; }

    public Player(string nom)
    {
        this.Name = nom;
    }

    public void WinOnePurse()
    {
        this.Purses++;
    }
}
