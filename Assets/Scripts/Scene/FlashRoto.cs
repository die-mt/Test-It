using UnityEngine;
using System.Collections;

public class FlashRoto : MonoBehaviour {

    private PlayerController Jugador;
    private GameObject CuerpoJ;
    private GameObject Plataforma;
    private bool done=false;

    void Awake()
    {
        CuerpoJ= GameObject.Find("Player");
        Plataforma = GameObject.Find("Platform (7)");

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!done)
        {
            if (other.tag == "Player" && CuerpoJ.GetComponent<PlayerController>().Flashing)// El jugador intenta hacer un salto flash dentro de la zona
            {
                Plataforma.transform.GetChild(0).Translate(0, 1000, 0);//No me deja destruirlo(o no lo se hacer bien) por lo que simplemente lo muevo al quinto carajo
                CuerpoJ.GetComponent<Rigidbody2D>().velocity = new Vector2(50, -12);
                Plataforma = GameObject.Find("Cueva");
                Plataforma.GetComponent<SpriteRenderer>().sortingLayerName = "Props";
                done = true;
            }
        }
    }
}
