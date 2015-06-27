using UnityEngine;
using System.Collections;
using InputHandler;

public class BatController : MonoBehaviour {

    public float MaxSpeed;
    public float MaxVelocity;
    private float Speed;
    private float Velocity;

    private bool isRadarActive = false;

	// Use this for initialization
	void Start () {
        InputManager.Instance.PushActiveContext("Gameplay");
        InputManager.Instance.AddCallback(0, HandleBatActions);
	}

    private void HandleBatActions(MappedInput input) {

        if (input.Ranges.ContainsKey("MoveHorizontal")) {
            float range = input.Ranges["MoveHorizontal"];

            transform.position = new Vector3(
                transform.position.x + Time.deltaTime * range * MaxSpeed,
                transform.position.y,
                0f
            );
        }

        if (input.Ranges.ContainsKey("MoveVertical")) {
            float range = input.Ranges["MoveVertical"];

            transform.position = new Vector3(
                transform.position.x,
                transform.position.y + Time.deltaTime * range * MaxSpeed,
                0f
            );

            
        }

        
        


        
    }
	
}
