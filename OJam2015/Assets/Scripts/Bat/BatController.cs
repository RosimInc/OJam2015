using UnityEngine;
using System.Collections;
using InputHandler;

[RequireComponent(typeof(BatAnimator))]
public class BatController : MonoBehaviour {

    public enum PlayerIndex	{Player1, Player2};

    public PlayerIndex player;

    public float maxVelocity;
    private float minVelocity;
    private float velocityX = 0f;
    private float velocityY = 0f;
    public float velocityFactor;
    public float minMovementRange;
    public float minVelocityRange;

    private bool isInInteractionRange;
    private GameObject interactiveElement;

    private float spotlightTime = 0f;

    private Collider2D actionCollider;

    private BatAnimator batAnimator;
    private bool _alreadyGotAchievement = false;


	// Use this for initialization
	void Start () {
        batAnimator = GetComponent<BatAnimator>();

        InputManager.Instance.PushActiveContext("GameplayBat", (int)player);
        InputManager.Instance.AddCallback((int)player, HandleBatActions);

        minVelocity = -1 * maxVelocity;

        actionCollider = GetComponent<Collider2D>();

	}

    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, 8);

        bool containsVampire = false;

        foreach (Collider2D col in colliders)
        {
            if (col.tag == "Vampire")
            {
                containsVampire = true;
                break;
            }
        }

        if (containsVampire)
        {
            spotlightTime += Time.deltaTime;
        }
        else
        {
            spotlightTime = 0f;
        }

        if (spotlightTime >= 15f && !_alreadyGotAchievement)
        {
            BrainCloudManager.Instance.AddAchievement(BrainCloudManager.AchievementTypes.Spotlight);
            _alreadyGotAchievement = true;
        }
    }


    void OnTriggerEnter2D(Collider2D other) {

        if( other.tag == "BatInteraction"){
            isInInteractionRange = true;
            interactiveElement = other.gameObject;
            interactiveElement.GetComponent<Switch>().ShowAction();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "BatInteraction") {
            isInInteractionRange = false;
            interactiveElement.GetComponent<Switch>().HideAction();
            interactiveElement = null;
        }
    }

    private void HandleBatActions(MappedInput input) {
        if (this == null) return; //Bad hotfix code, should be fixed

        Move(input);   

        if (input.Actions.Contains("Action")) {
            
            if (isInInteractionRange && interactiveElement != null) {
                
                interactiveElement.GetComponent<Switch>().Activate();

                batAnimator.Shout();
            }

        }
        
       
        
    }


    private void Move(MappedInput input)
    {
        float rangeX = 0f;

        if (input.Ranges.ContainsKey("MoveLeftBat"))
        {
            rangeX = -input.Ranges["MoveLeftBat"];
        }
        else if (input.Ranges.ContainsKey("MoveRightBat"))
        {
            rangeX = input.Ranges["MoveRightBat"];
        }

        if (rangeX >= minMovementRange || rangeX <= -1 * minMovementRange)
        {

            velocityX = Mathf.Clamp(velocityX + velocityFactor * Time.deltaTime * rangeX, minVelocity, maxVelocity);

        }
        // if the joystick is in the deadzone, slow down the character
        else
        {
            if (velocityX > 0)
            {
                velocityX = Mathf.Clamp(velocityX - velocityFactor * 0.5f * Time.deltaTime, 0f, maxVelocity);
            }
            else
            {
                velocityX = Mathf.Clamp(velocityX + velocityFactor * 0.5f * Time.deltaTime, minVelocity, 0f);
            }

        }

        float rangeY = 0f;

        if (input.Ranges.ContainsKey("MoveDownBat"))
        {
            rangeY = -input.Ranges["MoveDownBat"];
        }
        else if (input.Ranges.ContainsKey("MoveUpBat"))
        {
            rangeY = input.Ranges["MoveUpBat"];
        }

        if (rangeY >= minMovementRange || rangeY <= -1 * minMovementRange)
        {
            velocityY = Mathf.Clamp(velocityY + velocityFactor * Time.deltaTime * rangeY, minVelocity, maxVelocity);
        }
        // if the joystick is in the deadzone, slow down the character
        else
        {
            if (velocityY > 0)
            {
                velocityY = Mathf.Clamp(velocityY - velocityFactor * 0.5f * Time.deltaTime, 0f, maxVelocity);
            }
            else
            {
                velocityY = Mathf.Clamp(velocityY + velocityFactor * 0.5f * Time.deltaTime, minVelocity, 0f);
            }
        }

        transform.position = new Vector3(
            transform.position.x + velocityX * Time.deltaTime * 60,
            transform.position.y + velocityY * Time.deltaTime * 60,
            0f
        );
    }

   

}
