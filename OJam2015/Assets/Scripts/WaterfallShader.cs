using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class WaterfallShader : MonoBehaviour
{
    private Material _material;

    void Awake()
    {
        if (GetComponent<Renderer>() != null)
        {
            // GetComponent<Renderer>().material creates its own instance of the material, so it's not shared
            _material = GetComponent<Renderer>().material;
        }
        else if (GetComponent<Image>() != null)
        {
            // For the UI images, GetComponent<Image>().material is a shared material, so we have to clone it
            _material = Instantiate(GetComponent<Image>().material);
            GetComponent<Image>().material = _material;
        }
    }

    public void StartFountain(Action callback)
    {
        _material.SetFloat("_UseInverseRatio", 0f);
        StartCoroutine(StartFountainCoroutine(callback));
    }

    private IEnumerator StartFountainCoroutine(Action callback)
    {
        float ratio = 0f;

        while (ratio < 1f)
        {
            ratio += Time.deltaTime / 0.5f;

            _material.SetFloat("_Ratio", Mathf.Lerp(1f, 0f, ratio));

            yield return null;
        }

        if (callback != null)
        {
            callback();
        }
    }

    public void StopFountain(Action callback)
    {
        _material.SetFloat("_UseInverseRatio", 1f);
        StartCoroutine(StopFountainCoroutine(callback));
    }

    private IEnumerator StopFountainCoroutine(Action callback)
    {
        float ratio = 0f;

        while (ratio < 1f)
        {
            ratio += Time.deltaTime / 0.5f;

            _material.SetFloat("_Ratio", Mathf.Lerp(1f, 0f, ratio));

            yield return null;
        }

        if (callback != null)
        {
            callback();
        }
    }
}
