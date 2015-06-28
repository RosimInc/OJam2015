using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Vampire : MonoBehaviour
{
    public Transform GroundCheck;
    public LayerMask GroundLayer;

    public float Speed = 10f;
    public float MinJumpForce = 700f;
    public float JumpForce = 20f;
    public float JumpMaxDuration = 0.5f;

    private const float GROUNDED_RADIUS = 0.2f;

    private Rigidbody2D _rigidBody2D;
    private bool _jumping = false; 
    private float _jumpDuration = 0f;
    private float _moveValue = 0f;

    void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (_jumping)
        {
            _jumpDuration += Time.deltaTime;

            if (_jumpDuration >= JumpMaxDuration)
            {
                _jumping = false;
            }
        }
    }

    void FixedUpdate()
    {
        _rigidBody2D.velocity = new Vector2(_moveValue * Speed, _rigidBody2D.velocity.y);

        if (_jumping)
        {
            _rigidBody2D.AddForce(new Vector2(0f, JumpForce));
        }

        _moveValue = 0f;
    }

    public bool IsGrounded()
    {
        bool isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GROUNDED_RADIUS, GroundLayer);
        
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                break;
            }

        }

        return isGrounded;
    }

    public void Move(float value)
    {
        _moveValue = value;
    }

    public void StartJumping()
    {
        _jumping = true;
        _rigidBody2D.AddForce(new Vector2(0f, MinJumpForce));
        _jumpDuration = 0f;
    }

    public void StopJumping()
    {
        _jumping = false;
    }
}
