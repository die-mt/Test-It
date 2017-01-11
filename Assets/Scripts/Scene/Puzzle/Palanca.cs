using UnityEngine;
using System.Collections;

public class Palanca : MonoBehaviour {

    GameObject Controlador;
    bool mensaje;


    void Start () {
        Controlador = GameObject.Find("Controller");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        print("Anoche cuando dormia");
        if (other.tag != "Player")
        {
            if (this.gameObject.GetComponent<SpriteRenderer>().flipY == false)
            {
                this.gameObject.GetComponent<SpriteRenderer>().flipY = true;
                mensaje = true;
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().flipY = false;
                mensaje = false;
            }
            print("Micarromelorobaron");
            Controlador.GetComponent<Puzzle>().Estado(int.Parse(this.name),mensaje);
            Controlador.GetComponent<Puzzle>().Resuelto();
        }

    }
}
