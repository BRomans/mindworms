using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainFart : Weapon {
    void Start() {
        Debug.Log("Brain Fart equiped");
    }
    protected override void fireWeapon() {
        throw new System.NotImplementedException();
    }
}
