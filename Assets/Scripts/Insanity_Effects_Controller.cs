
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Insanity_Effects_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    
    
    public bool isPlayerInsane;
    [SerializeField] Transform Player_Head;
    private Animator head_Animator;
    private PlayerController player;
    [Header("Isanity PostProcess")]
    [SerializeField] Volume Insanity_Volume;
    [SerializeField] float vignette_intensity;
    [SerializeField] float saturation;
    public float targetIntensity = 1.0f; // The target intensity value
    public float duration = 2.0f; // The duration for the transition

    private Vignette vignette;
    private ColorAdjustments colorAdjustments;

     private ChromaticAberration chromaticAberration;
    
    


    
    void Start()
    {
        head_Animator = Player_Head.GetComponent<Animator>();
        player = transform.GetComponent<PlayerController>();

        //post process volume
        Insanity_Volume.profile.TryGet<Vignette>(out vignette);
        
        Insanity_Volume.profile.TryGet<ColorAdjustments>(out colorAdjustments);
        Insanity_Volume.profile.TryGet<ChromaticAberration>(out chromaticAberration);
    }

    // Update is called once per frame
    void Update()
    {
        VolumeEdit();
        if(player.insanity >= player.MaxInsanity){
            isPlayerInsane = true;
        }
        else isPlayerInsane = false;

        
        InsanityProc();

    }
    
    void InsanityProc(){
        if(isPlayerInsane == true){
            head_Animator.SetBool("isInsane",true);
            StartCoroutine(ChangeChromaticAberrationIntensity());
        }
        else{
            head_Animator.SetBool("isInsane",false);
        }
    }
    void VolumeEdit(){
        vignette.intensity.value = vignette_intensity * (player.insanity/player.MaxInsanity);
        colorAdjustments.saturation.value = saturation* (player.insanity/player.MaxInsanity);
         //Debug.Log(colorAdjustments.saturation.value);
    }

     private bool isChanging = false; // To ensure the coroutine is not started multiple times

    private IEnumerator ChangeChromaticAberrationIntensity()
    {
        isChanging = true;
        
        while (isPlayerInsane)
        {
            // Transition to target intensity
            yield return StartCoroutine(ChangeIntensityRoutine(targetIntensity, duration));

            // Transition back to zero
            yield return StartCoroutine(ChangeIntensityRoutine(0.0f, duration));
        }

        isChanging = false;
    }

    private IEnumerator ChangeIntensityRoutine(float target, float duration)
    {
        float startIntensity = chromaticAberration.intensity.value;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            chromaticAberration.intensity.value = Mathf.Lerp(startIntensity, target, elapsed / duration);
            yield return null;
        }

        chromaticAberration.intensity.value = target;
    }
}