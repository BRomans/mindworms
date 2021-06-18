using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainFart : Weapon {
    void Start() {
        Debug.Log("Brain Fart equiped");
    }
    protected override void FireWeapon() {
        throw new System.NotImplementedException();
    }

    protected override void Aim() {
        throw new System.NotImplementedException();
    }
}
