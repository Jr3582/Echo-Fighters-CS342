using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource effectSource;
    public AudioClip menuButtonSound;
    public AudioClip backButtonSound;
    public AudioClip continueButtonSound;
    public AudioClip arrowButtonSound;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(AudioClip clip) {
        if (effectSource != null && clip != null) {
            effectSource.PlayOneShot(clip);
        }
    }
}
