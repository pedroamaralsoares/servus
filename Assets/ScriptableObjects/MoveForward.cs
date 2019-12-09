using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "AbilityData/MoveForward")]
public class MoveForward : StateData
{
    public float Speed;
    public float BlockDistance;
    public AnimationCurve SpeedGraph;

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
    }
    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl c = characterState.GetCharacterControl(animator);

        if (c.Jump)
        {
            animator.SetBool(CharacterControl.TransitionParameter.Jump.ToString(), true);
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

        if ((c.MoveRight && c.MoveLeft) || (!c.MoveRight && !c.MoveLeft))
        {
            animator.SetBool(CharacterControl.TransitionParameter.Move.ToString(), false);
            return;
        }

        if (c.MoveRight)
        {
            c.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            if (!CheckFront(c))
            {
                c.transform.Translate(Vector3.forward * Speed * SpeedGraph.Evaluate(stateInfo.normalizedTime) * Time.deltaTime);
            }
        }

        if (c.MoveLeft)
        {
            c.transform.rotation = Quaternion.Euler(0f, 270f, 0f);
            if (!CheckFront(c))
            {
                c.transform.Translate(Vector3.forward * Speed * SpeedGraph.Evaluate(stateInfo.normalizedTime) * Time.deltaTime);
            }
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    private bool CheckFront(CharacterControl control)
    {
        foreach (GameObject o in control.FrontSpheres)
        {
            RaycastHit hit;
            if (Physics.Raycast(o.transform.position, control.transform.forward, out hit, BlockDistance))
            {
                return true;
            }
        }

        return false;
    }
}
