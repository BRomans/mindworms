using UnityEngine;

public class SpawnPoint : MonoBehaviour {
    void Start() {
        GetComponent<Renderer>().enabled = false; //You don't need to see where this during the game. 
    }
}
