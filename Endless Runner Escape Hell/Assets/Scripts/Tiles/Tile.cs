using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private MeshRenderer coinMeshRenderer;
    [SerializeField] private Collider obstacleCollider;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnCoinOn()
    {
        coinMeshRenderer.enabled = true;
    }

    public void TurnObstacleOn()
    {
        obstacleCollider.enabled = true;
    }
}
