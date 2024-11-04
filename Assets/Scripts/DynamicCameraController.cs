using UnityEngine;

public class DynamicCameraController : MonoBehaviour {
    public Transform player1;
    public Transform player2;

    [SerializeField] private float minZoom = 4f;
    [SerializeField] private float maxZoom = 10f;
    [SerializeField] private float zoomFactor = 1f;
    [SerializeField] private float leftZoomFactor = 0.5f;
    [SerializeField] private float rightZoomFactor = 0.5f;
    [SerializeField] private float smoothSpeed = 0.125f;

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

        float distance = Vector3.Distance(player1.position, player2.position);
        float desiredZoom = Mathf.Clamp(minZoom + (distance * zoomFactor), minZoom, maxZoom);

        if (player1.position.x < minX + 1f || player2.position.x < minX + 1f) {
            float leftZoomAdjustment = Mathf.Max(
                Mathf.Abs(player1.position.x - minX),
                Mathf.Abs(player2.position.x - minX)
            ) * leftZoomFactor;
            desiredZoom += leftZoomAdjustment;
        }
        
        if (player1.position.x > maxX - 1f || player2.position.x > maxX - 1f) {
            float rightZoomAdjustment = Mathf.Max(
                Mathf.Abs(player1.position.x - maxX),
                Mathf.Abs(player2.position.x - maxX)
            ) * rightZoomFactor;
            desiredZoom += rightZoomAdjustment;
        }

        desiredZoom = Mathf.Min(desiredZoom, 7f);

        if (cam.orthographic) {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, desiredZoom, smoothSpeed);
        } else {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, desiredZoom, smoothSpeed);
        }
    }

}
