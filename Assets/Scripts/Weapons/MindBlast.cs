using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindBlast : Weapon {
    void Start() {
        Debug.Log("Mind Blast equiped");
    }

    protected override void fireWeapon() {
        throw new System.NotImplementedException();
    }
}
