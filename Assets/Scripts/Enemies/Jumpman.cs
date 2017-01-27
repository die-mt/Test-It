using UnityEngine;
using System.Collections;

public class Jumpman : MonoBehaviour
{


    public float MaxSpeedHorizontal = 5f;
    public float MaxSpeedVertical = 10f;
    public Sprite Standingposition;
    public Sprite Jumping;
    public Sprite U2Dead;
    public bool vivo=true;
    public AudioClip splash_dead;

    private bool facingRight = true;			// For determining which way the player is currently facing.
    private int Direction = -1;
    private GameObject Controlador;
    private GameObject Player;
    private bool ataque = false;
    private int contador = 0;


    private Rigidbody2D cuerpo;

    // Use this for initialization
    void Start()
    {
        Controlador = GameObject.Find("Controller");
        Player = GameObject.Find("Player");
        cuerpo = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (vivo)
        {
            cuerpo.velocity = new Vector2(Direction * MaxSpeedHorizontal, cuerpo.velocity.y);
            if (cuerpo.velocity.y > MaxSpeedVertical)
                cuerpo.velocity = new Vector2(cuerpo.velocity.x, MaxSpeedVertical);
            if (cuerpo.velocity.y > 2)
                GetComponent<SpriteRenderer>().sprite = Jumping;
            else
            {
                GetComponent<SpriteRenderer>().sprite = Standingposition;
            }
        }
        if (ataque==true && contador >= 25)
        {
            contador = 0;
            ataque = false;
        }
        else
        {contador++; }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (vivo)
        {
            if (other.tag == "Player")
            {
                if (other.GetComponent<PlayerController>().Flashing)
                {
                    GetComponent<SpriteRenderer>().sprite = U2Dead;
                    Destroy(this.gameObject, 1);
                    AudioSource.PlayClipAtPoint(splash_dead, Camera.main.transform.position);

                    vivo = false;
                }
                else
                {
                    if (ataque==false)
                    {
                        Player.GetComponent<PlayerController>().lives--;
                        Player.GetComponent<PlayerController>().contador=0;
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
            if (other.tag == "Enemy" || other.tag == "Rompible" || other.tag == "Barrier")
            {
                Flip();
            }
            if (other.tag == "ground")
            {
                GetComponent<SpriteRenderer>().sprite = Standingposition;
            }
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
