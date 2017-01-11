using UnityEngine;
using System.Collections;

public class Animacion : MonoBehaviour {

    private GameObject Controller;
    private bool done = false;

    void Awake()
    {
        Controller = GameObject.Find("Controller");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!done && other.tag == "Player")
        {
            Controller.GetComponent<LoadXmlData>().Escribe(1, "Explicacion4", 5, 1);
            Controller.GetComponent<LoadXmlData>().DeslizaDeidad(600,800);
            done = true;
        }
    }
}
