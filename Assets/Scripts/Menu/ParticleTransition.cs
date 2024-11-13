using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParticleTransition : MonoBehaviour
{
    public GameObject particlePrefab;
    public float particleDuration = 2f;
    public string nextScene;

    public void PlayParticle() {
        GameObject particles = Instantiate(particlePrefab, Vector3.zero, Quaternion.identity);
        StartCoroutine(WaitAndTransition());
    }

    private IEnumerator WaitAndTransition() {
        yield return new WaitForSeconds(particleDuration);
        SceneManager.LoadScene(nextScene);
    }
}
