using UnityEngine;
using System.Collections;

public class FlyingEnemyBehaviour : MonoBehaviour
{


    public float MaxSpeedHorizontal = 5f;
    public float MaxSpeedVertical = 10f;

    private bool facingRight = true;			// For determining which way the player is currently facing.
    private Vector2 Direction;
    private GameObject Controlador;
    private GameObject player;
    private float MaxRange = 300f;
    private Animator anim;


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
            anim.SetBool("encontrado", true);
        }
        else
        {
            cuerpo.velocity = new Vector2(0, 0);
            anim.SetBool("encontrado", false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerController>().Flashing)
            {
                Destroy(this.gameObject);
            }
            else
            {
                other.GetComponent<Rigidbody2D>().velocity = (new Vector2(0, 50));
                print("salta!");
                Controlador.GetComponent<LoadXmlData>().Escribe(1, "Enemigo", 5,2);
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

