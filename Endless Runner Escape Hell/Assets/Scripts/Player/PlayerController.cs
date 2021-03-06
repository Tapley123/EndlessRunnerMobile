using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Score), typeof(CollectCoins))]
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
    #endregion

    
    [SerializeField] private enCharacter character;
    [SerializeField] private List<GameObject> characters;
    private GameObject myCharacter;

    [Header ("Refs")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private GameObject mainCamera;
    private Animator animator;

    [Header("Controls")]
    //jump buttons
    private KeyCode jumpButton1 = KeyCode.W;
    private KeyCode jumpButton3 = KeyCode.UpArrow;
    private KeyCode jumpButton2 = KeyCode.Space;
    //roll buttons
    private KeyCode rollButton1 = KeyCode.S;
    private KeyCode rollButton2 = KeyCode.DownArrow;

    [Header("Tweaking Variables")]
    [Range (1, 10)] public float defaultRunSpeed = 3f;
    public float currentSpeed;
    public float sidwaysSpeed = 1.5f;
    public float SidwaysBoost = 5f;
    [SerializeField] private bool canSlideLeftAndRight = false;
    [SerializeField] private float gravity = 12f;
    private float startGravity;


    private float verticalVelocity = 0f;
    private Vector3 moveVector;
    public bool isDead = false;
    private float startTime;

    [Header("Rolling")]
    [SerializeField] private bool rolling = false;
    private float startColliderHeight;
    private float halfColliderHeight;
    private float startColliderPivotHeight;
    private float halfColliderPivotHeight;

    [Header("Jumping")]
    public float jumpHeight = 2f;
    [SerializeField] private bool jumping = false;

    [Header("Kick")]
    [SerializeField] private bool useKicking = true;
    [Range (0, 1)][SerializeField] private float kickGravityNormalised = 0.5f;
    [Range (0, 1)][SerializeField] private float kickJumpNormalised = 0.8f;
    [Range (0, 1)][SerializeField] private float kickDelay = 0.25f;

    [Header("Speed Increse")]
    private float newSpeedGoal; //the next difficulty levels speed
    private bool increasingSpeed = false; //only true when the player is at the next difficulty level and their speed needs to increase
    private float speedAcceleration = 1f; //speed in which the player accelerates when they hit a new difficulty level

    void Awake()
    {
        SpawnCharacter();
    }

    void Start()
    {
        animator = this.GetComponentInChildren<Animator>();
        //controller = this.GetComponent<CharacterController>();
        //mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        currentSpeed = defaultRunSpeed;
        startGravity = gravity;

        //rolling
        startColliderHeight = controller.height;
        halfColliderHeight = controller.height / 2; //half the size of the collider at the start
        startColliderPivotHeight = controller.center.y;
        halfColliderPivotHeight = controller.center.y / 2; //half the y offset of the collider at the start

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
            return;

        //prevents all player movement untill the start animation has finished
        if(Time.time - startTime < mainCamera.GetComponent<CameraFollow>().AnimationDuration)
        {
            controller.Move(Vector3.forward * currentSpeed * Time.deltaTime);
            return;
        }

        Animation();
        PlayerMovement();
        RollingBehaviors();
        JumpingBehaviors();
        SmoothlyIncreaseSpeed();

        if (useKicking)
            KickingBehaviors();
    }

    void Animation()
    {
        //if the jump button is pressed
        if (Input.GetKeyDown(jumpButton1) || Input.GetKeyDown(jumpButton2) || Input.GetKeyDown(jumpButton3) || SwipeManager.swipeUp)
            animator.SetTrigger("Jump");

        if (Input.GetKeyDown(rollButton1) || Input.GetKeyDown(rollButton2) || SwipeManager.swipeDown)
            animator.SetTrigger("Roll");
    }

    #region Physics
    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal") + Input.acceleration.x; //horzontal movement is controlled by both tilting a phone or using keyboard controls

        if(canSlideLeftAndRight)
        {
            if (SwipeManager.swipeLeft)
            {
                horizontalInput = SidwaysBoost * -1;
            }
            if (SwipeManager.swipeRight)
            {
                horizontalInput = SidwaysBoost;
            }
        }
        
        //Debug.Log("Horizontal Input = " + horizontalInput);

        Vector3 direction = new Vector3(horizontalInput * sidwaysSpeed, 0, currentSpeed);

        if(controller.isGrounded)
        {
            if (Input.GetKeyDown(jumpButton1) || Input.GetKeyDown(jumpButton2) || Input.GetKeyDown(jumpButton3) || SwipeManager.swipeUp)
                verticalVelocity = jumpHeight;
        }
        

        verticalVelocity -= gravity * Time.deltaTime;

        direction.y = verticalVelocity;

        controller.Move(direction * defaultRunSpeed * Time.deltaTime);
    }
    #endregion

    #region Behaviors
    private void SpawnCharacter()
    {
        //Debug.Log("Change here to not be given the default character in the level");
        character = DefaultLevelCharacter.Instance.defaultLevelCharacter;

        if (character == enCharacter.zombie)
        {
            myCharacter = Instantiate (characters[0]);
            myCharacter.transform.parent = this.transform;
        }

        if(character == enCharacter.pearl)
        {
            myCharacter = Instantiate(characters[1]);
            myCharacter.transform.parent = this.transform;
        }

        if (character == enCharacter.japonese)
        {
            myCharacter = Instantiate(characters[2]);
            myCharacter.transform.parent = this.transform;
        }
    }

    public void NextSpeedLevel(float modifier)
    {
        newSpeedGoal = defaultRunSpeed + modifier;
        increasingSpeed = true;
    }

    private void SmoothlyIncreaseSpeed()
    {
        if (increasingSpeed && currentSpeed < newSpeedGoal)
        {
            currentSpeed += speedAcceleration * Time.deltaTime;
        }

        if (currentSpeed >= newSpeedGoal)
            increasingSpeed = false;
    }

    void Death()
    {
        //Debug.Log("Dead");
        isDead = true;

        this.GetComponent<Score>().OnDeath();
        this.GetComponent<CollectCoins>().OnDeath();
        this.GetComponent<PlayerUI>().ActivateAdButtons();
    }

    void RollingBehaviors()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
            rolling = true;
        else
            rolling = false;


        //makes the hit collider shorter when the player is rolling 
        if (rolling)
        {
            controller.height = halfColliderHeight;
            controller.center = new Vector3(controller.center.x, halfColliderPivotHeight, controller.center.z);
        }
        else
        {
            controller.height = startColliderHeight;
            controller.center = new Vector3(controller.center.x, startColliderPivotHeight, controller.center.z);
        }
    }

    void JumpingBehaviors()
    {
        //checks if the player is jumping
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            jumping = true;
        else
            jumping = false;
    }

    void KickingBehaviors()
    {
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(KickCoroutine(kickDelay));
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Kick"))
            gravity = gravity * kickJumpNormalised;
        else
            gravity = startGravity;
    }
    private IEnumerator KickCoroutine(float time)
    {
        yield return new WaitForSeconds(time);

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Running") && controller.isGrounded)
        {
            animator.SetTrigger("Kick");
            verticalVelocity = jumpHeight * kickJumpNormalised;
        }
    }
    #endregion

    #region Collisions
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Check if you you hit something in front of you
        if (hit.point.z > transform.position.z + controller.radius && hit.gameObject.CompareTag("Obstacle"))
            Death();

        if(hit.gameObject.CompareTag("Obstacle"))
        {
            //Debug.Log("You Have Been Hit");
        }
    }
    #endregion
}
