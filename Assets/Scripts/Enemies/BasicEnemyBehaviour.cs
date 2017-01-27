using UnityEngine;
using System.Collections;

public class BasicEnemyBehaviour : MonoBehaviour {


    public float MaxSpeedHorizontal = 5f;
    public float MaxSpeedVertical = 10f;
    public AudioClip splash_dead;

    private bool facingRight = true;			// For determining which way the player is currently facing.
    private int Direction=-1;
    private GameObject Controlador;
    private GameObject Player;
    private bool ataque = false;
    private int contador = 0;


    private Rigidbody2D cuerpo;

    // Use this for initialization
    void Start () {
        Controlador = GameObject.Find("Controller");
        cuerpo = GetComponent<Rigidbody2D>();
        Player = GameObject.Find("Player");
    }
	
    void FixedUpdate()
    {
        cuerpo.velocity = new Vector2(Direction * MaxSpeedHorizontal, cuerpo.velocity.y);
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
            }
            else
            {
                if (ataque == false)
                {
                    Player.GetComponent<PlayerController>().lives--;
                    Player.GetComponent<PlayerController>().contador = 0;
                    Player.GetComponent<PlayerController>().ChangeLifeImage();
                    Controlador.GetComponent<Controller>().QuitaVida();
                    Player.GetComponent<BoxCollider2D>().enabled = false;
                    Player.GetComponent<CircleCollider2D>().enabled = false;
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
