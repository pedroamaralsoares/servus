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

        transform.position = Vector3.Lerp(transform.position, toPosition, 3 * Time.deltaTime);

        if(Input.GetKeyDown("i")) {
            startAgain();
        }
        if(Input.GetKeyDown("o")) {
            endAgain();
        }

    }

    public void startAgain() {
        transform.position = posInitial;
        inStation = true;
    }

    public void endAgain() {
        inStation = false;
    }
}
