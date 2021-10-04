using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManagerOptimised : MonoBehaviour
{
    private TileObjectPooler top;

    public List<GameObject> tiles;
    private List<GameObject> activeTiles;
    private Transform playerT;

    private float spawnZ; //at what z position is the next tile going to spawned at
    [SerializeField] private float tileLength = 16.653f; // how far forward the next tile needs to be spawnd from the current one
    [SerializeField] private float safeZone = 10f; // amount of units you need to be in front of the tile behind you before it is deleted
    [SerializeField] private float amountOfBlankTilesAtStart = 3f; // the amount of blank tiles (0) at the start of the game
    [SerializeField] private int amountOfTilesOnScreen = 8; // the amount of tiles the game has on screen at any given point
    private int lastTileIndex = 0; //the index of the last spawned tile (used to stop the same tile repeating)

    void Start()
    {
        top = this.GetComponent<TileObjectPooler>();

        safeZone = tileLength + safeZone;

        activeTiles = new List<GameObject>();
        playerT = GameObject.FindGameObjectWithTag("Player").transform;
        spawnZ = -tileLength; //this will spawn a tile behind the player first

        SpawnStartTiles();
    }


    void Update()
    {
        //Everytime the player crosses a tile it spawns a new tile in front and deletes the one at the back
        if (playerT.position.z - safeZone > (spawnZ - amountOfTilesOnScreen * tileLength))
        {
            ActivateTile();
            //DeactivateTile();
        }
    }

    private void SpawnStartTiles()
    {
        for (int i = 0; i < top.blankTiles.Count; i++)
        {
            Debug.Log(i);
            top.blankTiles[i].SetActive(true);
            top.blankTiles[i].transform.position = Vector3.forward * spawnZ;
            spawnZ += tileLength;
        }
    }

    private void ActivateTile()
    {
        GameObject go = top.GetRandomTile();
        go.SetActive(true);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
    }

    private void DeactivateTile()
    {
        Destroy(activeTiles[0]); //destroy the tile at the back
        activeTiles.RemoveAt(0); //remove the destroyed tile from the array of active tiles
    }

    //used to pick a random tile index that isnt the last index that was used
    private int RandomTileIndex()
    {
        if (tiles.Count <= 1)
            return 0;

        int randomIndex = lastTileIndex;
        while (randomIndex == lastTileIndex)
        {
            randomIndex = UnityEngine.Random.Range(0, tiles.Count);
        }

        lastTileIndex = randomIndex;
        return randomIndex;
    }
}
