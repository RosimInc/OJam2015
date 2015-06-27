using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public abstract class Activatable : MonoBehaviour
{
    protected AudioSource _activationSound;

    private bool _activated = false;

    void Awake()
    {
        _activationSound = GetComponent<AudioSource>();
    }

    protected abstract void ActivateAnimation();

    public void Activate()
    {
        if (!_activated)
        {
            _activated = true;

            if (!_activationSound.isPlaying)
            {
                _activationSound.Play();
            }

            ActivateAnimation();
        }
    }
}
