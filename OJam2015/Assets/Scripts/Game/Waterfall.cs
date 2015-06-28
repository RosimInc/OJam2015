using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Waterfall : Activatable
{
    public GameObject WaterVeil;

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
            Activate();
        }
    }


    public override void Activate()
    {
        activated = !activated;

        if (activated && !_waterSound.isPlaying)
        {
            _waterSound.Play();
        }
        else if (!activated && _waterSound.isPlaying)
        {
            _waterSound.Stop();
        }

        _collider.enabled = activated;

        WaterVeil.SetActive(activated);
    }
}
