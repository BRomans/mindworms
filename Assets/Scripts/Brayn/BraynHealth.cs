using UnityEngine;

public class BraynHealth : MonoBehaviour {
    private int currentHealth;
    public int initHealth = 100;
    public int CurrentHealth => currentHealth;
    void Start() {
        currentHealth = initHealth;
    }

    public void ChangeHealth(int amount) {
        currentHealth += amount;
    }
}
