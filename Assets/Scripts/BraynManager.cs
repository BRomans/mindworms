using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BraynManager : MonoBehaviour
{
    private List<Brayn> brayns = new List<Brayn>();
    [SerializeField] private GameObject BraynPrefab;
    [SerializeField] private Camera MainCamera;
    public static BraynManager singleton; //only 1 of the same kind, having more than 1 manager per task domain is useless and you know it.

    private int currentBrayn;

    public int braynsPerTeam = 1;
    public int teams = 2;

    void Start(){
        if(singleton != null) {
            Debug.LogError("saw another BraynManager, killing " + gameObject);
            Destroy(gameObject);
            return;
        }
        singleton = this;

        spawnBrayns(braynsPerTeam, teams);
        NextBrayn();
    }

    void Update(){ 
        
    }

    bool spawnBrayns(int braynsPerTeam, int teams) {
        Debug.Log("Spawning Brayns");
        spawnBrayn(currentBrayn, 0); //TEMP
        return true; //TEMP
    }

    bool spawnBrayn(int id, int team){
        Debug.Log("Spawning a Brayn for team " + team);
        Vector3 spawnPosition = new Vector3(5, 7);
        Brayn tBrayn = Instantiate(BraynPrefab, spawnPosition, Quaternion.identity).GetComponent<Brayn>();
        tBrayn.braynId = id;
        Debug.Log("sB, Spawned " + tBrayn);
        brayns.Add(tBrayn);
        return true; //TEMP
    }

    private void NextBrayn(){
        StartCoroutine(NextBraynCoroutine());
    }

    private IEnumerator NextBraynCoroutine(){
        Debug.LogWarning("Starting the Next Brayn Coroutine");
        int nextBrayn = currentBrayn + 1;
        Debug.Log("NBC, nextBrayn = " + nextBrayn);
        currentBrayn = -1; //drop current brayn turn, this stops control over current brayn after next Update tick. 
       
        yield return new WaitForSeconds(1);

        Debug.Assert(currentBrayn == -1, "currentBrayn was NOT dropped");

        currentBrayn = nextBrayn;
        currentBrayn = (currentBrayn >= brayns.Count) ? 0 : nextBrayn;
        Debug.Log("NBC, currentBrayn = " + currentBrayn);

        MainCamera.GetComponent<CameraController>().focusBrayn(brayns[currentBrayn]);
    }
}
