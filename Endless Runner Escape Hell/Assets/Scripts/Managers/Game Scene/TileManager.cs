using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public List<GameObject> tiles;
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

        //spawns the max amount of tiles at the begining of the game
        for (int i = 0; i < amountOfTilesOnScreen; i++)
        {
            if(i < amountOfBlankTilesAtStart)
                SpawnTile(0);
            else
                SpawnTile(); 
        }
    }


    void Update()
    {
        //Debug.Log("If " + (playerT.position.z - safeZone).ToString() + " > " + (spawnZ - amountOfTilesOnScreen * tileLength).ToString());

        //Everytime the player crosses a tile it spawns a new tile in front and deletes the one at the back
        if (playerT.position.z - safeZone > (spawnZ - amountOfTilesOnScreen * tileLength))
        {
            //Debug.Log("Move 1 Tile");
            SpawnTile();
            DeleteTile();
        }
    }

    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject go;
        if(prefabIndex == -1)
            go = Instantiate(tiles[RandomTileIndex()]) as GameObject;
        else
            go = Instantiate(tiles[prefabIndex]) as GameObject;
        
        go.transform.SetParent(this.transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go); //add the new tile to a list of the current tiles
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]); //destroy the tile at the back
        activeTiles.RemoveAt(0); //remove the destroyed tile from the array of active tiles
    }

    //used to pick a random tile index that isnt the last index that was used
    private int RandomTileIndex()
    {
        if(tiles.Count <= 1)
            return 0;

        int randomIndex = lastTileIndex;
        while(randomIndex == lastTileIndex)
        {
            randomIndex = UnityEngine.Random.Range(0, tiles.Count);
        }

        lastTileIndex = randomIndex;
        return randomIndex;
    }
}
