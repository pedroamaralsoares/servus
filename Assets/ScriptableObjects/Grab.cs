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

            c.playerRigPosZ = 0;
            
        }
        else {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (control.DraggableObject)
            {
                control.DraggableObject.GetComponent<NeonCube>().domesticated = true;

                if (!c.Pulling && !c.Pushing) {

                    float distToDraggable = Vector3.Distance(control.playerRig.parent.position,control.DraggableObject.transform.position)
                                        - control.DraggableObject.transform.localScale.x
                                        - 1 /*half size of player */;
                    Debug.Log(distToDraggable);
                    //0.26f
                    c.playerRigPosZ = (distToDraggable+0.2f)/2;
                }
            }

            
        }

        if (c.Pulling)
        {
            animator.SetBool(CharacterControl.TransitionParameter.Pulling.ToString(), true);

            if (c.DraggableObject) {
                float distToDraggable = Vector3.Distance(c.playerRig.parent.position,c.DraggableObject.transform.position)
                                        - c.DraggableObject.transform.localScale.x
                                        - 1 /*half size of player */;
                c.playerRigPosZ = (distToDraggable)/2;
            }
        }
        else
        {
            animator.SetBool(CharacterControl.TransitionParameter.Pulling.ToString(), false);
        }

        if (c.Pushing)
        {
            animator.SetBool(CharacterControl.TransitionParameter.Pushing.ToString(), true);

            if (c.DraggableObject) {
                float distToDraggable = Vector3.Distance(c.playerRig.parent.position,c.DraggableObject.transform.position)
                                    - c.DraggableObject.transform.localScale.x
                                    - 1 /*half size of player */;
                c.playerRigPosZ = (distToDraggable - 0.08f)/2;
            }
            
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
