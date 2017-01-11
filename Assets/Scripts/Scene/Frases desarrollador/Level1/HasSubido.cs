using UnityEngine;
using System.Collections;

public class HasSubido : MonoBehaviour {

    private GameObject Controller;
    private bool done = false;

    void Awake()
    {
        Controller = GameObject.Find("Controller");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
         if (!done && other.tag=="Player")
          {
            Controller.GetComponent<LoadXmlData>().Escribe(1, "Mexican", 5,3);
            done = true;
            print("movidop");
          }
      }
}
