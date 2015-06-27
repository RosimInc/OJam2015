﻿using UnityEngine;
using System.Collections;
using InputHandler;

[RequireComponent(typeof(VampireAnimator))]
[RequireComponent(typeof(Vampire))]
public class VampireController : MonoBehaviour
{
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
        InputManager.Instance.PushActiveContext("Gameplay");
        InputManager.Instance.AddCallback(0, HandleVampireActions);
	}

    private void HandleVampireActions(MappedInput input)
    {
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