using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building
{
    protected int posX, posY;
    protected int health, maxHealth;
    protected Faction factionType;

    public Building(int x, int y, int hp, Faction faction)
    {
        posX = x;
        posY = y;
        health = hp;
        factionType = faction;

        maxHealth = hp;
    }

    public abstract bool Death();
}
