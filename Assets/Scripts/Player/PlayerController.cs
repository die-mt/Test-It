using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    [HideInInspector]
    public bool facingRight = true;			// For determining which way the player is currently facing.
    [HideInInspector]
    public bool jump = false;				// Condition for whether the player should jump.
    //[HideInInspector]
    public int lives = 3;

    public float maxSpeed = 10f;

    public float jumpForce = 1000f;			// Amount of force added when the player jumps.

    //private bool escaleras = false;
    private bool secondJump = true; //Verdadero si puede hacer el salto flash. Para evitar que lo haga varias veces seguidas
    private bool flashing = false;  //Verdadero si está haciendo el salto flash
    private bool initTimer = false; 
    private bool getButtonDowmJump = false;
    private bool getButtonUpJump = false;
    private bool flashRoto=false;
    private bool plataformaCambiada = false;
    private bool gamePaused;

    public Sprite iconoVida0;
    public Sprite iconoVida1;
    public Sprite iconoVida2;

    public Sprite iconoEnergia0;
    public Sprite iconoEnergia1; 
    public Sprite iconoEnergia2; 
    public Sprite iconoEnergia3; 
    public Sprite iconoEnergia4;

    public AudioClip pain;
    public AudioClip metalsound;
    public AudioClip brokenblock;
    public AudioClip jumpSound;
    public AudioClip flashSound;


    private float energy;
    private int rompibleCount=0;
    public int contador = 0;

    private Transform groundCheck;			// A position marking where to check if the player is grounded.
    private Transform groundCheck1;
    private bool grounded = false;			// Whether or not the player is grounded.
    private bool aproximacionRompible = false;  //Para detectar cuando se acerca al bloque rompible.
    private Vector3 moveDirection;

    private GameObject Controlador;
    private GameObject paused;

    private Animator animator;


    void Awake()
    {
        // Setting up references.
        groundCheck = transform.Find("groundCheck");
        groundCheck1 = transform.Find("groundCheck1");
        Controlador = GameObject.Find("Controller");
        paused = GameObject.Find("Canvas");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        gamePaused = paused.GetComponent<pauseScript>().gamePaused;
        if (!gamePaused)
        {
            // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
            grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.Linecast(transform.position, groundCheck1.position, 1 << LayerMask.NameToLayer("Ground"));
            aproximacionRompible = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Rompible")) || Physics2D.Linecast(transform.position, groundCheck1.position, 1 << LayerMask.NameToLayer("Rompible"));

            if (Input.GetButtonDown("Jump"))
                getButtonDowmJump = true;
            if (Input.GetButtonUp("Jump"))
                getButtonUpJump = true;
        }
        if (GetComponent<BoxCollider2D>().enabled == false && GetComponent<CircleCollider2D>().enabled == false && contador >= 25)
        {
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<CircleCollider2D>().enabled = true;
            contador = 0;
        }
        else { contador++; }
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
            if (move > 0)
            {
                animator.SetInteger("State", 1);
            }
            else if (move == -1)
            {
                animator.SetInteger("State", 1);
            }

            else if (move == 0)
            {
                animator.SetInteger("State", 0);
            }
        }
        if (grounded || aproximacionRompible)
        {
            if (getButtonDowmJump)
            {
                //anim.SetBool("Ground", grounded);
                getButtonDowmJump = false;
                getButtonUpJump = false;
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
                animator.SetInteger("State", 2);
                AudioSource.PlayClipAtPoint(jumpSound, Camera.main.transform.position);

            }
            if (flashing && !aproximacionRompible) //Cuando alcanza el suelo despues de salto flash
            {
                flashing = false;
                rompibleCount = 0;
            }
            if (!secondJump && !aproximacionRompible)
            {
                secondJump = true;
            }

            if (flashRoto)
            {
                flashRoto = false;
            }
        }
        else
        {
            if (secondJump)
            {
                animator.SetInteger("State", 2);
                
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
                    AudioSource.PlayClipAtPoint(flashSound, Camera.main.transform.position);
                    getButtonDowmJump = false;
                    getButtonUpJump = false;    //Nada que ver con las físicas, sirve para controlar estados.
                    initTimer = false;
                    GetComponent<Rigidbody2D>().isKinematic = false;    //Quita las físicas del motor al objeto.
                    Vector3 ajuste = new Vector3(15, 0, 0);   //Vector ajuste para pruebas.
                    Vector3 currentPosition = transform.position /*- ajuste*/;   //Posicion del objeto.
                    currentPosition.z = 0;
                    if (!flashRoto)
                    {

                        Vector3 moveToward = Camera.main.ScreenToWorldPoint(Input.mousePosition);   //Posicion del raton
                        moveDirection = moveToward - currentPosition; //Vector que va desde la posicion del objeto hasta el raton
                        moveDirection.z = 0;    //Ponemos a cero el z
                        moveDirection.Normalize();  //Normalizamos el vector
                        Vector3 target = moveDirection * 400; //Multiplicamos el modulo por la fuerza que le pondremos
                        if (energy > 1.5f)  //Ponemos limites a la energia
                            energy = 1.5f;
                        if (energy < 0.7f)
                            energy = 0.7f;
                        //print(energy);
                        print("X: " + target.x);
                        print("Y: " + target.y);
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(target.x * energy, target.y * 3 * energy)); //Metemos la juerza en la direccion del 
                        energy = 0f;
                        GameObject.Find("EnergiaTag").GetComponent<UnityEngine.UI.Image>().sprite = iconoEnergia0;
                    }
                    else
                    {
                        Vector3 moveToward = GameObject.Find("Platform (7)").transform.position;   //Posicion del raton
                        moveDirection = moveToward - currentPosition; //Vector que va desde la posicion del objeto hasta el raton
                        moveDirection.z = 0;    //Ponemos a cero el z
                        moveDirection.Normalize();  //Normalizamos el vector
                        Vector3 target = moveDirection * 6000; //Multiplicamos el modulo por la fuerza que le pondremos
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(target.x, target.y));
                        GameObject plataforma = GameObject.Find("Platform (7)");
                        plataforma = GameObject.Find("Cueva");
                        plataformaCambiada = true;
                        Destroy(GameObject.Find("Platform (7)"));
                        Controlador.GetComponent<LoadXmlData>().DeslizaDeidad(900, 900);
                    }
                    secondJump = false;
                }
            }
        }

        if (initTimer)
        {
            energy += Time.deltaTime * 4;
            if (energy>1.5)
                GameObject.Find("EnergiaTag").GetComponent<UnityEngine.UI.Image>().sprite = iconoEnergia4;
            else if (energy > 1.1)
                GameObject.Find("EnergiaTag").GetComponent<UnityEngine.UI.Image>().sprite = iconoEnergia3;
            else if (energy > 0.8)
                GameObject.Find("EnergiaTag").GetComponent<UnityEngine.UI.Image>().sprite = iconoEnergia2;
            else if (energy > 0)
                GameObject.Find("EnergiaTag").GetComponent<UnityEngine.UI.Image>().sprite = iconoEnergia1;
        }

        print(flashRoto);

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
        if (other.CompareTag("Interrogacion") /*|| other.CompareTag("Enemy")*/)  //http://answers.unity3d.com/questions/172975/only-check-collision-of-certain-collider.html
        {
            other.GetComponent<Animator>().SetBool("PlayerIn", true);
            AudioSource.PlayClipAtPoint(metalsound, Camera.main.transform.position);
            if (lives != 0)
            {
                lives--;
                
                ChangeLifeImage();
                Controlador.GetComponent<Controller>().QuitaVida();
                //if (other.CompareTag("Enemy"))
                    //Controlador.GetComponent<LoadXmlData>().Escribe(1, "Enemigo", 5);
            }
            else
            {
                //Destroy(this);
                //UnityEngine.SceneManagement.SceneManager.LoadScene("Test It! Level1");

                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

            }
        }

        if (flashing)
        {
            if (other.CompareTag("Rompible"))
            {
                if (rompibleCount<2)
                {
                    AudioSource.PlayClipAtPoint(brokenblock, Camera.main.transform.position);
                    Destroy(other.gameObject);
                    rompibleCount++;
                    print(rompibleCount);
                }
                else
                {
                    if (aproximacionRompible)
                    {
                        flashing = false;
                        secondJump = true;
                        rompibleCount = 0;
                        //GetComponent<Rigidbody2D>().isKinematic = false;  Puesto para intentar arreglar el problema con los bloques lilas, no ayudaba en nada 
                    }
                }
            }
        }
    }

   public void ChangeLifeImage()
    {
        AudioSource.PlayClipAtPoint(pain, Camera.main.transform.position);
        if (lives == 2)
            GameObject.Find("VidasTag").GetComponent<UnityEngine.UI.Image>().sprite = iconoVida0;
        if (lives == 1)
            GameObject.Find("VidasTag").GetComponent<UnityEngine.UI.Image>().sprite = iconoVida1;
        if (lives == 0)
            GameObject.Find("VidasTag").GetComponent<UnityEngine.UI.Image>().sprite = iconoVida2;
        if (lives == -1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);//"Test It! LevelTutorial");
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("FlashRoto") && !grounded)
        {
            flashRoto = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("FlashRoto") && plataformaCambiada)
        {
            Controlador.GetComponent<LoadXmlData>().Escribe(1, "Bug", 7,7);
            Destroy(other.gameObject);
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
