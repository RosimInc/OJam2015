using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Camera))]
public class BatCamera : MonoBehaviour {

	public GameObject batty, platform, levelHolder;
	public Vector2 minPos = new Vector2(-10, -10);
	public Vector2 maxPos = new Vector2(150, 20);
	private Camera cam;
	private Vector3 startPosDif;

	void Awake()
	{
		Generator gen = Generator.Instance;
		gen.platform = platform;
		gen.levelHolder = levelHolder;
	}

	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera>();
		startPosDif = -batty.transform.position + this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = startPosDif + batty.transform.position;
		transform.position = new Vector3(
			Mathf.Clamp(newPos.x, minPos.x, maxPos.x),
			Mathf.Clamp(newPos.y, minPos.y, maxPos.y),
			newPos.z);
	}
}
