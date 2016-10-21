using UnityEngine;
using System.Collections;

public class ColeccionableRecogido : MonoBehaviour {

    private GameObject Controlador;

    void Awake()
    {
        Controlador = GameObject.Find("Controller");
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            print("estoy dentro(ColeccionableRecogido)");
            Controlador.GetComponent<Controller>().SumaColeccionables();
            Destroy(this.gameObject, 0);
        }
    }
}
