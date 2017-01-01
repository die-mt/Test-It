﻿using UnityEngine;
using System.Collections;

public class DeadLine : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
            //UnityEngine.SceneManagement.SceneManager.LoadScene("Test It! Level1");
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);//"Test It! LevelTutorial");
        Destroy(other);
    }
}
