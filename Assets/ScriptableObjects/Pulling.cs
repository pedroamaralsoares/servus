using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "AbilityData/Pulling")]
public class Pulling : StateData
{
    public float Speed;
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl c = characterState.GetCharacterControl(animator);

        if (!c.Grab || !c.CheckForDraggable())
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (control.DraggableObject)
            {
                control.DraggableObject.transform.parent = null;
                control.DraggableObject = null;
            }

            animator.SetBool(CharacterControl.TransitionParameter.Grab.ToString(), false);
        }
        else if (!c.Pulling)
        {
            animator.SetBool(CharacterControl.TransitionParameter.Pulling.ToString(), false);
        }
        else
        {
            c.transform.Translate(-Vector3.forward * Speed * Time.deltaTime);
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
