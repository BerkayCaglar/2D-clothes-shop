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
        if (!Player.Instance.isOnDialogue && !Player.Instance.isOnFade && !Player.Instance.isOnInventory && !Player.Instance.isOnShopInventory)
        {
            RigidBody.velocity = GetInputAndDirection() * Speed;
        }
        else
        {
            RigidBody.velocity = Vector2.zero;
        }
    }
    private Vector2 GetInputAndDirection()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        return new Vector2(horizontal, vertical);
    }
}