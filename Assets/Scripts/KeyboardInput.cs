using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            VirtualInputManager.Instance.MoveRight = true;
        }   
        else
        {
            VirtualInputManager.Instance.MoveRight = false;
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            VirtualInputManager.Instance.MoveLeft = true;
        }
        else
        {
            VirtualInputManager.Instance.MoveLeft = false;
        }

        if (Input.GetButton("Jump"))
        {
            VirtualInputManager.Instance.Jump = true;
        }
        else
        {
            VirtualInputManager.Instance.Jump = false;
        }

        if (Input.GetKey(KeyCode.W))
        {
            VirtualInputManager.Instance.Grab = true;

            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                VirtualInputManager.Instance.MovingObjectRight = true;
                VirtualInputManager.Instance.MovingObjectLeft = false;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                VirtualInputManager.Instance.MovingObjectLeft = true;
                VirtualInputManager.Instance.MovingObjectRight = false;
            }
            else
            {
                VirtualInputManager.Instance.MovingObjectLeft = false;
                VirtualInputManager.Instance.MovingObjectRight = false;
            }
        }
        else
        {
            VirtualInputManager.Instance.Grab = false;
        }
    }
}
