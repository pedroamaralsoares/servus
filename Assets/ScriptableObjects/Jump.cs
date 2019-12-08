using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "AbilityData/Jump")]
public class Jump : StateData
{
    public float JumpForce;
    public AnimationCurve Gravity;
    public AnimationCurve Pull;

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        Debug.Log(JumpForce);
        characterState.GetCharacterControl(animator).RIGID_BODY.AddForce(Vector3.up * JumpForce);
        animator.SetBool(CharacterControl.TransitionParameter.Grounded.ToString(), false);
    }
    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl control = characterState.GetCharacterControl(animator);

        control.GravityMultiplier = Gravity.Evaluate(stateInfo.normalizedTime);
        control.PullMultiplier = Pull.Evaluate(stateInfo.normalizedTime);
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
