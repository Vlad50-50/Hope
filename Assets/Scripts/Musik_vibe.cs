using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musik_vibe : MonoBehaviour
{  
    public AudioClip musicClip;
    private AudioSource audioSource;

    void Awake() {
        DontDestroyOnLoad(gameObject);

    }

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = musicClip;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.Play();
    }
}
