using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public List<GameObject> tiles;
    private Transform playerT;
    private float spawnZ = 0f;
    private float tileLength = 16.653f; // how far forward the next tile needs to be spawnd from the current one
    [SerializeField] private int amountOfTilesOnScreen = 8;

    void Start()
    {
        playerT = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i = 0; i < amountOfTilesOnScreen; i++)
        {
            SpawnTile();
        }
    }


    void Update()
    {

        //Everytime the player crosses a tile it spawns another one
        if(playerT.position.z > (spawnZ - amountOfTilesOnScreen * tileLength))
        {
            SpawnTile();
        }
    }

    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject go;
        go = Instantiate(tiles[0]) as GameObject;
        go.transform.SetParent(this.transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
    }
}
