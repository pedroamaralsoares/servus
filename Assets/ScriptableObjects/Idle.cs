using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "AbilityData/Idle")]
public class Idle : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetBool(CharacterControl.TransitionParameter.Jump.ToString(), false);
    }
    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl c = characterState.GetCharacterControl(animator);

        c.playerRigPosZ = 0;

        if (c.Jump)
        {
            animator.SetBool(CharacterControl.TransitionParameter.Jump.ToString(), true);
        }

        if (c.MoveRight)
        {
            animator.SetBool(CharacterControl.TransitionParameter.Move.ToString(), true);
        }

        if (c.MoveLeft)
        {
            animator.SetBool(CharacterControl.TransitionParameter.Move.ToString(), true);
        }

        if (c.Grab)
        {
            c.CheckForDraggable();
            if (c.DraggableObject)
            {
                animator.SetBool(CharacterControl.TransitionParameter.Grab.ToString(), true);
                c.DraggableObject.transform.parent = c.transform;
            }
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
