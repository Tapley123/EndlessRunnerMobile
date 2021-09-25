using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Refs")]
    private Animator animator;
    private CharacterController controller;
    private GameObject mainCamera;

    //general 
    private float sidwaysSpeed = 1.5f;
    private float gravity = 12f;
    [SerializeField] private float speed = 3f;
    private float playerSpeed;
    private float distanceFromPlayer;

    [Header("Tweaking")]
    [SerializeField] private float rotateToPlayerSpeed = 3f;

    private float verticalVelocity = 0f;
    private Vector3 moveVector;
    private bool caughtPlayer = false;
    private float startTime;


    void Start()
    {
        animator = this.GetComponentInChildren<Animator>();
        controller = this.GetComponent<CharacterController>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        startTime = Time.time;
    }

    void Update()
    {
        if (caughtPlayer)
            return;

        //prevents all player movement untill the start animation has finished
        if (Time.time - startTime < mainCamera.GetComponent<CameraFollow>().AnimationDuration)
        {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }

        distanceFromPlayer = Vector3.Distance(this.transform.position, PlayerController.Instance.transform.position); //checks the distance the enemy is from the player

        EnemyMovement();
    }

    #region Physics
    void EnemyMovement()
    {
        playerSpeed = PlayerController.Instance.currentSpeed;
        speed = playerSpeed;
        sidwaysSpeed = PlayerController.Instance.sidwaysSpeed * 0.2f;

        //face the player
        Vector3 direction = PlayerController.Instance.transform.position - this.transform.position; //get the Vector in the direction of the player
        Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction); //get a quaternion of the rotation needed
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotateToPlayerSpeed * Time.deltaTime); //smoothly look at the player


        //if the enemy is not on the ground it applys the gravity force to the player
        if (controller.isGrounded)
            verticalVelocity = -0.5f;
        else
            verticalVelocity -= gravity * Time.deltaTime;


        //if facing left move left   /   else move right
        if (this.transform.rotation.y < 0)
            moveVector = new Vector3(-sidwaysSpeed, verticalVelocity, speed);
        else
            moveVector = new Vector3(sidwaysSpeed, verticalVelocity, speed);

        controller.Move(moveVector * PlayerController.Instance.defaultRunSpeed * Time.deltaTime);
    }
    #endregion

    #region Behaviors
    //called when the enemy touches the player
    void CaughtPlayer()
    {
        //Debug.Log("Caught The Player");
        caughtPlayer = true;
    }
    #endregion

    #region Collisions
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.CompareTag("Player"))
            CaughtPlayer();
    }
    #endregion
}
