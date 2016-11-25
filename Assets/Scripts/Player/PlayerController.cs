using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    [HideInInspector]
    public bool facingRight = true;			// For determining which way the player is currently facing.
    [HideInInspector]
    public bool jump = false;				// Condition for whether the player should jump.
    //[HideInInspector]
    public int lives = 2;

    public float maxSpeed = 10f;

    public float jumpForce = 1000f;			// Amount of force added when the player jumps.


    //private bool escaleras = false;
    private bool secondJump = true;
    private bool flashing = false; 
    private bool initTimer = false;
    private bool getButtonDowmJump = false;
    private bool getButtonUpJump = false;

    private float energy;

    private Transform groundCheck;			// A position marking where to check if the player is grounded.
    private Transform groundCheck1;
    private bool grounded = false;			// Whether or not the player is grounded.
    private bool aproximacionRompible = false;  //Para detectar cuando se acerca al bloque rompible.
    private Vector3 moveDirection;

    private GameObject Controlador;

    //private Animator anim;					// Reference to the player's animator component.


    void Awake()
    {
        // Setting up references.
        groundCheck = transform.Find("groundCheck");
        groundCheck1 = transform.Find("groundCheck1");
        Controlador = GameObject.Find("Controller");
        //anim = GetComponent<Animator>();
    }

    void Update()
    {
        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(transform.position, groundCheck1.position, 1 << LayerMask.NameToLayer("Ground"));
        aproximacionRompible = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Rompible")) || Physics2D.Linecast(transform.position, groundCheck1.position, 1 << LayerMask.NameToLayer("Rompible"));

        if (Input.GetButtonDown("Jump"))
            getButtonDowmJump = true;
        if (Input.GetButtonUp("Jump"))
            getButtonUpJump = true;
    }


    void FixedUpdate()
    {
        //anim.SetBool("Ground", grounded);
        //anim.SetFloat("vSpeed", rigidbody2D.velocity.y);

        float move = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");

        
        //animation.SetFloat("Speed", Mathf.Abs(move));
        /*if (!escaleras)
            GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        else
            GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, v * maxSpeed);*/
        if (!flashing)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }



        if (grounded || aproximacionRompible)
        {
            if (getButtonDowmJump)
            {
                //anim.SetBool("Ground", grounded);
                getButtonDowmJump = false;
                getButtonUpJump = false;
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
            }
            if (flashing && !aproximacionRompible)
                flashing = false;
            if (!secondJump && !aproximacionRompible)
                secondJump = true;
        }
        else
        {
            if (secondJump)
            {
                if (getButtonDowmJump)
                {
                    getButtonDowmJump = false;
                    getButtonUpJump = false;
                    initTimer = true;
                    GetComponent<Rigidbody2D>().isKinematic = true;
                    flashing = true;
                }

                if (getButtonUpJump && flashing)
                {
                    getButtonDowmJump = false;
                    getButtonUpJump = false;
                    initTimer = false;
                    GetComponent<Rigidbody2D>().isKinematic = false;
                    Vector3 currentPosition = transform.position;
                    Vector3 moveToward = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    moveDirection = moveToward - currentPosition;
                    moveDirection.z = 0;
                    moveDirection.Normalize();
                    Vector3 target = moveDirection * 400 + currentPosition;
                    if (energy > 1.5f)
                        energy = 1.5f;
                    if (energy < 0.7f)
                        energy = 0.7f;
                    //print(energy);
                    print(target.x);
                    print(target.y);
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(target.x * energy, target.y * 3 *energy));
                    energy = 0f;
                    secondJump = false;
                }
            }
        }

        if (initTimer)
            energy += Time.deltaTime;



        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();
    }


    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interrogacion"))
        {
            if (lives != 0)
            {
                Controlador.GetComponent<LoadXmlData>().Escribe(1,"CajaFalsa", 3);
                lives--;
            }
            else
            {
                Destroy(this);
                Controlador.GetComponent<LoadXmlData>().Escribe(1,"Snake", 5);
            }
        }
        if (flashing)
        {
            if (other.CompareTag("Rompible"))
            {
                print("eeeeyyycuboo");
                Destroy(other);
            }
        }
    }

    public bool Flashing { get { return flashing; } }

    /*void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Stairs"))
        {
            escaleras = false;
            GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }*/
}
