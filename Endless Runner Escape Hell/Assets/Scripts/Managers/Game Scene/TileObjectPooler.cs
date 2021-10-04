using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObjectPooler : MonoBehaviour
{
    public static TileObjectPooler current;
    public List<GameObject> tilesToPool;
    public int amountOfEachTileToSpawn = 3;
    private int pooledAmount;

    [SerializeField] private List<GameObject> pooledObjects;
    public List<GameObject> blankTiles;

    void Awake()
    {
        current = this;
        SpawnStartTiles();
        GetBlankTiles();
    }

    void SpawnStartTiles()
    {
        pooledObjects = new List<GameObject>();
        pooledAmount = tilesToPool.Count * amountOfEachTileToSpawn; //calculates the amount of tiles to spawn
        //Debug.Log("Pooled Amount = " + pooledAmount);

        //executes once per each tile that needs to be spawned into the pool
        for (int i = 0; i < pooledAmount; i++)
        {
            //adds (amountOfEachTile) to the pool 
            for (int x = 0; x < amountOfEachTileToSpawn; x++)
            {
                if (i < tilesToPool.Count)
                {
                    GameObject obj = Instantiate(tilesToPool[i]);
                    obj.SetActive(false);
                    obj.transform.parent = this.transform;
                    pooledObjects.Add(obj);
                }
            }
        }
    }

    private void GetBlankTiles()
    {
        blankTiles = new List<GameObject>();

        foreach(GameObject go in pooledObjects)
        {
            if(go.name == tilesToPool[0].gameObject.name + "(Clone)")
            {
                blankTiles.Add(go);
            }
        }
    }

    public GameObject GetRandomTile()
    {
        int randomNum = Random.Range(0, pooledObjects.Count);
        while (pooledObjects[randomNum].activeInHierarchy)
        {
            randomNum = Random.Range(0, pooledObjects.Count);
        }

        //found a random tile that is not currently being used
        if (!pooledObjects[randomNum].activeInHierarchy)
        {
            return pooledObjects[randomNum]; //return the random tile that is not currently being used
        }

        return null;
    }
}
