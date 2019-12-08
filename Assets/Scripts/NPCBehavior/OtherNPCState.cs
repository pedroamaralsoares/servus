using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OtherNPCState : StateMachineBehaviour
{
    private NPCControl npcControl;

    public List<NPCStateData> ListAbilityData = new List<NPCStateData>();

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (NPCStateData d in ListAbilityData)
            d.OnEnter(this, animator, stateInfo);
    }
    public void UpdateAll(OtherNPCState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        foreach (NPCStateData d in ListAbilityData)
            d.UpdateAbility(characterState, animator, stateInfo);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        UpdateAll(this, animator, stateInfo);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (NPCStateData d in ListAbilityData)
            d.OnExit(this, animator, stateInfo);
    }
    public NPCControl GetNPCControl(Animator animator)
    {
        if (npcControl == null)
        {
            npcControl = animator.GetComponentInParent<NPCControl>();
        }
        return npcControl;
    }
}
