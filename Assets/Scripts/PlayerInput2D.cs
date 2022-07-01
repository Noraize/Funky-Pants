using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput2D : MonoBehaviour
{
    private PlayerPhysics2D physics;
    [SerializeField] GameObject Scene;

    private void Awake()
    {
        physics = GetComponent<PlayerPhysics2D>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Scene.GetComponent<SFX>().JumpFunction();
            physics.JumpEnd();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            physics.JumpStart();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            physics.SetMovementDirection(1);
            //Scene.GetComponent<SFX>().RunningFunction();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            physics.SetMovementDirection(-1);
            //Scene.GetComponent<SFX>().RunningFunction();
        }
        if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            physics.SetMovementDirection(0);
            //Scene.GetComponent<SFX>().RunningStopFunction();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            physics.SlidingStart();
            if (physics.velocityx() > 5)
            {
                Scene.GetComponent<SFX>().RollingFunction();
            }
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            physics.SlidingEnd();
            Scene.GetComponent<SFX>().RollingStopFunction();
        }
    }
}

