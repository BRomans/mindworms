using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    protected Brayn brayn;
    protected void Start() {
        Debug.LogError("A raw weapon was used, how is that even possible?");
        throw new System.Exception("Yeah no, this should have failed at compilation.");

    }

    protected void Update() {
        if(BraynManager.Singleton.CurrentBrayn != brayn.BraynId)
            return;

        if(Input.GetMouseButton(0)) { //left-click
            Aim();
        } else if(Input.GetMouseButtonUp(0)) {
            FireWeapon();
        }
    }

    protected abstract void Aim();
    protected abstract void FireWeapon();
}
