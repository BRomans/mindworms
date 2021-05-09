using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogError("A raw weapon was used, how is that even possible?");
        throw new System.Exception("Yeah no, this should have failed at compilation.");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
