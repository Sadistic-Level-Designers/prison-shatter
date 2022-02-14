using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TriggerBehaviour))]
public class TriggerEffect : DependentTrigger
{
    
    [Header("Enable when OFF")]
    public List<Behaviour> componentsWhenOff;
    public List<GameObject> objectsWhenOff;

    [Header("Enable when ON")]
    public List<Behaviour> componentsWhenOn;
    public List<GameObject> objectsWhenOn;

    void OnEnable() {
        trigger = GetComponent<TriggerBehaviour>();
    }

    void Awake() {
        if(this.isActive) {
            OnTrigger();
        } else {
            OnReset();
        }
    }

    public override void OnTrigger() {
        foreach(Behaviour b in componentsWhenOff) {
            if(b != null) b.enabled = false;
        }
        foreach(GameObject o in objectsWhenOff) {
            if(o != null) o.SetActive(false);
        }

        foreach(Behaviour b in componentsWhenOn) {
            if(b != null) b.enabled = true;
        }
        foreach(GameObject o in objectsWhenOn) {
            if(o != null) o.SetActive(true);
        }
    }

    public override void OnReset() {
        foreach(Behaviour b in componentsWhenOn) {
            if(b != null) b.enabled = false;
        }
        foreach(GameObject o in objectsWhenOn) {
            if(o != null) o.SetActive(false);
        }

        foreach(Behaviour b in componentsWhenOff) {
            if(b != null) b.enabled = true;
        }
        foreach(GameObject o in objectsWhenOff) {
            if(o != null) o.SetActive(true);
        }
    }
}
