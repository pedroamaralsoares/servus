using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class DroneLight : MonoBehaviour
{
    public Transform target;
    public bool tracking;
    public bool killing;
    public bool trackingNPC;

    private DroneNavAgent droneControl;

    public Transform wrongSoundPrefab;
    void Start()
    {
        droneControl = transform.parent.GetComponent<DroneNavAgent>();
        target = this.transform;
    }

    public IEnumerator TriggerDeath()
    {
        tracking = false;

        /* Basic death while we don't have a real character. we just disappear */
        /*
        if (target != null && target.tag == "Player") {
            target.localScale = new Vector3(0,0,0);
            target.gameObject.SetActive(false);
        }
        */

        target.GetComponent<CharacterControl>().Dying = true;

        Instantiate(wrongSoundPrefab,transform.position,transform.rotation);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMove>().enabled = false;

        /* Wait 2 seconds until we restart the level */
        yield return new WaitForSeconds (3f);
        LevelDebugManager levelDebugManager = GameObject.Find("LevelDebugManager").transform.GetComponent<LevelDebugManager>();
        levelDebugManager.Death();
    }

    void Update()
    {
        if (tracking && target != null) {
            SmoothLookAt(target);
        }
        
        if (transform.parent.GetComponent<DroneNavAgent>().tracking) {
            GetComponent<Light>().intensity = 13.5f;
        }
        else {
            GetComponent<Light>().intensity = 0;
        }

        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (killing) return;

        RaycastHit hit;
        Debug.DrawRay(transform.position,fwd*15, Color.green,2);
        if (Physics.Raycast(transform.position, fwd, out hit, 200))
        {
            if (hit.collider != null && hit.collider.tag == "Player")
            {
                StartCoroutine(TriggerDeath());
                killing = true;
                
            }
            
        }
    }


    void SmoothLookAt (Transform target) {
        Vector3 lTargetDir = target.position - transform.position;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lTargetDir), Time.time * 0.5f);
    }
}
