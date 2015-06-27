using UnityEngine;
using System.Collections;
using InputHandler;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(VampireAnimator))]
public class VampireController : MonoBehaviour
{
    public float Speed;

    private Rigidbody2D _rigidBody2D;
    private VampireAnimator _vampireAnimator;

    private float _xAxisValue;

    void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _vampireAnimator = GetComponent<VampireAnimator>();
    }

	// Use this for initialization
	void Start ()
    {
        InputManager.Instance.PushActiveContext("Gameplay");
        InputManager.Instance.AddCallback(0, HandleVampireActions);
	}

    void FixedUpdate()
    {
        _rigidBody2D.velocity = new Vector2(_xAxisValue * Speed, _rigidBody2D.velocity.y);
    }

    private void HandleVampireActions(MappedInput input)
    {
        if (input.Ranges.ContainsKey("MoveHorizontalVampire"))
        {
            _xAxisValue = input.Ranges["MoveHorizontalVampire"];
            _vampireAnimator.Move(_xAxisValue);
        }
    }
}
