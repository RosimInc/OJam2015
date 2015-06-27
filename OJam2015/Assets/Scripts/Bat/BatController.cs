using UnityEngine;
using System.Collections;
using InputHandler;

[RequireComponent(typeof(BatAnimator))]
public class BatController : MonoBehaviour {

    public float maxVelocity;
    private float minVelocity;
    private float velocityX = 0f;
    private float velocityY = 0f;
    public float velocityFactor;
    public float minMovementRange;
    public float minVelocityRange;

    public bool isInInteractionRange;
    public GameObject interactiveElement;

    private Collider2D actionCollider;

    private BatAnimator batAnimator; 


	// Use this for initialization
	void Start () {
        batAnimator = GetComponent<BatAnimator>();
        
        InputManager.Instance.PushActiveContext("Gameplay");
        InputManager.Instance.AddCallback(0, HandleBatActions);

        minVelocity = -1 * maxVelocity;

        actionCollider = GetComponent<Collider2D>();

	}


    void OnTriggerEnter2D(Collider2D other) {

        if( other.tag == "BatInteraction"){
            isInInteractionRange = true;
            interactiveElement = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "BatInteraction") {
            isInInteractionRange = false;
            interactiveElement = null;
        }
    }

    private void HandleBatActions(MappedInput input) {

        Move(input);        
        

        if (input.Actions.Contains("Action")) {
            Debug.Log(isInInteractionRange + ", " + interactiveElement);
            if (isInInteractionRange && interactiveElement != null) {
                
                //interactiveElement.activate();

                batAnimator.Shout();
            }

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
