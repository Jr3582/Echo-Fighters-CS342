using UnityEngine;

public class DynamicCameraController : MonoBehaviour {
    public Transform player1;
    public Transform player2;

    [SerializeField] private float minZoom = 4f;
    [SerializeField] private float maxZoom = 7f;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private float zoomStartDistance = 12f;

    public float minX = -10f;
    public float maxX = 10f;
    private Camera cam;

    void Start() {
        cam = GetComponent<Camera>();
    }

    void LateUpdate() {
        if (player1 == null || player2 == null) return;

        Vector3 middlePoint = (player1.position + player2.position) / 2f;

        Vector3 newPosition = new Vector3(
            Mathf.Clamp(middlePoint.x, minX, maxX),
            Mathf.Max(middlePoint.y, 0),
            transform.position.z
        );

        transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed);

        float xDistance = Mathf.Abs(player1.position.x - player2.position.x);
        float t = Mathf.InverseLerp(zoomStartDistance, zoomStartDistance * 2, xDistance);
        float desiredZoom = Mathf.Lerp(minZoom, maxZoom, t);

        if (cam.orthographic) {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, desiredZoom, smoothSpeed);
        } else {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, desiredZoom, smoothSpeed);
        }
    }
}
