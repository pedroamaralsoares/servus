using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New NPC State", menuName = "AbilityData/NPCIdle")]
public class NPCIdle : NPCStateData
{
    public override void OnEnter(OtherNPCState npcState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void UpdateAbility(OtherNPCState npcState, Animator animator, AnimatorStateInfo stateInfo)
    {
        NPCControl c = npcState.GetNPCControl(animator);

        if (c.Move)
        {
            animator.SetBool(NPCControl.TransitionParameter.Move.ToString(), true);
        }

        if (c.Wonder)
        {
            animator.SetBool(NPCControl.TransitionParameter.Wonder.ToString(), true);
        }
    }

    public override void OnExit(OtherNPCState npcState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
