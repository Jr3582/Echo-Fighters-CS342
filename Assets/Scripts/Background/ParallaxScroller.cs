using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroller : MonoBehaviour {
    [SerializeField] private List<Transform> backgroundLayers;
    [SerializeField] private float baseSpeed = 1f;
    [SerializeField] private float speedMultiplier = 0.5f;

    private void Update() {
        for (int i = 0; i < backgroundLayers.Count; i++)
        {
            float layerSpeed = baseSpeed * (1 - i * speedMultiplier);
            backgroundLayers[i].Translate(Vector3.right * layerSpeed * Time.deltaTime);
        }
    }
}
