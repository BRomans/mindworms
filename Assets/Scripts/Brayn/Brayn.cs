using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BraynHealth))]
public class Brayn : MonoBehaviour {
    [HideInInspector]
    public int BraynId;
    public int TeamId;
    [HideInInspector]
    public Weapon CurrentWeapon = null;
    [SerializeField] 
    private BraynHealth health;
    private SpriteRenderer rend;
    // Start is called before the first frame update
    void Start() {
        health = GetComponent<BraynHealth>();
        rend = GetComponent<SpriteRenderer>();

        name = "Brayn " + BraynId + "(" + TeamId + ")";
    }

    // Update is called once per frame
    void Update() {
    }

    private bool Die() {
        return true;
    }

    public override System.String ToString() {
        return "Brayn " + BraynId + ", which plays for team " + TeamId;
    }
}
