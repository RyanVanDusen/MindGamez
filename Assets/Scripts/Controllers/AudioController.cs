using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour {

    [HideInInspector]
    public AudioSource audioSource;
    [SerializeField]
    AudioClip audioClip;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
	}

    public void Play()
    {
        audioSource.PlayOneShot(audioClip, 1);
    }
}
