using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New NPC State", menuName = "AbilityData/Move")]
public class Walk : NPCStateData
{
    public float Speed;
    public float DestroyRadius;

    public override void OnEnter(OtherNPCState npcState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void UpdateAbility(OtherNPCState npcState, Animator animator, AnimatorStateInfo stateInfo)
    {
        NPCControl c = npcState.GetNPCControl(animator);

        if (c.Move)
        {
            if (Vector3.Distance(c.Waypoint.position, c.transform.position) < DestroyRadius)
            {
                GameObject closestDrone = c.FindClosestDrone();
                if (closestDrone != null)
                {
                    closestDrone.GetComponent<DroneNavAgent>().npcTargets.Remove("npc" + c.ID);
                }

                animator.SetBool(NPCControl.TransitionParameter.Move.ToString(), false);
                c.Move = false;
                Destroy(c.gameObject);
            }
            else
            {
                Vector3 newDirection = Vector3.RotateTowards(c.transform.position, new Vector3(c.Waypoint.position.x, c.transform.position.y, c.Waypoint.position.z), 1f, 0.0f);
                c.transform.LookAt(new Vector3(c.Waypoint.position.x, c.transform.position.y, c.Waypoint.position.z));
                c.transform.position = Vector3.MoveTowards(c.transform.position, c.Waypoint.transform.position, Time.deltaTime * Speed);
            }
        }
        else
        {
            animator.SetBool(NPCControl.TransitionParameter.Move.ToString(), false);
        }
    }

    public override void OnExit(OtherNPCState npcState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
