﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used for disabling the obsticals when the player goes passed them so the enemy isnt stopped by them
public class ObsticalDisabler : MonoBehaviour
{
    public Transform player;
    public GameObject[] obstacals;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        obstacals = GameObject.FindGameObjectsWithTag("Obstacle");
        DisableObstacals();
    }

    //disables all the colliders on all the obstacles after the player has run passed them
    void DisableObstacals()
    {
        if (obstacals != null)
        {
            for (int i = 0; i < obstacals.Length; i++)
            {
                if (player.position.z > obstacals[i].transform.position.z)
                {
                    if(obstacals[i].GetComponent<Collider>().enabled)
                    {
                        obstacals[i].GetComponent<Collider>().enabled = false;
                        //Debug.Log("you ran passed " + obstacals[i].name);
                    }
                }
            }
        }
    }
}
