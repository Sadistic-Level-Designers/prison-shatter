using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBehaviour : MonoBehaviour
{
    [Header("Trigger")]
    public bool isActive = false;
    private bool _wasActive = false;

    void Update() {
        if(isActive != _wasActive) {
            if(isActive) {
                OnTrigger();
            } else {
                OnReset();
            }

            _wasActive = isActive;
        }
    }

    public virtual void OnTrigger() {
        Debug.Log(gameObject.name + " trigger has been activated");    
    }

    public virtual void OnReset() {
        Debug.Log(gameObject.name + " trigger has been reset");
    }
}
