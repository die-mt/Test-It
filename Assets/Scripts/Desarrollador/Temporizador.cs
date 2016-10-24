using UnityEngine;
using System.Collections;

public class Temporizador : MonoBehaviour {

    public float targetTime;
    public GameObject Controller;
    public bool Lectura=false;

    void Update()
    {
        if (Lectura)
        { 
            targetTime -= Time.deltaTime;

            if (targetTime <= 0.0f)
            {
                timerEnded();
                Lectura = false;
            }

        }
    }

    void timerEnded()
    {
        GetComponent<LoadXmlData>().Borra();
    }

    public void MarcaTiempos(float target)
    {
        targetTime = target;
        Lectura = true;

    }
}
