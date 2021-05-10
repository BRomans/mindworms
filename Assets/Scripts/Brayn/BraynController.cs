using System;
using UnityEngine;

public class BraynController : MonoBehaviour {
    public float speed = 1f;
    public float jumpSpeed = 0.2f;
    private float movement = 0f;
    private bool isGrounded = true;
    private Rigidbody2D rigidBody;
    private Brayn brayn;

    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        brayn = GetComponent<Brayn>();
    }

    void Update() {
        if(BraynManager.Singleton.CurrentBrayn != brayn.BraynId)
            return;
        movement = Input.GetAxis("Horizontal");
        if(movement > 0f) {
            rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
        } else if(movement < 0f) {
            rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
        } else {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }

        if(Input.GetButtonDown("Jump") && isGrounded) {  //Gives double jump
            isGrounded = false;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
        }

            if(Input.GetKeyDown(KeyCode.Alpha1)) {
                //brayn.CurrentWeapon = Instantiate<>
            } else if(Input.GetKeyDown(KeyCode.Alpha2)) {
                //brayn.CurrentWeapon = new BrainFart();
            }
    }

    void OnCollisionStay2D(Collision2D other) {
        if(other.transform.tag == "Terrain")
            isGrounded = true;
    }
}
