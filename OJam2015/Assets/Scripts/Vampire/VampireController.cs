using UnityEngine;
using System.Collections;
using InputHandler;

[RequireComponent(typeof(VampireAnimator))]
[RequireComponent(typeof(Vampire))]
public class VampireController : MonoBehaviour
{
    public enum PlayerIndex { Player1, Player2 };

    public PlayerIndex player;

    private VampireAnimator _vampireAnimator;
    private Vampire _vampire;

    private bool _jumpAlreadyPressed = false;

    void Awake()
    {
        _vampireAnimator = GetComponent<VampireAnimator>();
        _vampire = GetComponent<Vampire>();
    }

	// Use this for initialization
	void Start ()
    {
        InputManager.Instance.AddCallback((int)player, HandleVampireActions);
	}

    private void HandleVampireActions(MappedInput input)
    {
        if (this == null) return; //Bad hotfix code, should be fixed

        if (input.Ranges.ContainsKey("MoveHorizontal"))
        {
            float xAxisValue = input.Ranges["MoveHorizontal"];

            _vampireAnimator.Move(xAxisValue);
            _vampire.Move(xAxisValue);
        }

        bool jumpPressed = input.States.Contains("Jump");
        bool isGrounded = _vampire.IsGrounded();

        if (jumpPressed && !_jumpAlreadyPressed && isGrounded)
        {
            _vampire.StartJumping();
        }
        else if (!jumpPressed)
        {
            _vampire.StopJumping();
        }
        
        _jumpAlreadyPressed = jumpPressed;
    }
}
