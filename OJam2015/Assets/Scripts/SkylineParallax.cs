using UnityEngine;
using System.Collections;

public class SkylineParallax : MonoBehaviour {

    public Camera mainCamera;
    public float speedMultiplier;
    private float lastCamPosition;

	// Use this for initialization
	void Start () {
        lastCamPosition = mainCamera.transform.position.x;
	}
	
	// Update is called once per frame
	void LateUpdate () {
	    
        float step = lastCamPosition - mainCamera.transform.position.x;

        this.transform.position = new Vector3(transform.position.x - step * speedMultiplier, transform.position.y, transform.position.z);

        lastCamPosition = mainCamera.transform.position.x;
	}
}
