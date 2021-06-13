using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(PlayerMotor))]
public class PlayerMotorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PlayerMotor motor = (PlayerMotor)target;
        GUILayout.BeginHorizontal();
        GUILayout.Label("move speed: ", GUILayout.ExpandWidth(false));
        motor._moveSpeed = EditorGUILayout.FloatField(motor._moveSpeed);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Jump speed: ", GUILayout.ExpandWidth(false));
        motor._jumpSpeed = EditorGUILayout.FloatField(motor._jumpSpeed);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Knockback Force: ", GUILayout.ExpandWidth(false));
        motor.knockbackForce = EditorGUILayout.FloatField(motor.knockbackForce);
        GUILayout.EndHorizontal();
    }

}
