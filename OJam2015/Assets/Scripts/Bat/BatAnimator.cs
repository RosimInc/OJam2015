using UnityEngine;
using System.Collections;

public class BatAnimator : MonoBehaviour {

    public Transform leftWing;
    public Transform rightWing;
    public float rotationSpeed = 3.5f;
    public float maxRotationAngle = 50f;
    public float minRotationAngle = -50f;

    float ratio = 0f;

	// Use this for initialization
	void Start () {
    
	}
	
	// Update is called once per frame
	void Update () {

        flapWings();

	}

    void flapWings() {

        //float rotation = Mathf.Clamp( rotationSpeed * Time.deltaTime, minRotationAngle, maxRotationAngle );

        ratio += Time.deltaTime * rotationSpeed;

        float rotation = Mathf.Lerp(maxRotationAngle, minRotationAngle, Mathf.Pow( Mathf.Abs(Mathf.Sin(ratio)), 0.5f));

        leftWing.localEulerAngles = new Vector3(leftWing.localEulerAngles.x, leftWing.localEulerAngles.y, rotation);
        rightWing.localEulerAngles = new Vector3(rightWing.localEulerAngles.x, rightWing.localEulerAngles.y, -rotation);




        /*
        float angle = Mathf.Clamp( leftWing.localRotation.eulerAngles.z >= 180 ? leftWing.localRotation.eulerAngles.z - 360 : leftWing.localRotation.eulerAngles.z , minRotationAngle, maxRotationAngle );

        if (angle == maxRotationAngle || angle == minRotationAngle) {
            rotation = rotation * -1;
        }

        Debug.Log("rotation: " + rotation + "angle: " + leftWing.localRotation.eulerAngles.z);

        leftWing.RotateAround(leftWing.parent.transform.position, new Vector3(0, 0, 1), rotation);
        rightWing.RotateAround(rightWing.parent.transform.position, new Vector3(0, 0, -1), rotation);*/

        //leftWing.localEulerAngles = new Vector3(leftWing.localEulerAngles.x, leftWing.localEulerAngles.y, leftWing.localRotation.z + 20);
        //rightWing.localEulerAngles = new Vector3(rightWing.localEulerAngles.x, rightWing.localEulerAngles.y, rightWing.localRotation.z + 20);


    }
}
