using UnityEngine;
using System.Collections;

public class BatLight : MonoBehaviour {

	public GameObject batty;
	private Vector3 startPosDif;

	// Use this for initialization
	void Start () {
		startPosDif = -batty.transform.position + this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = batty.transform.position + startPosDif;
	}
}
