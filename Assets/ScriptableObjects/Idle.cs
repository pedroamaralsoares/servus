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
            if (c.DraggableObject) 
                c.DraggableObject.transform.parent = null;
            animator.SetBool(CharacterControl.TransitionParameter.Move.ToString(), true);
        }

        if (c.MoveLeft)
        {
            if (c.DraggableObject)
                c.DraggableObject.transform.parent = null;
            animator.SetBool(CharacterControl.TransitionParameter.Move.ToString(), true);
        }

        if (c.Grab && !c.MoveLeft && !c.MoveRight)
        {
            c.CheckForDraggable();
            if (c.DraggableObject)
            {
                animator.SetBool(CharacterControl.TransitionParameter.Grab.ToString(), true);
            }
        }

        if (c.Dying) {
            animator.SetBool(CharacterControl.TransitionParameter.Dying.ToString(), true);
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
