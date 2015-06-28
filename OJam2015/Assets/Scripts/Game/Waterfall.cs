using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(AudioSource))]
public class Waterfall : Activatable
{
    public GameObject WaterVeil;
    public WaterfallShader WaterShader;

    private bool _activated = true;

    private AudioSource _waterSound;
    private Collider2D _collider;

    void Awake()
    {
        _waterSound = GetComponent<AudioSource>();
        _collider = GetComponent<Collider2D>();
    }

    public override void Activate(Action callback)
    {
        if (!_activated)
        {
            WaterShader.StartFountain(() => AfterAnimation(callback));
            _waterSound.Play();
        }
        else if (_activated)
        {
            WaterShader.StopFountain(() => AfterAnimation(callback));
        }
    }

    private void AfterAnimation(Action callback)
    {
        if (_activated)
        {
            _waterSound.Stop();
        }

        _activated = !_activated;

        _collider.enabled = _activated;

        if (callback != null)
        {
            callback();
        }
    }
}
