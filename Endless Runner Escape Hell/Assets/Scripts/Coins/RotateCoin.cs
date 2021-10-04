using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCoin : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 3f;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * rotationSpeed);
    }
}
