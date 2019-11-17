using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "AbilityData/GroundDetector")]
public class GroundDetector : StateData
{
    [Range(0.01f, 1f)]
    public float CheckTime;
    public float Distance;

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl control = characterState.GetCharacterControl(animator);

        if (stateInfo.normalizedTime >= CheckTime)
        {
            if (IsGrounded(control))
            {
                animator.SetBool(CharacterControl.TransitionParameter.Grounded.ToString(), true);
            }
            else
            {
                animator.SetBool(CharacterControl.TransitionParameter.Grounded.ToString(), false);
            }
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    private bool IsGrounded(CharacterControl control)
    {
        if (-0.001 < control.RIGID_BODY.velocity.y && control.RIGID_BODY.velocity.y <= 0.0f)
            return true;

        if (Mathf.Abs(control.RIGID_BODY.velocity.y) > 0f)
        {
            foreach (GameObject o in control.BottomSpheres)
            {
                RaycastHit hit;
                Debug.DrawRay(o.transform.position, -Vector3.up * Distance, Color.yellow);
                if (Physics.Raycast(o.transform.position, -Vector3.up, out hit, Distance))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
