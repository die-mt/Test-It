using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public Text text;
    private int Recogidos;
    private GameObject Controlador;

    // Use this for initialization
    void Start()
    {
        Controlador = GameObject.Find("Controller");
        Recogidos = 0;
        text.text = Recogidos.ToString();
        Controlador.GetComponent<LoadXmlData>().Escribe(1, "introduccion", 10);
    }

    public void SumaColeccionables()
    {
        Recogidos++;
        text.text = Recogidos.ToString();
    }

}
