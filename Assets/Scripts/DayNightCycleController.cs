using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class DayNightCycleController : MonoBehaviour
{
    // Start is called before the first frame update
    
    
    public bool isNight = false;
    
    public Animator animator;
    public float animatorSpeedMagnitude;
    private float baseAnimSpeed;
    void Start()
    {
        baseAnimSpeed = animator.speed;
    }
    // Update is called once per frame
    void Update()
    {
        animator.speed = baseAnimSpeed * animatorSpeedMagnitude;
    }
    
}
