using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "NightParser", menuName = "ScriptableObjects/NightParser", order = 1)]
public class NightParser : ScriptableObject
{
    [SerializeField] private bool value;
    public event Action OnValueChanged;
    public event Action OnValueTrue;

    public bool Value
    {
        get => value;
        set
        {
            if (this.value != value)
            {
                this.value = value;
                OnValueChanged?.Invoke();
                if (this.value)
                {
                    OnValueTrue?.Invoke();
                }
            }
        }
    }

    private void OnValidate()
    {
        // This method is called in the editor when a value is changed via the inspector.
        // Useful for editor-time changes.
        if (this.value)
        {
            OnValueTrue?.Invoke();
        }
    }

}
