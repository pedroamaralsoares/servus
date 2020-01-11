using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class DroneSimpleLight2 : MonoBehaviour
{
    public Transform target;
    public bool tracking;
    public bool killing;

    private DroneSimpleTarget droneControl;

    public Transform wrongSoundPrefab;


    private Vector3 fwd;
    private float currentHitDistance;
    void Start()
    {
        droneControl = transform.parent.GetComponent<DroneSimpleTarget>();
        GetComponent<Light>().intensity = 13.5f;
    }

    public IEnumerator TriggerDeath(Collider collider)
    {
        tracking = false;

        /* Basic death while we don't have a real character. we just disappear */
        /*
        if (target != null && target.tag == "Player") {
            target.localScale = new Vector3(0,0,0);
            target.gameObject.SetActive(false);
        }
        */

        collider.gameObject.GetComponent<CharacterControl>().Dying = true;

        Instantiate(wrongSoundPrefab, transform.position, transform.rotation);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMove>().enabled = false;

        /* Wait 2 seconds until we restart the level */
        yield return new WaitForSeconds(3f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        SmoothLookAt(target);
        //transform.LookAt(target);
        

        fwd = transform.TransformDirection(Vector3.forward);

        if (killing) return;

        RaycastHit hit;

        

        if (Physics.SphereCast(transform.position, 10.5f, fwd, out hit, 17))
        {
            currentHitDistance = hit.distance;

            if (hit.collider != null && hit.collider.tag == "Player")
            {
                StartCoroutine(TriggerDeath(hit.collider));
                killing = true;
                target = hit.collider.transform;
                //transform.parent.GetComponent<Animator>().speed = 0;
            }
        }
    }


    void SmoothLookAt(Transform target)
    {
        Vector3 lTargetDir = new Vector3(target.position.x, 0, target.position.z) - transform.position;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lTargetDir), Time.time * 0.5f);
    }

    private void OnDrawGizmosSelected () {
        Gizmos.color = Color.red;
            Debug.DrawLine(transform.position, fwd * currentHitDistance);
            Gizmos.DrawWireSphere(transform.position + fwd * currentHitDistance,10.5f);
    }

}
