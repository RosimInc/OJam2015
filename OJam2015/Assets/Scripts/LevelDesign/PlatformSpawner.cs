using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PlatformSpawner : MonoBehaviour {

	public int length;
	public GameObject hint;

	private const float width = 2.75f;

	// Use this for initialization
	void Start () {

		Generator.Instance.AddPlatform(transform.position.x, transform.position.y, length);

		Destroy(hint);
		Destroy(gameObject);
		Destroy(this);
	}
	
	// Update is called once per frame
	void Update () {
		//float numTiles = (int)(length / width);
		//float len = numTiles * length;
		hint.transform.localScale = new Vector3(length, 0.1f, 1);
		hint.transform.position = new Vector3((length-1) * width / 2f, 0, 0);
	}
}
