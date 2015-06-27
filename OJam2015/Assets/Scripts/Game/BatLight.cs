using UnityEngine;
using System.Collections;

public class BatLight : MonoBehaviour {

	public GameObject batty;
	private Vector3 startPosDif;
	private float lightChangeTime = 0f;
	private float initialRange;
	private float initialIntensity;
	private float initialSpotAngle;

	private Light spot;

	// Use this for initialization
	void Start () {
		startPosDif = -batty.transform.position + this.transform.position;
		spot = GetComponent<Light>();
		initialRange = spot.range;
		initialIntensity = spot.intensity;
		initialSpotAngle = spot.spotAngle;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = batty.transform.position + startPosDif;

		lightChangeTime -= Time.deltaTime;
		if (lightChangeTime <= 0)
		{
			spot.range = initialRange * (1 + 0.1f * GetRandomValue());
			spot.intensity = initialIntensity * (1 + 0.06f * GetRandomValue());
			spot.spotAngle = initialSpotAngle * (1 + 0.016f * GetRandomValue());
			lightChangeTime = Random.value * 0.2f + 0.02f;
		}
	}

	private float GetRandomValue()
	{
		return Random.value * 2 - 1;
	}
}
