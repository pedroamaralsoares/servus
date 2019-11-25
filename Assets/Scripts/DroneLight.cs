using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class DroneLight : MonoBehaviour
{
    public Transform target;
    public bool tracking;
    public bool trackingNPC;

    private DroneNavAgent droneControl;
    void Start()
    {
        droneControl = transform.parent.GetComponent<DroneNavAgent>();
        target = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (tracking && target != null) {
            SmoothLookAt(target);
        }

        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        RaycastHit hit;
        Debug.DrawRay(transform.position,fwd*15, Color.green,2);
        if (Physics.Raycast(transform.position, fwd, out hit, 200))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider != null && hit.collider.tag == "Player")
            {
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                Debug.Log("DIE");
            }
            //else if (hit.collider != null && trackingNPC)
            //{
            //    this.gameObject.GetComponent<Light>().enabled = false;
            //    StartCoroutine(CheckNPC());
            //}
        }
    }

    //public IEnumerator CheckNPC()
    //{
    //    Debug.Log("YEEET");
    //    yield return new WaitForSeconds(3f);

    //    // play animation of light
    //    this.gameObject.GetComponent<Light>().enabled = true;
    //    // check!
    //    droneControl.prios.Pop();
    //}

    void SmoothLookAt (Transform target) {
        Vector3 lTargetDir = target.position - transform.position;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lTargetDir), Time.time * 1.5f);
    }
}
