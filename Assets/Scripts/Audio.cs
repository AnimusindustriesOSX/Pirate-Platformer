using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip [] audioClips;
    AudioSource audioSource;
    int clipIndex;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void playSound(){
        clipIndex = UnityEngine.Random.Range(0, audioClips.Length);
        gameObject.GetComponent<AudioSource> ().clip = audioClips[clipIndex];
        audioSource.Play();
    }
}
