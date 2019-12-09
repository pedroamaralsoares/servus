using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New NPC State", menuName = "AbilityData/Wonder")]
public class Alert : NPCStateData
{
    public float AlertTimer;
    public Transform QuestionMark;

    public override void OnEnter(OtherNPCState npcState, Animator animator, AnimatorStateInfo stateInfo)
    {
        NPCControl c = npcState.GetNPCControl(animator);

        Instantiate(QuestionMark, new Vector3(c.gameObject.transform.position.x, c.gameObject.transform.position.y + 3f, c.gameObject.transform.position.z), QuestionMark.rotation);

        GameObject closestDrone = c.FindClosestDrone();
        if (closestDrone != null)
        {
            closestDrone.GetComponent<DroneNavAgent>().tracking = true;
            closestDrone.GetComponent<DroneNavAgent>().playerTarget++;
        }

        c.LED.material.color = Color.yellow;
        c.LED.material.SetColor("_EmissionColor", Color.yellow);
        AlertTimer = 5.0f;
    }

    public override void UpdateAbility(OtherNPCState npcState, Animator animator, AnimatorStateInfo stateInfo)
    {
        NPCControl c = npcState.GetNPCControl(animator);

        if (c.Wonder)
        {
            AlertTimer -= Time.deltaTime;
            if (AlertTimer <= 0)
            {
                c.LED.material.color = Color.green;
                c.LED.material.SetColor("_EmissionColor", Color.green);

                GameObject closestDrone = c.FindClosestDrone();

                if (closestDrone != null)
                {
                    closestDrone.GetComponent<DroneNavAgent>().playerTarget--;
                }

                animator.SetBool(NPCControl.TransitionParameter.Wonder.ToString(), false);
                c.Wonder = false;
            }
        }
    }

    public override void OnExit(OtherNPCState npcState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
