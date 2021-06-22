using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour {
    private float height; //get Sprite height @ start
    private float width; //get Sprite width @ start
    public float getHeight => height;
    public float getWidth => width;
    //public Sprite terrainSprite;
    //private SpriteRenderer rend;
    //private PolygonCollider2D coll;
    //List<Vector2> physicsShape = new List<Vector2>();
    private List<SpawnPoint> spawnPoints = new List<SpawnPoint>();


    // Start is called before the first frame update
    void Start() {
        //rend = GetComponent<SpriteRenderer>();
        //coll = GetComponent<PolygonCollider2D>();
        //rend.sprite = terrainSprite;
        getSpawnPoints(transform);
    }

    // Update is called once per frame
    void Update() {
        //rend.sprite.GetPhysicsShape(0, physicsShape);
        //coll.SetPath(0, physicsShape);
    }

    public Vector2 getOpenArea() { //TODO: This just returns width and height of the map, as there is nothing in terms of Terrain yet.
        return new Vector2(width, height);
    }

    private void getSpawnPoints(Transform t) {
        foreach(Transform child in t) {
            if(child.CompareTag("Spawn Point")) {
                spawnPoints.Add(child.GetComponent<SpawnPoint>());
            } else {
                getSpawnPoints(child);
            };
        }
        Debug.Log("We found: " + spawnPoints.Count + " spawn points");
    }

    public List<SpawnPoint> SpawnPoints() {
        return spawnPoints;
    }
}
