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
        //DontDestroyOnLoad(gameObject);
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
    [Range (1, 10)] public float defaultRunSpeed = 3f;
    public float currentSpeed;
    public float sidwaysSpeed = 1.5f;
    [SerializeField] private float gravity = 12f;


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


    void Start()
    {
        animator = this.GetComponentInChildren<Animator>();
        controller = this.GetComponent<CharacterController>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        currentSpeed = defaultRunSpeed;

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
    }

    void Animation()
    {
        //if the jump button is pressed
        if (Input.GetKeyDown(jumpButton1) || Input.GetKeyDown(jumpButton2) || Input.GetKeyDown(jumpButton3))
            animator.SetTrigger("Jump");

        if (Input.GetKeyDown(rollButton1) || Input.GetKeyDown(rollButton2))
            animator.SetTrigger("Roll");
    }

    #region Physics
    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        Vector3 direction = new Vector3(horizontalInput * sidwaysSpeed, 0, currentSpeed);

        if(controller.isGrounded)
        {
            if (Input.GetKeyDown(jumpButton1) || Input.GetKeyDown(jumpButton2) || Input.GetKeyDown(jumpButton3))
                verticalVelocity = jumpHeight;
        }
        

        verticalVelocity -= gravity * Time.deltaTime;

        direction.y = verticalVelocity;

        controller.Move(direction * defaultRunSpeed * Time.deltaTime);
    }
    #endregion

    #region Behaviors
    public void SetSpeed(float modifier)
    {
        currentSpeed = defaultRunSpeed + modifier;
    }

    void Death()
    {
        //Debug.Log("Dead");
        isDead = true;

        this.GetComponent<Score>().OnDeath();
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
    #endregion

    #region Collisions
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Check if you you hit something in front of you
        if (hit.point.z > transform.position.z + controller.radius && hit.gameObject.CompareTag("Obstacle"))
            Death();
    }
    #endregion
}
