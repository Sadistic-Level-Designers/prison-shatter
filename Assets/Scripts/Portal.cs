using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public SceneReference targetScene;

    void OnTriggerEnter2D(Collider2D c) {
        if(!gameObject.activeSelf) return;
        
        PlayerScript player;
        if(c.gameObject.TryGetComponent<PlayerScript>(out player)) {
            SceneManager.LoadScene( targetScene );
        }
    }
}