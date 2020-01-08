using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovementPuzzle1 : MonoBehaviour
{
    
    public Vector3 posStation;
    public Vector3 posInitial;
    public Vector3 posFinal;

    public bool inStation;

    void Start()
    {
        startAgain();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toPosition;
        if (inStation) {
            toPosition = posStation;
        }
        else {
            toPosition = posFinal;
        }

        transform.position = Vector3.Lerp(transform.position, toPosition, 2 * Time.deltaTime);


    }

    public void startAgain() {
        transform.position = posInitial;
        inStation = true;
    }

    public void endAgain() {
        inStation = false;
        StartCoroutine(WaitToStart());
    }

    private IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(1.5f);
        startAgain();
    }
}
