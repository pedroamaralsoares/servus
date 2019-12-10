using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New NPC State", menuName = "AbilityData/Float")]
public class Float : NPCStateData
{
    public float Height;

    public override void OnEnter(OtherNPCState npcState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void UpdateAbility(OtherNPCState npcState, Animator animator, AnimatorStateInfo stateInfo)
    {
        NPCControl c = npcState.GetNPCControl(animator);
        
        if (c.Float)
        {
            Vector3 transformPosition = c.transform.position;
            transformPosition.y = Height;
            c.transform.position = transformPosition;
        }
        else
        {
            animator.SetBool(NPCControl.TransitionParameter.Float.ToString(), false);
        }
    }

    public override void OnExit(OtherNPCState npcState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
