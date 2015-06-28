using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Waterfall : Activatable
{
    public GameObject WaterVeil;

    private bool _activated = true;

    private AudioSource _waterSound;
    private Collider2D _collider;

    void Awake()
    {
        _waterSound = GetComponent<AudioSource>();
        _collider = GetComponent<Collider2D>();
    }

    public override void Activate()
    {
        _activated = !_activated;

        if (_activated && !_waterSound.isPlaying)
        {
            _waterSound.Play();
        }
        else if (!_activated && _waterSound.isPlaying)
        {
            _waterSound.Stop();
        }

        _collider.enabled = _activated;

        WaterVeil.SetActive(_activated);
    }
}
