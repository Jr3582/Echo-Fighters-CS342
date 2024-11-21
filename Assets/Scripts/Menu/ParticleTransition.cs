using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParticleTransition : MonoBehaviour
{
    public GameObject particlePrefab;
    public float particleDuration = 2f;
    public string nextScene;
    public float particleCooldown = 6f;
    private float lastParticleTime = -6f;

    public void PlayParticle() {
        if (Time.time - lastParticleTime >= particleCooldown) {
            lastParticleTime = Time.time;

            SoundManager.Instance.PlaySound(SoundManager.Instance.menuButtonSound);
            GameObject particles = Instantiate(particlePrefab, Vector3.zero, Quaternion.identity);
            Destroy(particles, particleDuration);

            StartCoroutine(WaitAndTransition());
        }
    }

    private IEnumerator WaitAndTransition() {
        yield return new WaitForSeconds(particleDuration);
        SceneManager.LoadScene(nextScene);
    }
}
