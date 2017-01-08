using UnityEngine;
using System.Collections;

public class ColeccionableRecogido : MonoBehaviour {

    private GameObject Controlador;
    private Animator animator;

    void Awake()
    {
        Controlador = GameObject.Find("Controller");
        animator = GameObject.Find("Coleccionables").GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            print("estoy dentro(ColeccionableRecogido)");
            Controlador.GetComponent<Controller>().SumaColeccionables();
            animator.SetBool("Entering", true);
            Controlador.GetComponent<LoadXmlData>().Escribe(1, "coleccionable", 5,5);
            Destroy(this.gameObject, 0);

        }
    }
}
