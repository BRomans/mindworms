using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BraynHealth))]
public class Brayn : MonoBehaviour {
    public int braynId;
    public int teamId;
    public int movementSpeed = 1;
    public BraynHealth health;
    private SpriteRenderer rend;
    // Start is called before the first frame update
    void Start() {
        health = GetComponent<BraynHealth>();
        rend = GetComponent<SpriteRenderer>();

        name = "Brayn " + braynId + "(" + teamId + ")";
    }

    // Update is called once per frame
    void Update() {
    }

    private bool Die() {
        return true;
    }

    public override System.String ToString() {
        return "Brayn " + braynId + ", which plays for team " + teamId;
    }
}
