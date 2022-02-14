using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DependentTrigger : TriggerBehaviour
{
    public TriggerBehaviour trigger;

    void LateUpdate() {
        isActive = trigger.isActive;
    }
}
