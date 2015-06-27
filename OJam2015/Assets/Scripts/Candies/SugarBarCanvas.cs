using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Canvas))]
public class SugarBarCanvas : MonoBehaviour
{
    private Canvas _canvas;

    void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }

    void Start()
    {
        _canvas.worldCamera = Camera.main;
    }
}
