using UnityEngine;
using System.Collections;

public class Temporizador : MonoBehaviour {

    public float targetTime;
    public GameObject Controller;
    public bool Lectura=false;
    private bool fading = false;
    private float Fadetimer;
    private int Tipo;

    void Update()
    {
        if (Lectura)
        { 
            targetTime -= Time.deltaTime;

            if (targetTime <= Fadetimer && !fading && Tipo==1)
            {
                GetComponent<LoadXmlData>().Fade(Fadetimer);
                fading = true;
            }


            if (targetTime <= 0.0f && Tipo == 1)
            {
                timerEnded();
                Lectura = false;
                fading = false;
            }

            if (targetTime<=0.0f && Tipo==2)
            {
                print("Cambio nivel ahora!");
                UnityEngine.SceneManagement.SceneManager.LoadScene("Test It! Level2");//"");
            }

        }
    }

    void timerEnded()
    {
        GetComponent<LoadXmlData>().Borra();
    }

    public void MarcaTiempos(float target, float FadeTime, int tipo)
    {
        targetTime = target;
        Lectura = true;
        Fadetimer = FadeTime;
        Tipo = tipo;

        fading = false;
    }
}
