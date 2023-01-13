using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnimationController : MonoBehaviour
{
    public static AnimationController Instance;
    private AnimatorOverrideController overrideController;
    [Tooltip("The animator component")]
    private static Animator[] animators;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        animators = GetComponentsInChildren<Animator>();
    }
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
                SetAnimations(4);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                SetAnimations(3);
            }
            else if (Input.GetAxis("Vertical") > 0)
            {
                SetAnimations(1);
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                SetAnimations(2);
            }
        }
        else
        {
            SetAnimations(0);
        }
    }
    private void SetAnimations(float blend)
    {
        foreach (Animator animator in animators)
        {
            if (animator.runtimeAnimatorController != null)
            {
                animator.SetFloat("Blend", blend);
            }
        }
    }
    public void SetRightAnimatorForClothes(RuntimeAnimatorController overrideAnimator, string type, bool isWearing)
    {
        if (isWearing)
        {
            foreach (Animator animator in animators)
            {
                if (animator.CompareTag("Sockets/Head Socket") && type == "Head")
                {
                    SetAnimatiorForClothes(overrideAnimator, animator);
                    break;
                }
                else if (animator.CompareTag("Sockets/Body Socket") && type == "Body")
                {
                    SetAnimatiorForClothes(overrideAnimator, animator);
                    break;
                }
                else if (animator.CompareTag("Sockets/Feet Socket") && type == "Feet")
                {
                    SetAnimatiorForClothes(overrideAnimator, animator);
                    break;
                }
            }
        }
        else
        {
            foreach (Animator animator in animators)
            {
                if (animator.CompareTag("Sockets/Head Socket") && type == "Head")
                {
                    animator.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                    break;
                }
                else if (animator.CompareTag("Sockets/Body Socket") && type == "Body")
                {
                    animator.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                    break;
                }
                else if (animator.CompareTag("Sockets/Feet Socket") && type == "Feet")
                {
                    animator.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                    break;
                }
            }
        }

    }
    private void SetAnimatiorForClothes(RuntimeAnimatorController overrideAnimator, Animator animator)
    {
        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;

        animator.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        overrideController[animator.runtimeAnimatorController.animationClips[0].name] = overrideAnimator.animationClips[0];
        overrideController[animator.runtimeAnimatorController.animationClips[1].name] = overrideAnimator.animationClips[1];
        overrideController[animator.runtimeAnimatorController.animationClips[2].name] = overrideAnimator.animationClips[2];
        overrideController[animator.runtimeAnimatorController.animationClips[3].name] = overrideAnimator.animationClips[3];
        overrideController[animator.runtimeAnimatorController.animationClips[4].name] = overrideAnimator.animationClips[4];
    }
}
