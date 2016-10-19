using UnityEngine;
using System.Collections;

public class ColeccionableRecogido : MonoBehaviour {

    public GameObject Controlador;

    void OnTriggerStay2D(Collider2D other)
    {
        print("estoy dentro(ColeccionableRecogido)");
        Controlador.GetComponent<Controller>().SumaColeccionables();
        Destroy(this.gameObject, 0);
    }
}
