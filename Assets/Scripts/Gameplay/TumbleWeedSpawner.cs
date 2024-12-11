using UnityEngine;

public class TumbleWeedSpawner : MonoBehaviour
{
    public GameObject tumbleweedPrefab;
    public float spawnInterval = 2f;
    public float minY = -3f;
    public float maxY = 3f;
    public float spawnX = 10f;
    public float tumbleweedSpeed = 5f;

    private void Start() {
        InvokeRepeating(nameof(SpawnTumbleweed), 0f, spawnInterval);
    }

    private void SpawnTumbleweed() {
        float randomY = Random.Range(minY, maxY);

        Vector3 spawnPosition = new Vector3(spawnX, randomY, 0f);

        GameObject tumbleweed = Instantiate(tumbleweedPrefab, spawnPosition, Quaternion.identity);

        Rigidbody2D rb = tumbleweed.GetComponent<Rigidbody2D>();
        if (rb == null) {
            rb = tumbleweed.AddComponent<Rigidbody2D>();
        }
        rb.velocity = new Vector2(-tumbleweedSpeed, 0f);

        Destroy(tumbleweed, 30f);
    }
}
