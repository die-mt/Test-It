using UnityEngine;
using System.Collections;

public class FlashRoto : MonoBehaviour {

    private GameObject Controlador;
    private GameObject Jugador;

    void Awake()
    {
        Controlador = GameObject.Find("Controller");
        Jugador = GameObject.Find("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && Jugador.GetComponent<PlayerController>.Flashing)
        {

        }
    }
}
