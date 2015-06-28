using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(AudioSource))]
public class Waterfall : Activatable
{
    public GameObject WaterVeil;
    public WaterfallShader WaterShader;
    
    public bool activated = true;

    private AudioSource _waterSound;
    private Collider2D _collider;

    void Awake()
    {
        _waterSound = GetComponent<AudioSource>();
        _collider = GetComponent<Collider2D>();
    }

    void Start()
    {
        if (!activated)
        {
            activated = !activated;
            Activate(null);
        }
    }

    public override void Activate(Action callback)
    {
        if (!activated)
        {
            WaterShader.StartFountain(() => AfterAnimation(callback));
            _waterSound.Play();
        }
        else if (activated)
        {
            WaterShader.StopFountain(() => AfterAnimation(callback));
        }
    }

    private void AfterAnimation(Action callback)
    {
        if (activated)
        {
            _waterSound.Stop();
        }

        activated = !activated;

        _collider.enabled = activated;

        if (callback != null)
        {
            callback();
        }
    }
}
