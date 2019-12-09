using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualInput : MonoBehaviour
{
    private CharacterControl characterControl;

    private void Awake()
    {
        characterControl = this.gameObject.GetComponent<CharacterControl>();
    }

    void Update()
    {
        if (VirtualInputManager.Instance.MoveRight)
        {
            characterControl.MoveRight = true;
        }
        else
        {
            characterControl.MoveRight = false;
        }

        if (VirtualInputManager.Instance.MoveLeft)
        {
            characterControl.MoveLeft = true;
        }
        else
        {
            characterControl.MoveLeft = false;
        }

        if (VirtualInputManager.Instance.Jump)
        {
            characterControl.Jump = true;
        }
        else
        {
            characterControl.Jump = false;
        }

        if (VirtualInputManager.Instance.Grab)
        {
            characterControl.Grab = true;

            if (VirtualInputManager.Instance.MovingObjectLeft)
            {
                if (characterControl.gameObject.transform.rotation == Quaternion.Euler(0, 90f, 0))
                {
                    characterControl.Pulling = true;
                    characterControl.Pushing = false;
                }
                else
                {
                    characterControl.Pushing = true;
                    characterControl.Pulling = false;
                }
            }
            else if (VirtualInputManager.Instance.MovingObjectRight)
            {
                if (characterControl.gameObject.transform.rotation == Quaternion.Euler(0, 90f, 0))
                {
                    characterControl.Pushing = true;
                    characterControl.Pulling = false;
                }
                else
                {
                    characterControl.Pulling = true;
                    characterControl.Pushing = false;
                }
            }
            else
            {
                characterControl.Pushing = false;
                characterControl.Pulling = false;
            }
        }
        else
        {
            characterControl.Grab = false;
        }
    }
}
