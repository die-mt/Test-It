using UnityEngine;
using System.Collections;

public class BasicEnemyBehaviour : MonoBehaviour {


    public float MaxSpeedHorizontal = 5f;
    public float MaxSpeedVertical = 10f;

    private bool facingRight = true;			// For determining which way the player is currently facing.
    private int Direction=-1;
    private GameObject Controlador;


    private Rigidbody2D cuerpo;

    // Use this for initialization
    void Start () {
        Controlador = GameObject.Find("Controller");
        cuerpo = GetComponent<Rigidbody2D>();
    }
	
    void FixedUpdate()
    {
        cuerpo.velocity = new Vector2(Direction * MaxSpeedHorizontal, cuerpo.velocity.y);
        if (cuerpo.velocity.y > MaxSpeedVertical)
            cuerpo.velocity = new Vector2(cuerpo.velocity.x, MaxSpeedVertical);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            if (other.GetComponent<PlayerController>().Flashing)
            {
                Destroy(this.gameObject);
                Controlador.GetComponent<LoadXmlData>().DeslizaDeidad(0, 0);
                Controlador.GetComponent<LoadXmlData>().Escribe(1, "Mexican", 5,3);
            }
            else
            {
                other.GetComponent<Rigidbody2D>().velocity = (new Vector2(0, 50));
                print("salta!");
                Controlador.GetComponent<LoadXmlData>().DeslizaDeidad(500, 500);
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
