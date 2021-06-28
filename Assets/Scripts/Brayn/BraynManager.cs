using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UDP_receiver))]
public class BraynManager : MonoBehaviour {
    public UDP_receiver udpReceiver;

    private List<Brayn> brayns = new List<Brayn>();
    [SerializeField] private GameObject braynPrefab;
    public Camera MainCamera;
    public static BraynManager Singleton; //only 1 of the same kind, having more than 1 manager per task domain is useless and you know it.
    private int currentBrayn;

    public int CurrentBrayn { get { return currentBrayn; } }

    [Header("Teams")]
    public int BraynsPerTeam = 1;
    public int Teams = 2;

    public void Start() {
        if(Singleton != null) {
            Debug.LogError("saw another BraynManager, killing " + gameObject);
            Destroy(gameObject);
            return;
        }
        Singleton = this;

        udpReceiver = GetComponent<UDP_receiver>();
        udpReceiver.listenPort = 1048;

        setCamera();

        spawnBrayns(BraynsPerTeam, Teams);
        NextBrayn();
    }

    public void Update() {
        if(Input.GetKeyDown(KeyCode.P)) {
            NextBrayn();
        }
    }

    private void setCamera() {
        if(!MainCamera)
            MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private bool spawnBrayns(int braynsPerTeam, int teams) {
        Debug.Log("Spawning Brayns");
        for(int iTBaynsPerTeam = 1; iTBaynsPerTeam <= braynsPerTeam; iTBaynsPerTeam++) { //starting at 1 each as input from Inspector starts at 1 for human convenience
            for(int iTeams = 1; iTeams <= teams; iTeams++) {
                spawnBrayn(currentBrayn, iTeams);
                Debug.Log("iteams " + iTeams);
                currentBrayn++;
            }
        }

        //spawnBrayn(currentBrayn, 0); //TEMP
        //spawnBrayn(currentBrayn+1, 1); //TEMP
        return true; //TEMP
    }

    private bool spawnBrayn(int id, int team) {
        Debug.Log("Spawning a Brayn for team " + team);
        Vector3 spawnPosition = (team == 1) ? new Vector3(5, 10) : new Vector3(-30, 15);
        Brayn tBrayn = Instantiate(braynPrefab, spawnPosition, Quaternion.identity).GetComponent<Brayn>();
        //tBrayn.transform.parent = 
        tBrayn.BraynId = id;
        tBrayn.TeamId = team;
        Debug.Log("sB, Spawned " + tBrayn);
        brayns.Add(tBrayn);
        return true; //TEMP
    }

    private void NextBrayn() {
        StartCoroutine(NextBraynCoroutine());
    }

    private IEnumerator NextBraynCoroutine() {
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
