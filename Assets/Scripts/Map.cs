using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enum for telling the teams apart
public enum Faction
{
    Dire,
    Radient,
    Neutral
}

//Enum for the different resource types
public enum ResourceType
{
    Gold,
    Iron
}

public enum Tiles
{
    emptyTile,
    meleeUnitDire,
    meleeUnitRadient,
    rangedUnitDire,
    rangedUnitRadient,
    wizardUnitNeutral,
    factoryBuildingDire,
    factoryBuildingRadient,
    resourceBuildingDire,
    resourceBuildingRadient
}

public class Map : MonoBehaviour
{
    public List<Building> buildings = new List<Building>();
    public List<FactoryBuilding> factories = new List<FactoryBuilding>();
    public List<ResourceBuilding> mines = new List<ResourceBuilding>();

    public List<Unit> units = new List<Unit>();
    public List<RangedUnit> rangedUnits = new List<RangedUnit>();
    public List<MeleeUnit> meleeUnits = new List<MeleeUnit>();
    public List<WizardUnit> wizardUnits = new List<WizardUnit>();

    //private Unit[,] unitMap;
    //private Building[,] buildingMap;
    public Tiles[,] mapTiles;

    private int unitNum;
    private int buildingNum;
    public int round = 1;

    private int mapHeight = 20;
    private int mapWidth = 20;

    //Constructor called with the number of units as a parameter
    public Map(int unitN, int buildingN, int mHight, int mWidth)
    {
        unitNum = unitN;
        buildingNum = buildingN;

        mapHeight = mHight;
        mapWidth = mWidth;

        mapTiles = new Tiles[mapWidth, mapHeight];
        //buildingMap = new Building[mapWidth, mapHeight];
        //unitMap = new Unit[mapWidth, mapHeight];
    }

    //Creates the unit objects and randomises thier x and y positions
    public void GenerateBattlefeild()
    {
        //Buildings spawning
        for (int i = 0; i < buildingNum; i++)
        {
            int unitTypeN = Random.Range(0, 2);
            string unitName;

            if (unitTypeN == 0)
            {
                unitName = "Ranged";
            }
            else
            {
                unitName = "Melee";
            }

            FactoryBuilding factory = new FactoryBuilding(0, 0, 100, "|^|", Faction.Dire, Random.Range(3, 10), unitName, Random.Range(50, 71));
            factories.Add(factory);

            ResourceBuilding mine = new ResourceBuilding(0, 0, 100, "|V|", Faction.Dire, Random.Range(3, 10), ResourceType.Gold);
            mines.Add(mine);
        }

        for (int i = 0; i < buildingNum; i++)
        {
            int unitTypeN = Random.Range(0, 2);
            string unitName;

            if (unitTypeN == 0)
            {
                unitName = "Ranged";
            }
            else
            {
                unitName = "Melee";
            }

            FactoryBuilding factory = new FactoryBuilding(0, 0, 100, "|^|", Faction.Radient, Random.Range(3, 10), unitName, Random.Range(50, 71));
            factories.Add(factory);

            ResourceBuilding mine = new ResourceBuilding(0, 0, 100, "|V|", Faction.Radient, Random.Range(3, 10), ResourceType.Gold);
            mines.Add(mine);
        }

        for (int i = 0; i < (buildingNum * 2); i++)
        {
            WizardUnit wizard = new WizardUnit("Wizard", 0, 0, Faction.Neutral, 20, 2, 3, 1, "^", false);

            wizard.MapHeight = mapHeight;
            wizard.MapWidth = mapWidth;

            wizardUnits.Add(wizard);
        }

        foreach (FactoryBuilding u in factories)
        {
            for (int i = 0; i < factories.Count; i++)
            {
                int xPos = Random.Range(0, mapHeight);
                int yPos = Random.Range(0, mapWidth);

                while ((xPos == factories[i].PosX && yPos == factories[i].PosY) || (xPos == 0 && yPos == 0))
                {
                    xPos = Random.Range(0, mapHeight);
                    yPos = Random.Range(0, mapWidth);
                }

                u.PosX = xPos;
                u.PosY = yPos;
            }

            //buildingMap[u.PosY, u.PosX] = (Building)u;
            buildings.Add(u);

            u.SpawnPointY = u.PosY;

            if (u.PosX < mapHeight - 1)
            {
                u.SpawnPointX = u.PosX + 1;
            }
            else
            {
                u.SpawnPointX = u.PosX - 1;
            }
        }

        foreach (ResourceBuilding u in mines)
        {
            for (int i = 0; i < mines.Count; i++)
            {
                int xPos = Random.Range(0, mapHeight);
                int yPos = Random.Range(0, mapWidth);

                while ((xPos == mines[i].PosX && yPos == mines[i].PosY) || (xPos == factories[i].PosX && yPos == factories[i].PosY))
                {
                    xPos = Random.Range(0, mapHeight);
                    yPos = Random.Range(0, mapWidth);
                }

                u.PosX = xPos;
                u.PosY = yPos;
            }

            //buildingMap[u.PosY, u.PosX] = (Building)u;
            buildings.Add(u);
        }

        foreach (WizardUnit u in wizardUnits)
        {
            for (int i = 0; i < wizardUnits.Count; i++)
            {
                int xPos = Random.Range(0, mapHeight);
                int yPos = Random.Range(0, mapWidth);

                while ((xPos == mines[i].PosX && yPos == mines[i].PosY) || (xPos == factories[i].PosX && yPos == factories[i].PosY) || (xPos == wizardUnits[i].PosX && yPos == wizardUnits[i].PosY))
                {
                    xPos = Random.Range(0, mapHeight);
                    yPos = Random.Range(0, mapWidth);
                }

                u.PosX = xPos;
                u.PosY = yPos;
            }

            //unitMap[u.PosY, u.PosX] = (Unit)u;
            units.Add(u);
        }
    }

    public void PlaceUnits()
    {
        foreach (RangedUnit u in rangedUnits)
        {
            if (u.FactionType == Faction.Dire)
            {
                mapTiles[u.PosY, u.PosX] = Tiles.rangedUnitDire;
            }
            else
            {
                mapTiles[u.PosY, u.PosX] = Tiles.rangedUnitRadient;
            }

        }

        foreach (MeleeUnit u in meleeUnits)
        {
            if (u.FactionType == Faction.Dire)
            {
                mapTiles[u.PosY, u.PosX] = Tiles.meleeUnitDire;
            }
            else
            {
                mapTiles[u.PosY, u.PosX] = Tiles.meleeUnitRadient;
            }

        }

        foreach (WizardUnit u in wizardUnits)
        {
            mapTiles[u.PosY, u.PosX] = Tiles.wizardUnitNeutral;
        }
    }

    public void PlaceBuildings()
    {
        //for (int i = 0; i < mapWidth; i++)
        //{
        //    for (int j = 0; j < mapHeight; j++)
        //    {
        //        buildingMap[i, j] = null;
        //    }
        //}

        //foreach (Building b in buildings)
        //{
        //    if (b is FactoryBuilding)
        //    {
        //        FactoryBuilding build = (FactoryBuilding)b;
        //        buildingMap[build.PosY, build.PosX] = b;
        //    }
        //    else if (b is ResourceBuilding)
        //    {
        //        ResourceBuilding build = (ResourceBuilding)b;
        //        buildingMap[build.PosY, build.PosX] = b;
        //    }
        //}

        foreach (FactoryBuilding b in factories)
        {
            if (b.FactionType == Faction.Dire)
            {
                mapTiles[b.PosY, b.PosX] = Tiles.factoryBuildingDire;
            }
            else
            {
                mapTiles[b.PosY, b.PosX] = Tiles.factoryBuildingRadient;
            }
        }

        foreach (ResourceBuilding b in mines)
        {
            if (b.FactionType == Faction.Dire)
            {
                mapTiles[b.PosY, b.PosX] = Tiles.resourceBuildingDire;
            }
            else
            {
                mapTiles[b.PosY, b.PosX] = Tiles.resourceBuildingRadient;
            }
        }
    }
}
