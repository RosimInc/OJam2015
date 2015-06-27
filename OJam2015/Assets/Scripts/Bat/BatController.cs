using UnityEngine;
using System.Collections;
using InputHandler;

public class BatController : MonoBehaviour {

    public float maxVelocity;
    private float minVelocity;
    private float velocityX = 0f;
    private float velocityY = 0f;
    public float velocityFactor;
    public float minMovementRange;
    public float minVelocityRange;

    private bool isRadarActive = false;

	// Use this for initialization
	void Start () {
        InputManager.Instance.PushActiveContext("Gameplay");
        InputManager.Instance.AddCallback(0, HandleBatActions);

        minVelocity = -1 * maxVelocity;
	}

    private void HandleBatActions(MappedInput input) {

        Move(input);

        


        if (input.Actions.Contains("Action")) {



        }


        
    }


    private void Move(MappedInput input){
        if (input.Ranges.ContainsKey("MoveHorizontal")) {
            float rangeX = input.Ranges["MoveHorizontal"];

            if (rangeX >= minMovementRange || rangeX <= -1 * minMovementRange) {
                
                velocityX = Mathf.Clamp(velocityX + velocityFactor * Time.deltaTime * rangeX, minVelocity, maxVelocity);

            }

            // if the joystick is in the deadzone, slow down the character
            else {
                if (velocityX > 0) {
                    velocityX = Mathf.Clamp(velocityX - velocityFactor * 0.5f * Time.deltaTime, 0f, maxVelocity);
                }
                else {
                    velocityX = Mathf.Clamp(velocityX + velocityFactor * 0.5f * Time.deltaTime, minVelocity, 0f);
                }

            }

            
        }

        if (input.Ranges.ContainsKey("MoveVertical")) {
            float rangeY = input.Ranges["MoveVertical"];

            if (rangeY >= minMovementRange || rangeY <= -1 * minMovementRange) {

                velocityY = Mathf.Clamp(velocityY + velocityFactor * Time.deltaTime * rangeY, minVelocity, maxVelocity);

            }
            // if the joystick is in the deadzone, slow down the character
            else {

                if (velocityY > 0) {

                    velocityY = Mathf.Clamp(velocityY - velocityFactor *0.5f  * Time.deltaTime, 0f, maxVelocity);
                }
                else {
                    velocityY = Mathf.Clamp(velocityY + velocityFactor * 0.5f * Time.deltaTime, minVelocity, 0f);
                }
            }

        }

        transform.position = new Vector3(
            transform.position.x + velocityX,
            transform.position.y + velocityY,
            0f
        );
    }
	


}
