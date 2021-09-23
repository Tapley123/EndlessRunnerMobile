using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region singleton
    private static PlayerController _instance;

    public static PlayerController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PlayerController>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    [Header ("Refs")]
    private Animator animator;
    private CharacterController controller;
    private GameObject mainCamera;

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
    public float currentSpeed;
    public float sidwaysSpeed = 1.5f;
    [SerializeField] private float gravity = 12f;



    private float verticalVelocity = 0f;
    private Vector3 moveVector;


    

    void Start()
    {
        animator = this.GetComponentInChildren<Animator>();
        controller = this.GetComponent<CharacterController>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        currentSpeed = defaultRunSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //prevents all player movement untill the start animation has finished
        if(Time.time < mainCamera.GetComponent<CameraFollow>().AnimationDuration)
        {
            controller.Move(Vector3.forward * currentSpeed * Time.deltaTime);
            return;
        }

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

        //if the player is not on the ground it applys the gravity force to the player
        if(controller.isGrounded)
            verticalVelocity = -0.5f;
        else
            verticalVelocity -= gravity * Time.deltaTime;


        moveVector.x = Input.GetAxisRaw("Horizontal") * sidwaysSpeed; //move left/right
        moveVector.y = verticalVelocity; 
        moveVector.z = currentSpeed; // move forward

        controller.Move(moveVector * Time.deltaTime * defaultRunSpeed);
    }

    public void SetSpeed(float modifier)
    {
        currentSpeed = defaultRunSpeed + modifier;
    }
}
