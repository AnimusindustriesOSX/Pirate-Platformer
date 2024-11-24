using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.Experimental.GraphView;
#endif
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource saneExplorationMusic; 
    public AudioSource saneCombatMusic; 

    public AudioSource insaneExplorationMusic; 
    public AudioSource insaneCombatMusic; 
    private bool isPausedOrMinimized;
    private AudioSource currentAudioSource; 
    public AudioSource deathTrack; 
    public float fadeDuration = 2f; // Duration of the fade effect in seconds
    private PlayerController player;
    private DayNightCycleController timer;
    public int queueCount;
    private bool QueueCoolDown;
    private bool isTransitioning = false; // Flag to check if a transition is in progress
    private Queue<AudioSource> transitionQueue = new Queue<AudioSource>(); // Queue for transitions
    void Start()
    {
        currentAudioSource = saneExplorationMusic;
        QueueCoolDown = false;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        timer = GameObject.FindWithTag("Time").GetComponent<DayNightCycleController>();
        // Start the transition coroutine
        
    }
     void OnApplicationPause(bool pauseStatus)
    {
        isPausedOrMinimized = pauseStatus;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        isPausedOrMinimized = !hasFocus;
    }

    void Update(){
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        timer = GameObject.FindWithTag("Time").GetComponent<DayNightCycleController>();
        if(QueueCoolDown == false){
            if(player.HP <= 0 ) {
            QueueCoolDown = true;
            RequestTransition(deathTrack);
        }
        else if(!timer.isNight && player.insanity < player.MaxInsanity){
            QueueCoolDown = true;
            RequestTransition(saneExplorationMusic);
        } 
        
        else if(!timer.isNight && player.insanity >= player.MaxInsanity) {
            QueueCoolDown = true;
            RequestTransition(insaneExplorationMusic);
        }
            
        else if(timer.isNight && player.insanity >= player.MaxInsanity) {
             QueueCoolDown = true;
             RequestTransition(insaneCombatMusic);
        }
            
        else if(timer.isNight && player.insanity < player.MaxInsanity) {
            QueueCoolDown = true;
            RequestTransition(saneCombatMusic);
        }
        queueCount = transitionQueue.Count;
        }
        
    }



public void RequestTransition(AudioSource newAudioSource)
    {
        // Check if the new audio source is already playing
        if (currentAudioSource.clip == newAudioSource.clip && currentAudioSource.isPlaying)
        {
            //Debug.Log("Requested audio source is already playing. No transition needed.");
            QueueCoolDown = false;
        }
        else{
            transitionQueue.Enqueue(newAudioSource);

        if (!isTransitioning)
        {
            StartCoroutine(ProcessTransitionQueue());

        }
        }
        
        
    }

    IEnumerator ProcessTransitionQueue()
    {
        
        while (transitionQueue.Count > 0)
        {
            isTransitioning = true;
            AudioSource nextAudioSource = transitionQueue.Dequeue();

            yield return StartCoroutine(FadeOutAndIn(currentAudioSource, nextAudioSource, fadeDuration));

            // After the current transition, the new audio source becomes the primary one
            currentAudioSource = nextAudioSource;
            QueueCoolDown = false;
            isTransitioning = false;
            
        }
        
    }

    IEnumerator FadeOutAndIn(AudioSource fadeOutSource, AudioSource fadeInSource, float duration)
    {
        float elapsedTime = 0f;
        

        // Ensure the second audio source is stopped and starts playing from the correct position
        //fadeInSource.Stop();
        //fadeInSource.time = fadeOutSource.time; // Sync the playback position
        fadeInSource.Play();
        fadeInSource.volume = 0f;
        fadeOutSource.volume = 1f;

        // Fade out the first audio source and fade in the second audio source
        while (elapsedTime < duration)
        {
            if (isPausedOrMinimized)
            {
                yield return null;
                continue;
            }
            
            
            float volumeOut = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            float volumeIn = Mathf.Lerp(0f, 1f, elapsedTime / duration);

            fadeOutSource.volume = volumeOut;
            fadeInSource.volume = volumeIn;

            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        // Ensure the volumes are exactly at their final values
        fadeOutSource.volume = 0f;
        fadeInSource.volume = 1f;

        // Optionally stop the first audio source after fading out
        fadeOutSource.Stop();
        
    }
}

