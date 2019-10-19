using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryBuilding : Building
{
    public int PosX
    {
        get { return base.posX; }
        set { base.posX = value; }
    }

    public int PosY
    {
        get { return base.posY; }
        set { base.posY = value; }
    }

    public int Health
    {
        get { return base.health; }
        set { base.health = value; }
    }

    public string Symbol
    {
        get { return base.symbol; }
    }

    public Faction FactionType
    {
        get { return base.factionType; }
    }

    private int spawnSpeed;

    public int SpawnSpeed
    {
        get { return spawnSpeed; }
    }

    public string UnitType
    {
        get { return unitType; }
    }

    public int SpawnPointX
    {
        get { return spawnPointX; }
        set { spawnPointX = value; }
    }

    public int SpawnPointY
    {
        get { return spawnPointY; }
        set { spawnPointY = value; }
    }

    private int spawnCost;

    public int SpawnCost
    {
        get { return spawnCost; }
        set { spawnCost = value; }
    }

    private string unitType;
    private int spawnPointX, spawnPointY;

    public FactoryBuilding(int x, int y, int hp, string sym, Faction faction, int sSpeed, string uType, int sCost)
        : base(x, y, hp, sym, faction)
    {
        spawnSpeed = sSpeed;
        unitType = uType;

        SpawnCost = sCost;
    }

    //Returns a unit to be spawned
    public Unit SpawnUnit()
    {
        if (unitType == "Melee")
        {
            MeleeUnit knight = new MeleeUnit("Knight", SpawnPointX, SpawnPointY, factionType, 40, 1, 5, 1, "/", false);
            return knight;
        }
        else
        {
            RangedUnit archer = new RangedUnit("Archer", SpawnPointX, SpawnPointY, FactionType, 30, 1, 3, 3, "{|", false);
            return archer;
        }
    }

    //Returns if the building has more than 0 health of not
    public override bool Death()
    {
        if (Health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //The info of the building
    public override string ToString()
    {
        return "Barracks: X: " + PosX + " Y: " + PosY
            + "\nHP: " + Health
            + "\nFaction " + FactionType
            + "\nSpawn speed: " + SpawnSpeed
            + "\nUnit spawning: " + unitType;
    }
}
