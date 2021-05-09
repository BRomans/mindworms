using UnityEngine;

public class CameraController : MonoBehaviour {
    private Camera cam;
    public float minZoom = -1f;
    public float maxZoom = -5f;
    public float zoomSpeed = 0.1f;
    public float mouseSpeed = 1; //TODO: lookAroundSpeed? It controls the speed with which the camera looks around when you move the mouse while right-clicking
    // Start is called before the first frame update
    void Start() {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButton(1)) { //right-click
            moveCamera();
        }

        if(Input.GetKeyDown(KeyCode.Q)) {
            resetPosition();
        }

        if(Input.GetAxis("Mouse ScrollWheel") != 0) {
            float distance = Mathf.Clamp(transform.position.z - Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, maxZoom, minZoom);
            transform.position = new Vector3(transform.position.x, transform.position.y, distance);
        }
    }

    private void moveCamera() {
        float x = mouseSpeed * Input.GetAxis("Mouse X");
        float y = mouseSpeed * Input.GetAxis("Mouse Y");
        transform.Translate(x, y, 0);
    }

    private void resetPosition() {
        transform.localPosition = Vector3.zero + Vector3.back * 3;
    }

    public void focusBrayn(Brayn braynToFocus) {
        transform.SetParent(braynToFocus.transform);
        resetPosition();
        Debug.Log("CC, Brayn Camera is now focused on " + transform.parent);
    }
}
