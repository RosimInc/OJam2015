using UnityEngine;
using System.Collections;

public class CandyRotation : MonoBehaviour
{
    public float Speed = 20f;

    private Transform _transform;

	void Awake()
    {
        _transform = GetComponent<Transform>();
    }
	
	void Update ()
    {
        _transform.Rotate(0f, 0f, Time.deltaTime * -Speed);
	}
}
