using UnityEngine;
using System.Collections;

public class Armaalcielo : MonoBehaviour {

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
            Controller.GetComponent<LoadXmlData>().Escribe(1, "Explicacion1", 5, 1);
            Controller.GetComponent<LoadXmlData>().DeslizaDeidad(200, 600);
            done = true;
        }
    }
}
