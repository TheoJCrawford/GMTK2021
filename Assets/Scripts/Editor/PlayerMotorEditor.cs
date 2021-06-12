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
        GUILayout.Label("move speed: ");
        motor._moveSpeed = EditorGUILayout.FloatField(motor._moveSpeed);
        motor._moveSpeed = GUILayout.HorizontalSlider(motor._moveSpeed, 0f, 36f);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("move speed: ");
        motor._jumpSpeed = EditorGUILayout.FloatField(motor._jumpSpeed);
        motor._jumpSpeed = GUILayout.HorizontalSlider(motor._jumpSpeed, 0f, 20f);
        GUILayout.EndHorizontal();
    }

}
