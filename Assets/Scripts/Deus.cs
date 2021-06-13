using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deus:MonoBehaviour
{
    CameraControls camController;
    public bool spiritLocked;
    private void Start()
    {
        camController = Camera.main.GetComponent<CameraControls>();
    }

    public void ChangeController()
    {
        if (GameObject.FindGameObjectWithTag("Player") && GameObject.FindGameObjectWithTag("Spirit"))
        {
            camController.ChangeTarget();
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().ControlShifting();
            GameObject.FindGameObjectWithTag("Spirit").GetComponent<PlayerControls>().ControlShifting();
        }
        else
        {
            camController.ChangeTarget();
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().ControlShifting();
        }
    }
}
