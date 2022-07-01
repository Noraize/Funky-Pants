using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SFX : MonoBehaviour
{
    public AudioSource Jump;
    public AudioSource OpenDoor;
    public AudioSource CloseDoor;
    public AudioSource Running;
    public AudioSource Rolling;
    public AudioSource Squiggle;
    public void JumpFunction()
    {
        Jump.Play();
    }
    public void OpenDoorFunction()
    {
        OpenDoor.Play();
    }
    public void CloseDoorFunction()
    {
        CloseDoor.Play();

    }
    public void RunningFunction()
    {
        Running.Play();
    }
    public void RollingFunction()
    {
        Rolling.Play();
    }
    public void RunningStopFunction()
    {
        Running.Stop();
    }
    public void RollingStopFunction()
    {
        Rolling.Stop();
    }
    public void SquiggleStart()
    {
        Squiggle.Play();
    }
}
