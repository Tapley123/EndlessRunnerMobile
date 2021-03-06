using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManagerOptimised : MonoBehaviour
{
    private List<GameObject> activeTiles;
    [SerializeField] private Transform playerT;

    private float spawnZ; //at what z position is the next tile going to spawned at
    [SerializeField] private float tileLength = 16.653f; // how far forward the next tile needs to be spawnd from the current one
    [SerializeField] private float safeZone = 10f; // amount of units you need to be in front of the tile behind you before it is deleted
    [SerializeField] private float amountOfBlankTilesAtStart = 3f; // the amount of blank tiles (0) at the start of the game
    [SerializeField] private int amountOfTilesOnScreen = 8; // the amount of tiles the game has on screen at any given point
    private int lastTileIndex = 0; //the index of the last spawned tile (used to stop the same tile repeating)

    void Start()
    {
        safeZone = tileLength + safeZone;

        activeTiles = new List<GameObject>();
        //playerT = GameObject.FindGameObjectWithTag("Player").transform;
        spawnZ = -tileLength; //this will spawn a tile behind the player first

        SpawnStartTiles();
    }


    void Update()
    {
        //Debug.Log("If " + (playerT.position.z - safeZone).ToString() + " > " + (spawnZ - amountOfTilesOnScreen * tileLength).ToString());

        //Everytime the player crosses a tile it spawns a new tile in front and deletes the one at the back
        if (playerT.position.z - safeZone > (spawnZ - amountOfTilesOnScreen * tileLength))
        {
            //Debug.Log("Move 1 Tile");
            ActivateTile();
            DeactivateTile();
        }
    }

    private void SpawnStartTiles()
    {
        for (int i = 0; i < amountOfTilesOnScreen; i++)
        {
            GameObject go;
            if (i < TileObjectPooler.current.blankTiles.Count)
            {
                go = TileObjectPooler.current.blankTiles[i];
            }
            else
            {
                go = TileObjectPooler.current.GetRandomTile();
            }
             
            go.SetActive(true);
            go.transform.position = Vector3.forward * spawnZ;
            spawnZ += tileLength;

            activeTiles.Add(go);
        }
    }

    private void ActivateTile()
    {
        GameObject go = TileObjectPooler.current.GetRandomTile();

        go.SetActive(true);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;

        //go.GetComponent<Tile>().TurnObstacleOn();

        activeTiles.Add(go);
    }

    private void DeactivateTile()
    {
        activeTiles[0].GetComponent<Tile>().TurnCoinOn();
        activeTiles[0].SetActive(false);
        activeTiles.RemoveAt(0);
    }
}
