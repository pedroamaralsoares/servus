using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New NPC State", menuName = "AbilityData/Move")]
public class Walk : NPCStateData
{
    public float Speed;
    public float DestroyRadius;
    public Transform Waypoint;

    public override void OnEnter(OtherNPCState npcState, Animator animator, AnimatorStateInfo stateInfo)
    {
        NPCControl c = npcState.GetNPCControl(animator);

        GameObject closestDrone = c.FindClosestDrone();
        if (closestDrone != null)
        {
            closestDrone.GetComponent<DroneNavAgent>().tracking = true;
            closestDrone.GetComponent<DroneNavAgent>().npcTargets.Add("npc" + c.ID, c.gameObject.transform);
        }

        Waypoint = GameObject.Find("Waypoint").transform;
    }

    public override void UpdateAbility(OtherNPCState npcState, Animator animator, AnimatorStateInfo stateInfo)
    {
        NPCControl c = npcState.GetNPCControl(animator);

        if (c.Move)
        {
            if (Vector3.Distance(Waypoint.position, c.transform.position) < DestroyRadius)
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
                Vector3 newDirection = Vector3.RotateTowards(c.transform.position, Waypoint.transform.position, 1f, 0.0f);
                c.transform.LookAt(Waypoint.transform.position);
                c.transform.position = Vector3.MoveTowards(c.transform.position, Waypoint.transform.position, Time.deltaTime * Speed);
            }
        }
    }

    public override void OnExit(OtherNPCState npcState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
