using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "AbilityData/Idle")]
public class Idle : StateData
{
    public float BlockDistance;

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetBool(CharacterControl.TransitionParameter.Jump.ToString(), false);
    }
    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl c = characterState.GetCharacterControl(animator);

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
            CheckForDraggable(c);
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

    private void CheckForDraggable(CharacterControl control)
    {
        foreach (GameObject o in control.FrontSpheres.GetRange(5, 5))
        {
            Debug.DrawRay(o.transform.position, o.transform.position * BlockDistance);
            RaycastHit hit;
            if (Physics.Raycast(o.transform.position, control.transform.forward, out hit, BlockDistance))
            {
                if (hit.collider.gameObject.tag == "Draggable")
                {
                    Debug.Log("Tag");
                    control.DraggableObject = hit.collider.gameObject;
                }
                break;
            }
        }
    }
}
