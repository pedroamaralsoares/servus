using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "AbilityData/Grab")]
public class Grab : StateData
{

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl c = characterState.GetCharacterControl(animator);
        
        if (!c.Grab)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (control.DraggableObject)
            {
                control.DraggableObject.transform.parent = null;
                control.DraggableObject = null;
            }

            animator.SetBool(CharacterControl.TransitionParameter.Grab.ToString(), false);
        }
        else {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (control.DraggableObject)
            {
                control.DraggableObject.GetComponent<NeonCube>().domesticated = true;
            }
        }

        if (c.Pulling)
        {
            animator.SetBool(CharacterControl.TransitionParameter.Pulling.ToString(), true);
        }
        else
        {
            animator.SetBool(CharacterControl.TransitionParameter.Pulling.ToString(), false);
        }

        if (c.Pushing)
        {
            animator.SetBool(CharacterControl.TransitionParameter.Pushing.ToString(), true);
        }
        else
        {
            animator.SetBool(CharacterControl.TransitionParameter.Pushing.ToString(), false);
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
