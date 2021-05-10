using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    void Start() {
        Debug.LogError("A raw weapon was used, how is that even possible?");
        throw new System.Exception("Yeah no, this should have failed at compilation.");

    }

    void Update() {
        if(Input.GetMouseButton(0)) { //right-click
            fireWeapon();
        }
    }

    protected abstract void fireWeapon();
}
