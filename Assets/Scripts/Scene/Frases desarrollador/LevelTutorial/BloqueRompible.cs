﻿using UnityEngine;
using System.Collections;

public class BloqueRompible : MonoBehaviour {

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
            Controller.GetComponent<LoadXmlData>().Escribe(1, "Explicacion0-4", 5, 3);
            done = true;
        }
    }
}
