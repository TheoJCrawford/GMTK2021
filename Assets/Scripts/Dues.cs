using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dues:MonoBehaviour
{
    public void ChangeController()
    {
        if(GameObject.FindGameObjectWithTag("Player") && GameObject.FindGameObjectWithTag("Spirit"))
        {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().ControlShifting();
                GameObject.FindGameObjectWithTag("Spirit").GetComponent<PlayerControls>().ControlShifting();
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().ControlShifting();
        }
    }
}
