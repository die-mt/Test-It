using UnityEngine;
using System.Collections;

public class Puzzle : MonoBehaviour {

    bool[] Palancas= new bool [15];
    private GameObject Controlador;


    void Start()
    {
        Controlador = GameObject.Find("Controller");
        for (int i = 0; i < 15; i++)
        {
            Palancas[i] = false;
        }
    }
	public void Resuelto()
    {
        print("nope");
        if (Palancas[0]==true && Palancas[1] == true && Palancas[2] == true && Palancas[3] == true)
        {
           Controlador.GetComponent<LoadXmlData>().Escribe(1, "Puzzle", 5,4);
            print("yassss");
        }
    }

    public void Estado(int posicion, bool estado)
    {
        Palancas[posicion] = estado;
        print("ta cambiado");
    }
}
