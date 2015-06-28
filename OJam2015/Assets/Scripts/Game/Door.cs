using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Door : Activatable
{
    public float FinalHeight = 15f;
    public float AnimationDuration = 1f;

    private bool _activated = false;

    private AudioSource _activationSound;

    void Awake()
    {
        _activationSound = GetComponent<AudioSource>();
    }

    public override void Activate()
    {
        if (!_activated)
        {
            _activated = true;

            StartCoroutine(SlideDoor());
        }
    }

    private IEnumerator SlideDoor()
    {
        float ratio = 0f;

        Vector3 initialPosition = transform.localPosition;
        Vector3 finalPosition = new Vector3(transform.localPosition.x, FinalHeight, transform.localPosition.z);

        while (ratio < 1f)
        {
            ratio += Time.deltaTime / AnimationDuration;

            transform.localPosition = Vector3.Lerp(initialPosition, finalPosition, Mathf.Pow(ratio, 5));

            yield return null;
        }

        if (_activationSound.isPlaying)
        {
            _activationSound.Stop();
        }
    }
}
