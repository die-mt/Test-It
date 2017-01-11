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
            Controlador.GetComponent<Controller>().SumaColeccionables();
            animator.SetBool("Entering", true);
            Destroy(this.gameObject, 0);

        }
    }
}
