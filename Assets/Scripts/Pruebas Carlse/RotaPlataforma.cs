using UnityEngine;
using System.Collections;

public class RotaPlataforma : MonoBehaviour {

    // Use this for initialization
    Transform plataforma;
    public float girox=0;
    public float giroy=0;
    public float giroz=0;
    void Start () {
        plataforma = this.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        plataforma.Rotate(plataforma.rotation.x+girox, plataforma.rotation.y + giroy, plataforma.rotation.z + giroz);//Cada frame rotara n grados.Si tocas la x e y pasan cosas divertidas
	}
}
