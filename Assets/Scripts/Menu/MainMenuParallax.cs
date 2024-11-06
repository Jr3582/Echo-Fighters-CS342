using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuParallax : MonoBehaviour
{
    [SerializeField] private float parallaxFactor = 0.1f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = (Input.mousePosition.x / Screen.width) * 2 - 1;
        float targetX = startPos.x + mouseX * parallaxFactor;
        transform.position = new Vector3(targetX, startPos.y, startPos.z);
    }
}
