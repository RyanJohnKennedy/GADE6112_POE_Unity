using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEngine : MonoBehaviour
{
    int gameTick;
    bool runGame = false;

    //All texts for the UI
    public Text txtPausePlay;
    public Text txtRound;
    //Radient texts
    public Text txtRadientResourseLeft;
    public Text txtRadientResourseGathered;
    public Text txtRadientUnits;
    //Dire texts
    public Text txtDireResourseLeft;
    public Text txtDireResourseGathered;
    public Text txtDireUnits;

    //Variables that can be adjusted by the user to change the map size
    const int mapHeight = 20;
    const int mapWidth = 20;

    //Variables to indacate how many resources each team has
    int direResources = 0;
    int radientResources = 0;

    //Variables to adjust how many units and buildings will spawn
    static int unitNum = 8;
    static int buildingNum = 6;

    //Map object
    Map m = new Map(unitNum, buildingNum, mapHeight, mapWidth);

    //GameObjects
    public GameObject emptyTile;
    public GameObject meleeUnitDire;
    public GameObject meleeUnitRadient;
    public GameObject rangedUnitDire;
    public GameObject rangedUnitRadient;
    public GameObject wizardUnitNeutral;
    public GameObject factoryBuildingDire;
    public GameObject factoryBuildingRadient;
    public GameObject resourceBuildingDire;
    public GameObject resourceBuildingRadient;

    // Start is called before the first frame update
    void Start()
    {
        Display();
        m.GenerateBattlefeild();
        InisialiseMap();
        m.PlaceUnits();
        m.PlaceBuildings();
        PlaceGameObjects();
    }

    // Update is called once per frame
    void Update()
    {
        if (runGame)
        {
            if (gameTick == 20)
            {
                GameLogic();
                InisialiseMap();
                m.PlaceUnits();
                m.PlaceBuildings();
                PlaceGameObjects();

                gameTick = 0;
            }
            else
            {
                gameTick++;
            }
        }
        else
        {
            gameTick = 0;
        }
    }

    public void PlayPause()
    {
        if (runGame == false)
        {
            runGame = true;
            txtPausePlay.text = "Pause";
        }
        else
        {
            runGame = false;
            txtPausePlay.text = "Unpause";
        }
    }

    private void GameLogic()
    {
        Display();

        foreach (ResourceBuilding RB in m.mines)
        {
            if (RB.FactionType == Faction.Dire)
            {
                direResources += RB.GenerateResource();
            }
            else if (RB.FactionType == Faction.Radient)
            {
                radientResources += RB.GenerateResource();
            }
        }

        foreach (FactoryBuilding FB in m.factories)
        {
            Unit u = FB.SpawnUnit();

            if (FB.FactionType == Faction.Dire && direResources > FB.SpawnCost)
            {
                if (m.round % FB.SpawnSpeed == 0)
                {
                    m.units.Add(u);

                    if (u is MeleeUnit)
                    {
                        MeleeUnit M = (MeleeUnit)u;

                        M.MapHeight = mapHeight;
                        M.MapWidth = mapWidth;
                        m.meleeUnits.Add(M);
                    }
                    else if (u is RangedUnit)
                    {
                        RangedUnit R = (RangedUnit)u;

                        R.MapHeight = mapHeight;
                        R.MapWidth = mapWidth;
                        m.rangedUnits.Add(R);
                    }
                    direResources -= FB.SpawnCost;
                }
            }
            else if (FB.FactionType == Faction.Radient && radientResources > FB.SpawnCost)
            {
                if (m.round % FB.SpawnSpeed == 0)
                {
                    m.units.Add(u);

                    if (u is MeleeUnit)
                    {
                        MeleeUnit M = (MeleeUnit)u;

                        m.meleeUnits.Add(M);
                    }
                    else if (u is RangedUnit)
                    {
                        RangedUnit R = (RangedUnit)u;

                        m.rangedUnits.Add(R);
                    }
                    radientResources -= FB.SpawnCost;
                }
            }
        }

        foreach (Unit u in m.units)
        {
            u.CheckAttackRange(m.units, m.buildings);
        }

        m.round++;
        CheckDeath();
        m.PlaceUnits();
        m.PlaceBuildings();
        PlaceGameObjects();
    }

    public void CheckDeath()
    {
        //Checks to see who has died and needs to be deleted
        for (int i = 0; i < m.rangedUnits.Count; i++)
        {
            if (m.rangedUnits[i].Death())
            {
                m.rangedUnits.RemoveAt(i);
            }
        }

        for (int i = 0; i < m.meleeUnits.Count; i++)
        {
            if (m.meleeUnits[i].Death())
            {
                m.meleeUnits.RemoveAt(i);
            }
        }

        for (int i = 0; i < m.wizardUnits.Count; i++)
        {
            if (m.wizardUnits[i].Death())
            {
                m.wizardUnits.RemoveAt(i);
            }
        }

        for (int i = 0; i < m.units.Count; i++)
        {
            if (m.units[i].Death())
            {
                if (m.units[i] is MeleeUnit)
                {
                    MeleeUnit M = (MeleeUnit)m.units[i];
                }
                else if (m.units[i] is RangedUnit)
                {
                    RangedUnit R = (RangedUnit)m.units[i];
                }
                else if (m.units[i] is WizardUnit)
                {
                    WizardUnit W = (WizardUnit)m.units[i];
                }

                m.units.RemoveAt(i);
            }
        }

        for (int i = 0; i < m.factories.Count; i++)
        {
            if (m.factories[i].Death())
            {
                m.factories.RemoveAt(i);
            }
        }

        for (int i = 0; i < m.mines.Count; i++)
        {
            if (m.mines[i].Death())
            {
                m.mines.RemoveAt(i);
            }
        }

        for (int i = 0; i < m.buildings.Count; i++)
        {
            if (m.buildings[i].Death())
            {
                if (m.buildings[i] is FactoryBuilding)
                {
                    FactoryBuilding FB = (FactoryBuilding)m.buildings[i];
                }
                else if (m.buildings[i] is ResourceBuilding)
                {
                    ResourceBuilding RB = (ResourceBuilding)m.buildings[i];
                }

                m.buildings.RemoveAt(i);
            }
        }
    }

    public void InisialiseMap()
    {
        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                m.mapTiles[i, j] = Tiles.emptyTile;
            }
        }
    }

    public void PlaceGameObjects()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("tile");
        foreach (GameObject g in tiles)
        {
            Destroy(g);
        }

        for (int x = 0; x < mapWidth; x++)
        {
            for (int z = 0; z < mapHeight; z++)
            {
                if (m.mapTiles[x, z] == Tiles.emptyTile)
                {
                    Instantiate(emptyTile, new Vector3(x, 0f, z), Quaternion.identity);
                }
                else if (m.mapTiles[x, z] == Tiles.meleeUnitDire)
                {
                    Instantiate(meleeUnitDire, new Vector3(x, 0.5f, z), Quaternion.identity);
                }
                else if (m.mapTiles[x, z] == Tiles.meleeUnitRadient)
                {
                    Instantiate(meleeUnitRadient, new Vector3(x, 0.5f, z), Quaternion.identity);
                }
                else if (m.mapTiles[x, z] == Tiles.rangedUnitDire)
                {
                    Instantiate(rangedUnitDire, new Vector3(x, 0.5f, z), Quaternion.identity);
                }
                else if (m.mapTiles[x, z] == Tiles.rangedUnitRadient)
                {
                    Instantiate(rangedUnitRadient, new Vector3(x, 0.5f, z), Quaternion.identity);
                }
                else if (m.mapTiles[x, z] == Tiles.wizardUnitNeutral)
                {
                    Instantiate(wizardUnitNeutral, new Vector3(x, 0.5f, z), Quaternion.identity);
                }
                else if (m.mapTiles[x, z] == Tiles.factoryBuildingDire)
                {
                    Instantiate(factoryBuildingDire, new Vector3(x, 0.5f, z), Quaternion.identity);
                }
                else if (m.mapTiles[x, z] == Tiles.factoryBuildingRadient)
                {
                    Instantiate(factoryBuildingRadient, new Vector3(x, 0.5f, z), Quaternion.identity);
                }
                else if (m.mapTiles[x, z] == Tiles.resourceBuildingDire)
                {
                    Instantiate(resourceBuildingDire, new Vector3(x, 0.5f, z), Quaternion.identity);
                }
                else if (m.mapTiles[x, z] == Tiles.resourceBuildingRadient)
                {
                    Instantiate(resourceBuildingRadient, new Vector3(x, 0.5f, z), Quaternion.identity);
                }
            }
        }
    }

    public void Display()
    {
        txtRound.text = "Round: " + m.round;
        

    }
}
