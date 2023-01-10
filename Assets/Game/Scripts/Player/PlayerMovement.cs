using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("The speed of the player")]
    [SerializeField]
    private float Speed = 5f;
    [Tooltip("The rigidbody of the player")]
    [SerializeField]
    private Rigidbody2D RigidBody;
    void Update()
    {
        Move();
    }

    private void Move()
    {
        RigidBody.velocity = GetInputAndDirection() * Speed;
    }
    private Vector2 GetInputAndDirection()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        return new Vector2(horizontal, vertical);
    }
}
