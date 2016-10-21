using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public Transform Enemigo;
    public Transform PosSpawn;

    private float tiempoEntreSpawn = 2;
    private float tiempoUltimoSpawn = 0;


	// Update is called once per frame
	void Update () {
	    if (Time.time -tiempoUltimoSpawn > tiempoEntreSpawn)
        {
            Transform enemy = (Transform)Instantiate(Enemigo, PosSpawn.position, transform.rotation);
            Destroy(enemy.gameObject, 4);
            tiempoUltimoSpawn = Time.time;
        }
	}
}
