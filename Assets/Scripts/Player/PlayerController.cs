using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    [HideInInspector]
    public bool facingRight = true;			// For determining which way the player is currently facing.
    [HideInInspector]
    public bool jump = false;				// Condition for whether the player should jump.
    //private bool escaleras = false;
    private bool doubleJump = true;
    private bool flashing = false;

    public float maxSpeed = 10f;

    public float jumpForce = 1000f;			// Amount of force added when the player jumps.

    private Transform groundCheck;			// A position marking where to check if the player is grounded.
    private bool grounded = false;			// Whether or not the player is grounded.
    private Vector3 moveDirection;
    //private Animator anim;					// Reference to the player's animator component.


    void Awake()
    {
        // Setting up references.
        groundCheck = transform.Find("groundCheck");
        //anim = GetComponent<Animator>();
    }

    void Update()
    {
        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        // If the jump button is pressed and the player is grounded then the player should jump.
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                //anim.SetBool("Ground", grounded);
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
            }
            else
            {
                if (doubleJump)
                {
                    //flashing = true;
                    /*GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    GetComponent<Rigidbody2D>().isKinematic = false;
                        Vector3 currentPosition = transform.position;
                        Vector3 moveToward = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        moveDirection = moveToward - currentPosition;
                        moveDirection.z = 0;
                        moveDirection.Normalize();
                        Vector3 target = moveDirection * 400 + currentPosition;
                    if (Input.GetButtonUp("Jump"))
                    {
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(target.x, target.y*3));
                    }*/
                }


                /*if (doubleJump && !grounded)
                    doubleJump = false;*/
            }
        }
    }


    void FixedUpdate()
    {
        //anim.SetBool("Ground", grounded);
        //anim.SetFloat("vSpeed", rigidbody2D.velocity.y);

        float move = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");

        if (grounded)
        {
            doubleJump = true;
            flashing = false;
        }
        //animation.SetFloat("Speed", Mathf.Abs(move));
        /*if (!escaleras)
            GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        else
            GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, v * maxSpeed);*/
        print(doubleJump);
        if (/*!flashing && */doubleJump)
            GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        else
        {
            if (Input.GetButton("Jump"))
            {
                //GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                GetComponent<Rigidbody2D>().isKinematic = true;
            }
            if (Input.GetButtonUp("Jump"))
            {
                flashing = true;
                GetComponent<Rigidbody2D>().isKinematic = false;
                Vector3 currentPosition = transform.position;
                Vector3 moveToward = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                moveDirection = moveToward - currentPosition;
                moveDirection.z = 0;
                moveDirection.Normalize();
                Vector3 target = moveDirection * 400 + currentPosition;
                GetComponent<Rigidbody2D>().AddForce(new Vector2(target.x, target.y * 3));
            }
            flashing = false;
            doubleJump = false;
        }


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

    /*void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Stairs"))
        {
            escaleras = true;
            GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Stairs"))
        {
            escaleras = false;
            GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }*/
}
