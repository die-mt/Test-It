using UnityEngine;
using System.Collections;

public class FlyingEnemyBehaviour : MonoBehaviour
{


    public float MaxSpeedHorizontal = 5f;
    public float MaxSpeedVertical = 10f;
    public bool NubeEspecial=false;
    public AudioClip splash_dead;

    private bool facingRight = true;			// For determining which way the player is currently facing.
    private Vector2 Direction;
    private GameObject Controlador;
    private GameObject player;
    private float MaxRange = 300f;
    private Animator anim;
    private bool ataque = false;
    private int contador = 0;


    private Rigidbody2D cuerpo;

    // Use this for initialization
    void Start()
    {
        Controlador = GameObject.Find("Controller");
        cuerpo = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Direction = player.transform.position-cuerpo.transform.position;
        if (Direction.sqrMagnitude<MaxRange)
        {
            Direction.Normalize();
            cuerpo.velocity = new Vector2(Direction.x * MaxSpeedHorizontal, Direction.y * MaxSpeedHorizontal + Mathf.Sin(Time.time*5)*5);
        }
        else
        {
            cuerpo.velocity = new Vector2(0, 0);
        }
        if (Direction.x > 0 && !facingRight)
            Flip();
        else if (Direction.x < 0 && facingRight)
            Flip();

        if (ataque == true && contador >= 25)
        {
            contador = 0;
            ataque = false;
        }
        else
        { contador++; }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerController>().Flashing)
            {
                AudioSource.PlayClipAtPoint(splash_dead, Camera.main.transform.position);
                Destroy(this.gameObject);
                if (NubeEspecial)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Test It! Level1Rizo");//"Test It! LevelTutorial");
                }
            }
            else
            {
                if (ataque == false)
                {

                    player.GetComponent<PlayerController>().lives--;
                    player.GetComponent<PlayerController>().contador = 0;
                    player.GetComponent<PlayerController>().ChangeLifeImage();
                    Controlador.GetComponent<Controller>().QuitaVida();
                    player.GetComponent<BoxCollider2D>().enabled = false;
                    player.GetComponent<CircleCollider2D>().enabled = false;
                    other.GetComponent<Rigidbody2D>().velocity = (new Vector2(25, 15));
                    ataque = true;
                    contador = 0;
                }
            }
        }
        if (other.tag == "Enemy" || other.tag == "Rompible")
        {
            Flip();
        }

    }

    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        Direction *= -1;
    }
}

