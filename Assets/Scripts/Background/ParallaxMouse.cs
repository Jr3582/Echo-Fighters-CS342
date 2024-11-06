using UnityEngine;

public class MouseParallax : MonoBehaviour {
    public Transform[] backgroundLayers;
    public float parallaxIntensity = 0.1f;
    private Vector3[] startPositions;

    void Start() {
        startPositions = new Vector3[backgroundLayers.Length];
        for (int i = 0; i < backgroundLayers.Length; i++)
        {
            startPositions[i] = backgroundLayers[i].position;
        }
    }

    void Update() {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Vector2 mouseOffset = (mousePosition - screenCenter) / screenCenter; // Normalized [-1, 1] range

        for (int i = 0; i < backgroundLayers.Length; i++) {
            float depthFactor = (i + 1) * parallaxIntensity;
            Vector3 targetPosition = startPositions[i] + new Vector3(mouseOffset.x * depthFactor, mouseOffset.y * depthFactor, 0);
            backgroundLayers[i].position = Vector3.Lerp(backgroundLayers[i].position, targetPosition, Time.deltaTime * 5);
        }
    }
}
