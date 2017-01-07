using UnityEngine;
using System.Collections;

public class Temporizador : MonoBehaviour {

    public float targetTime;
    public GameObject Controller;
    public bool Lectura=false;
    private bool fading = false;
    private float Fadetimer;

    void Update()
    {
        if (Lectura)
        { 
            targetTime -= Time.deltaTime;

            if (targetTime <= Fadetimer && !fading)
            {
                GetComponent<LoadXmlData>().Fade(Fadetimer);
                fading = true;
            }


            if (targetTime <= 0.0f)
            {
                timerEnded();
                Lectura = false;
                fading = false;
            }

        }
    }

    void timerEnded()
    {
        GetComponent<LoadXmlData>().Borra();
    }

    public void MarcaTiempos(float target, float FadeTime)
    {
        targetTime = target;
        Lectura = true;
        Fadetimer = FadeTime;

        fading = false;
    }
}
