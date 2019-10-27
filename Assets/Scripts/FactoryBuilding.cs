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

    public int MaxHealth
    {
        get { return base.maxHealth; }
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

    public FactoryBuilding(int x, int y, int hp, Faction faction, int sSpeed, string uType, int sCost)
        : base(x, y, hp, faction)
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
            MeleeUnit knight = new MeleeUnit(SpawnPointX, SpawnPointY, FactionType, 40, 1, 5, 1, false);
            return knight;
        }
        else
        {
            RangedUnit archer = new RangedUnit(SpawnPointX, SpawnPointY, FactionType, 25, 1, 2, 3, false);
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
}
