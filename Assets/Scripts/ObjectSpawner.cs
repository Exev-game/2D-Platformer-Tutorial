using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjectSpawner : MonoBehaviour
{
    public enum ObjectType { SmallGem, BigGem, Enemy, Gem };

    public Tilemap tilemap;
    public GameObject[] objectPrefabs; // 0=SmallGem, 1=BigGem, 2=Enemy, 3=Gem
    public float bigGemProbability = 0.2f; //20 % chance of spawning big gem
    public float enemyProbability = 0.1f; //chance of spawning enemies
    public float smallGemProbability = 0.3f;
    public float gemProbability = 0.3f;
    public int maxObjects = 5; //max amount of things spawning
    public float gemLifeTime = 10f; //gems dissappear over time
    public float spawnInterval = 0.5f;

    private List<Vector3> validSpawnPositions = new List<Vector3>();
    private List <GameObject> spawnObjects = new List<GameObject>();
    private bool isSpawning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Gather Valid Positions for spawning
        GatherValidPositions();
        //coroutine
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Finds valid spawning positions for gems and enemies
    private void GatherValidPositions()
    {
        validSpawnPositions.Clear(); //Clears the level in case level changes to a new one
        BoundsInt boundsInt = tilemap.cellBounds; //"returns bounds of the tilemap in cellsize"
        TileBase[] allTiles = tilemap.GetTilesBlock(boundsInt);
        Vector3 start = tilemap.CellToWorld(new Vector3Int (boundsInt.xMin, boundsInt.yMin, 0));


        //Looping X and Y. If it finds a tile, it determines that this is a valid spawning place
        for (int x = 0; x < boundsInt.size.x; x++)
        {
            for (int y = 0; x < boundsInt.size.y; y++)
            {
                TileBase tile = allTiles[x + y * boundsInt.size.x];
                if (tile!= null)
                {
                    Vector3 place = start + new Vector3 (x + 0.5f, y + 2f, 0);
                    validSpawnPositions.Add(place);
                }
            }
        }
    }
}
