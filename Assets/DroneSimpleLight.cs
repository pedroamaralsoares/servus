using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class DroneSimpleLight : MonoBehaviour
{
    public Transform target;
    public bool tracking;
    public bool killing;

    private DroneSimpleTarget droneControl;

    public Transform wrongSoundPrefab;
    void Start()
    {
        droneControl = transform.parent.GetComponent<DroneSimpleTarget>();
        target = this.transform;
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
        if (tracking && target != null)
        {
            SmoothLookAt(target);
        }

        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (killing) return;

        RaycastHit hit;

        if (Physics.SphereCast(transform.position, 3f, fwd, out hit, 17))
        {
            if (hit.collider != null && hit.collider.tag == "Player")
            {
                StartCoroutine(TriggerDeath(hit.collider));
                killing = true;
            }
        }
    }


    void SmoothLookAt(Transform target)
    {
        Vector3 lTargetDir = new Vector3(target.position.x, 0, target.position.z) - transform.position;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lTargetDir), Time.time * 0.5f);
    }
}
