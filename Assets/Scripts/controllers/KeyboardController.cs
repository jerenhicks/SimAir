using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour {

    public static KeyboardController instance = null;
    private static float speed = 5.0f;
    private Camera cam = null;
    private float minZoom = 5f;
    private float maxZoom = 20f;
    private static float zoomSpeed = 3f * speed;
    private Vector3 lastPanPosition;
    private static readonly float PanSpeed = 15f;
    private static readonly float[] BoundsZ = new float[] { 0f, 1000f };

    // Use this for initialization
    void Start () {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ControlPanelCanvasHandler handler = GameObject.Find("ControlPanelCanvas").GetComponent<ControlPanelCanvasHandler>();
            handler.togglePanel();
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            cam.transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0), Space.World);
            //cam.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            cam.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            //cam.transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
            cam.transform.Translate(new Vector3(0, 0, -1 * speed * Time.deltaTime), Space.World);
            this.clampPosition();
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            cam.transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        }

        var d = Input.GetAxis("Mouse ScrollWheel");
        if (d > 0f) {
            // scroll in
            if (cam.transform.position.y > minZoom) {
                cam.transform.position -= new Vector3(0, zoomSpeed * Time.deltaTime, 0);
            }
        } else if (d < 0f) {
            // scroll away
            if (cam.transform.position.y < maxZoom) {
                cam.transform.position += new Vector3(0, zoomSpeed * Time.deltaTime, 0);
            }
        }

        if (Input.GetMouseButtonDown(0)) {
            lastPanPosition = Input.mousePosition;
        } else if (Input.GetMouseButton(0)) {
            // Determine how much to move the camera
            Vector3 offset = cam.ScreenToViewportPoint(lastPanPosition - Input.mousePosition);
            Vector3 move = new Vector3(offset.x * PanSpeed, 0, offset.y * PanSpeed);

            // Perform the movement
            cam.transform.Translate(move, Space.World);

            // Ensure the camera remains within bounds.
            this.clampPosition();

            // Cache the position
            lastPanPosition = Input.mousePosition;
        }
    }

    private void clampPosition() {
        Vector3 pos = cam.transform.position;
        pos.x = cam.transform.position.x;
        pos.y = cam.transform.position.y;
        pos.z = Mathf.Clamp(cam.transform.position.z, BoundsZ[0], BoundsZ[1]);
        cam.transform.position = pos;
    }
}
