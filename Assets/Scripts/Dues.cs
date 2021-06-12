using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dues
{
    public static void ChangeController()
    {
     GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().ControlShifting();
    }
    public static float PlayerDistance(Vector3 pos1, Vector3 pos2) => Vector3.Distance(pos1, pos2);
    
}
