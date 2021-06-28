using System;
using UnityEngine;

[RequireComponent(typeof(BraynManager))]
public class LevelManager : MonoBehaviour {
    [Header("Terrain")]
    public bool randomTerrain = false;
    public GameObject terrain;
    
    private BraynManager braynManager;
    
    protected virtual void Start() {
        braynManager = GetComponent<BraynManager>();
        
        verifyCamera();
        verifyTerrain();
    }

    private void verifyCamera() {
        Camera cam = braynManager.MainCamera;
        if(!cam) cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Debug.Assert(cam != null, "there is no camera which we can identify as being the main Camera");
        if(cam.GetComponentInChildren<CameraController>() == null) {
            Debug.LogWarning("The main camera doesn't have the CameraController script!");
            cam.gameObject.AddComponent<CameraController>();
            Debug.LogWarning("Added CameraController to Main Camera as it seems it was forgotten!");
        }
    }

    private void verifyTerrain() { //TODO: Rewrite logic here, can be a lot better.
        GameObject[] listOfTerrains = GameObject.FindGameObjectsWithTag("Terrain");
        if(randomTerrain) {
            Debug.Log("We are grabbing a random terrain");
            foreach (GameObject go in listOfTerrains) Destroy(go);
            
            throw new NotImplementedException("Grab a random terrain from the Terrain Folder, " +
                "check if it has a proper TerrainController, " +
                "if not, grab another one, else continue," +
                "set the object as the terrain reference, " +
                "instantiate it");

            return;
        }
        if(listOfTerrains.Length == 0) {
            Debug.Log("No terrain was found in the current scene whatsoever, going to try and see if there is a terrain referered to this Level");
            instantiateTerrainFromReference();
        } else if (listOfTerrains.Length == 1) {
            //TODO: Verify it has a proper TerrainController
            bool properTC = true;
            if(properTC) { 
            listOfTerrains[0].transform.parent = transform;
                if(terrain && (listOfTerrains[0] != terrain))
                    Debug.LogWarning("The terrain we are using is NOT the one that was explicitly referenced. You will want to look into this");
                terrain = listOfTerrains[0];
            } else {
                Destroy(listOfTerrains[0]);
                instantiateTerrainFromReference();
            }
        } else {
            throw new NotImplementedException("We got too many terrains, handle it");
        }
    }

    private void instantiateTerrainFromReference() {
        if(terrain != null) {
            Debug.Log("Found terrain referenced!");
            Instantiate(terrain, transform);
        } else {
            throw new Exception("No terrain found that we can instantiate");
        }
    }

    protected virtual void Update() {}
}
