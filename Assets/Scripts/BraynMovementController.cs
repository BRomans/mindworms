using UnityEngine;

public class BraynMovementController : MonoBehaviour {
    public float speed = 1f;
    public float jumpSpeed = 0.2f;
    private float movement = 0f;
    private bool isGrounded = true;
    private Rigidbody2D rigidBody;

    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update() {
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
    }

    void OnCollisionStay2D(Collision2D other) {
        if(other.transform.tag == "Terrain") isGrounded = true;
    }
}
