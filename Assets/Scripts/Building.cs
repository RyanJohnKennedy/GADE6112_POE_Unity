using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building
{
    protected int posX, posY;
    protected int health, maxHealth;
    protected Faction factionType;
    protected string symbol;

    public Building(int x, int y, int hp, string sym, Faction faction)
    {
        posX = x;
        posY = y;
        health = hp;
        symbol = sym;
        factionType = faction;

        maxHealth = hp;
    }

    public abstract bool Death();
    public abstract override string ToString();
}
