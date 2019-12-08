using UnityEngine;
using System.Collections;

public abstract class NPCStateData : ScriptableObject
{
    public abstract void OnEnter(OtherNPCState npcState, Animator animator, AnimatorStateInfo stateInfo);
    public abstract void OnExit(OtherNPCState npcState, Animator animator, AnimatorStateInfo stateInfo);
    public abstract void UpdateAbility(OtherNPCState npcState, Animator animator, AnimatorStateInfo stateInfo);
}
