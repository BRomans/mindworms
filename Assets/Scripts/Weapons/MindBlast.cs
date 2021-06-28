using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindBlast : Weapon {
    public GameObject TargetRectanglePrefab; //TODO: do this dynamically
    private GameObject targetRectangle; //it's not a rectangle, w/e
    private Vector2 targetRectangleOriginalPos;
    
    /*We use a number of stages to the mindblast, they are as follows
     * 0: Not yet aimed / used
     * 1: Focused on a target, now we need to focus on the target (will show moving crosshairs
     * 2: Focus on target strength
     * 3: we shoot
     */
    private int stage = 0;

    private float startRange = 1f;
    private float endRange = 0.1f;
    private float oscilationRange;
    private float oscilationOffset;

    //private float result = oscilationOffset + Mathf.Sin(Time.time) * oscilationRange;

    void Start() {
        Debug.Log("Mind Blast equiped");
        base.brayn = transform.parent.GetComponent<Brayn>(); //TODO: This should be in the base class
        
        oscilationRange = (endRange - startRange) / 2;
        oscilationOffset = oscilationRange + startRange;
    }

    private void Update() {
        base.Update();
        updateTargetRectangle();

        //Debug.Log("beta" + BraynManager.Singleton.udpReceiver.betaSignal);
        Debug.Log("smr" + BraynManager.Singleton.udpReceiver.smrSignal);
    }

    private void updateTargetRectangle() {
        if (stage == 1) {

            targetRectangle.transform.localScale = new Vector3(oscilationOffset + (float)BraynManager.Singleton.udpReceiver.smrSignal * oscilationRange, oscilationOffset + (float)BraynManager.Singleton.udpReceiver.smrSignal * oscilationRange, 1);
        }
    }

    protected override void Aim() {
        if(stage != 0)
            return;
        Debug.Log("Aiming");
        if(!targetRectangle)
            targetRectangle = Instantiate(TargetRectanglePrefab, transform);

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        targetRectangle.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        //targetRectangle.transform = new Vector3(mous)
    }

    protected override void FireWeapon() {
        
        switch(stage) {
            case 0:
                targetRectangleOriginalPos = new Vector2(targetRectangle.transform.position.x, targetRectangle.transform.position.y);
                Debug.LogWarning(targetRectangleOriginalPos);
            Debug.Log("Ima charging my Mind Blast, NOW FOCUS!");
                break;
            case 1:
                Debug.Log("Focused, now try to improve it's strength!");
                break;
            case 2:
                Debug.Log("Fire the mindblast!");
                break;
        }
        stage++;
        if(stage > 2) { throw new System.NotImplementedException(); }
    }
}
