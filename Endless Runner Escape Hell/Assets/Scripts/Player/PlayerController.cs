using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header ("Class Refs")]
    private Animator animator;
    private CharacterController controller;

    [Header("Controls")]
    //jump buttons
    private KeyCode jumpButton1 = KeyCode.W;
    private KeyCode jumpButton3 = KeyCode.UpArrow;
    private KeyCode jumpButton2 = KeyCode.Space;
    //roll buttons
    private KeyCode rollButton1 = KeyCode.S;
    private KeyCode rollButton2 = KeyCode.DownArrow;

    [Header("Tweaking Variables")]
    [Range (1, 10)][SerializeField] private float defaultRunSpeed = 3f;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float sidwaysSpeed = 1.5f;
    [SerializeField] private float gravity = 12f;



    private float verticalVelocity = 0f;
    private Vector3 moveVector;


    

    void Start()
    {
        animator = this.GetComponentInChildren<Animator>();
        controller = this.GetComponent<CharacterController>();
        currentSpeed = defaultRunSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Animation();
        PlayerMovement();
    }

    void Animation()
    {
        //if the jump button is pressed
        if(Input.GetKeyDown(jumpButton1) || Input.GetKeyDown(jumpButton2) || Input.GetKeyDown(jumpButton3))
            animator.SetTrigger("Jump");

        if (Input.GetKeyDown(rollButton1) || Input.GetKeyDown(rollButton2))
            animator.SetTrigger("Roll");
    }

    void PlayerMovement()
    {
        moveVector = Vector3.zero;

        if(controller.isGrounded)
            verticalVelocity = -0.5f;
        else
            verticalVelocity -= gravity * Time.deltaTime;

        moveVector.x = Input.GetAxisRaw("Horizontal") * sidwaysSpeed;
        moveVector.y = verticalVelocity;
        moveVector.z = currentSpeed;

        controller.Move(moveVector * Time.deltaTime * defaultRunSpeed);
    }
}
