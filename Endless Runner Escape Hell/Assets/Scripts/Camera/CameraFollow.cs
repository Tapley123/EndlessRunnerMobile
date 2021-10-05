using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header ("Class Refs")]
    [SerializeField] private Transform lookAt;

    [Header ("Vectors")]
    private Vector3 startOffset;
    private Vector3 moveVector;

    [Header("Start Animation")]
    public float AnimationDuration = 2f;
    [SerializeField] private Vector3 animationOffset = new Vector3(0, 5, 7);
    private float transition = 0.0f;
    

    [Header("Tweaks")]
    [SerializeField] private float minCameraHeight = 2f;
    [SerializeField] private float maxCameraHeight = 5f;

    void Start()
    {
        //lookAt = GameObject.FindGameObjectWithTag("Player").transform;
        startOffset = transform.position - lookAt.position;
    }

    
    void Update()
    {
        FollowCam();
    }

    void FollowCam()
    {
        moveVector = lookAt.position + startOffset;
        moveVector.x = 0; //stops the camera moveing left and right
        moveVector.y = Mathf.Clamp(moveVector.y, minCameraHeight, maxCameraHeight); //restricts the height the camera can go between the min and max values

        if(transition > 1f)
            transform.position = moveVector;
        else
            StartAnimation();
    }

    void StartAnimation()
    {
        transform.position = Vector3.Lerp(moveVector + animationOffset, moveVector, transition);
        transition += Time.deltaTime * 1 / AnimationDuration;
        transform.LookAt(lookAt.position + Vector3.up);
    }
}
