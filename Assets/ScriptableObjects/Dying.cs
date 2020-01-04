using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "AbilityData/Dying")]
public class Dying : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetBool(CharacterControl.TransitionParameter.Jump.ToString(), false);
        animator.SetBool(CharacterControl.TransitionParameter.Move.ToString(), false);
        animator.SetBool(CharacterControl.TransitionParameter.Grab.ToString(), false);
        animator.SetBool(CharacterControl.TransitionParameter.Pushing.ToString(), false);
        animator.SetBool(CharacterControl.TransitionParameter.Pulling.ToString(), false);

        animator.SetBool(CharacterControl.TransitionParameter.Dying.ToString(), true);
    }
    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
    }
}
