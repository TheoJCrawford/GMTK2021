using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerControls))]
public class PlayerControlsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PlayerControls playerControls = (PlayerControls)target;
        GUILayout.BeginHorizontal();
        GUILayout.Label("Jump Limit: ", GUILayout.ExpandWidth(false));
        playerControls.jumpLimit = EditorGUILayout.IntSlider(playerControls.jumpLimit, 1, 10);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Pull Distance: ", GUILayout.ExpandWidth(false));
        playerControls.pullDist = EditorGUILayout.FloatField(playerControls.pullDist);
        GUILayout.EndHorizontal();

    }
}

