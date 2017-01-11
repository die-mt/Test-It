using UnityEngine;
using System.Collections;

public class Cena : MonoBehaviour {

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
            Controller.GetComponent<LoadXmlData>().Escribe(1, "Explicacion2-12", 6, 2);
            done = true;
            Controller.GetComponent<Temporizador>().MarcaTiempos(2, 0, 3);
            Destroy(this.GetComponent<SpriteRenderer>(), 0);
        }
    }
}
