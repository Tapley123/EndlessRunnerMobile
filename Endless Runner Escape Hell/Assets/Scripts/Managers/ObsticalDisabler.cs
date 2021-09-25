using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticalDisabler : MonoBehaviour
{
    public Transform player;
    public GameObject[] obstacals;
    public GameObject[] JumpingObstacals;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        obstacals = GameObject.FindGameObjectsWithTag("Obstacle");
        JumpingObstacals = GameObject.FindGameObjectsWithTag("Jump Obstacle");

        if(obstacals != null)
        {
            for (int i = 0; i < obstacals.Length; i++)
            {
                if(player.position.z > obstacals[i].transform.position.z)
                {
                    obstacals[i].GetComponent<Collider>().enabled = false;
                    Debug.Log("you ran passed " + obstacals[i].name);
                }
            }
        }

        if (JumpingObstacals != null)
        {
            for (int i = 0; i < JumpingObstacals.Length; i++)
            {
                if (player.position.z > JumpingObstacals[i].transform.position.z)
                {
                    JumpingObstacals[i].GetComponent<Collider>().enabled = false;
                    Debug.Log("you ran passed " + JumpingObstacals[i].name);
                }
            }
        }
    }
}
