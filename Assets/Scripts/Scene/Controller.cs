using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public Text text;
    private int Recogidos;

    // Use this for initialization
    void Start()
    {
        Recogidos = 0;
        text.text = Recogidos.ToString();
    }

    public void SumaColeccionables()
    {
        Recogidos++;
        text.text = Recogidos.ToString();
    }

}
