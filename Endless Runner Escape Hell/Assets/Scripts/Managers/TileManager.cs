using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public List<GameObject> tiles;
    private List<GameObject> activeTiles;
    private Transform playerT;
    private float spawnZ;
    private float tileLength = 16.653f; // how far forward the next tile needs to be spawnd from the current one
    [SerializeField] private float safeZone = 25f;
    [SerializeField] private int amountOfTilesOnScreen = 8;

    void Start()
    {
        activeTiles = new List<GameObject>();
        playerT = GameObject.FindGameObjectWithTag("Player").transform;
        spawnZ = -tileLength;

        for (int i = 0; i < amountOfTilesOnScreen; i++)
        {
            SpawnTile();
        }
    }


    void Update()
    {
        //Everytime the player crosses a tile it spawns another one
        if(playerT.position.z - safeZone> (spawnZ - amountOfTilesOnScreen * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }

    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject go;
        go = Instantiate(tiles[0]) as GameObject;
        go.transform.SetParent(this.transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go); //add the new tile to a list of the current tiles
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
