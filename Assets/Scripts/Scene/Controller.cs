using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public Text Colecc;
   // public Text VidHud;

    private int Recogidos;
    private GameObject Controlador;
    private GameObject Jugador;
    private int vidas;

    // Use this for initialization
    void Start()
    {
        Controlador = GameObject.Find("Controller");
        Recogidos = 0;
        Jugador= GameObject.Find("Player");
        vidas = Jugador.GetComponent<PlayerController>().lives;
        Colecc.text = Recogidos.ToString();
        //VidHud.text = vidas.ToString();
        Controlador.GetComponent<LoadXmlData>().Escribe(1, "introduccion", 5,5);
    }

    public void SumaColeccionables()
    {
        Recogidos++;
        Colecc.text = Recogidos.ToString();
    }

    public void QuitaVida()
    {
        //vidas--;
        //VidHud.text = vidas.ToString();
    }

}
