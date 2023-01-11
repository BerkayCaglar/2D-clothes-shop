using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [Tooltip("The animator component")]
    [SerializeField]
    private Animator animator;
    void Update()
    {
        UpdateAnimation();
    }
    private void UpdateAnimation()
    {
        AnimateMovement();
    }
    private void AnimateMovement()
    {
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && !Player.Instance.isOnDialogue && !Player.Instance.isOnFade && !Player.Instance.isOnInventory && !Player.Instance.isOnShopInventory)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                animator.SetFloat("Blend", 4);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                animator.SetFloat("Blend", 3);
            }
            else if (Input.GetAxis("Vertical") > 0)
            {
                animator.SetFloat("Blend", 1);
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                animator.SetFloat("Blend", 2);
            }
        }
        else
        {
            animator.SetFloat("Blend", 0);
        }
    }
}
