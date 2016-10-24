﻿using UnityEngine;
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
            Controlador.GetComponent<LoadXmlData>().Escribe(1,"coleccionable",3);
            Destroy(this.gameObject, 0);

        }
    }
}
