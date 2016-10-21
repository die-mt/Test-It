using UnityEngine;
using System.Collections;

public class BasicEnemyBehaviour : MonoBehaviour {

    public float MaxSpeedHorizontal = 5f;
    public float MaxSpeedVertical = 10f;

    private bool facingRight = true;			// For determining which way the player is currently facing.
    private int Direction=-1;


    private Rigidbody2D cuerpo;

    // Use this for initialization
    void Start () {
        cuerpo = GetComponent<Rigidbody2D>();
        //Destroy(this.gameObject, 5);
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
            other.GetComponent<Rigidbody2D>().velocity=(new Vector2(0, 100));
            print("salta!");
        }
        if (other.tag == "ground" || other.tag== "Enemy" )
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
