using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtTrigger : CollisionTrigger
{
    public override void OnTrigger() {
        PlayerScript.isHurt = true;
    }
}
