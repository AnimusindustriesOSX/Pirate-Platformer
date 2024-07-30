using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip [] audioClips;
    AudioSource audioSource;
    int clipIndex;

     // Reference to the AudioSource component
    public bool isProximityAudio = false;
    public bool playOnAwake = false;
    private Transform listener; // Reference to the listener's transform (usually the player or camera)
    public float maxDistance = 10f; // The maximum distance at which the audio source is audible
    public float minVolume = 0f; // Minimum volume when at maximum distance
    public float maxVolume = 1f; // Maximum volume when very close

    
     void Start()
    {
        listener = GameObject.FindWithTag("Player").GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
        if(playOnAwake) playSound();
    }
    void Update()
    {
        if(isProximityAudio){
        // Calculate the distance between the listener and the audio source
        float distance = Vector3.Distance(transform.position, listener.position);

        // Calculate the volume based on the distance
        float volume = Mathf.Clamp01(1 - (distance / maxDistance));
        volume = Mathf.Lerp(minVolume, maxVolume, volume);

        // Apply the calculated volume to the AudioSource
        audioSource.volume = volume;
        }
    }
    // Start is called before the first frame update
   

    public void playSound(){
        clipIndex = UnityEngine.Random.Range(0, audioClips.Length);
        gameObject.GetComponent<AudioSource> ().clip = audioClips[clipIndex];
        audioSource.Play();
    }



}
