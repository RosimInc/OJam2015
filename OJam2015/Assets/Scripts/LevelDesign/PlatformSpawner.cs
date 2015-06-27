using UnityEngine;
using System.Collections;

public class PlatformSpawner : MonoBehaviour {

	public int length;

	// Use this for initialization
	void Start () {

		Generator.Instance.AddPlatform(transform.position.x, transform.position.y, length);

		Destroy(gameObject);
		Destroy(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
