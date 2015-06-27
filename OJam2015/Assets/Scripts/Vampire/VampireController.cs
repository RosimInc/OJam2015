using UnityEngine;
using System.Collections;
using InputHandler;

public class VampireController : MonoBehaviour {

    public float Speed;

	// Use this for initialization
	void Start () {
        InputManager.Instance.PushActiveContext("Gameplay");
        InputManager.Instance.AddCallback(0, HandleVampireActions);
	}

    private void HandleVampireActions(MappedInput input)
    {
        if (input.Ranges.ContainsKey("MoveHorizontalVampire"))
        {
            float range = input.Ranges["MoveHorizontalVampire"];

            transform.position = new Vector3(transform.position.x + Time.deltaTime * range * Speed, 0f, 0f);
        }
    }


}
