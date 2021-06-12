using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerControls))]
public class PlayerControlsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PlayerControls playerControls = (PlayerControls)target;
        GUILayout.BeginHorizontal();
        GUILayout.Label("Am I the mc?", GUILayout.ExpandWidth());
        playerControls.isMainChar = GUILayout.Toggle(playerControls.isMainChar, "Am I?");
        GUILayout.EndHorizontal();
        GUILayout.Label("Can Act: " + playerControls.inControl);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Jump Limit: ", GUILayout.ExpandWidth(false));
        playerControls.jumpLimit = EditorGUILayout.IntSlider(playerControls.jumpLimit, 1, 10);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Pull Distance: ", GUILayout.ExpandWidth(false));
        playerControls.pullDist = EditorGUILayout.FloatField(playerControls.pullDist);
        GUILayout.EndHorizontal();
        GUILayout.Label("Tag partner (Only for main): " + playerControls.otherHalf);
        if(GUILayout.Button("Set Ally")){
            EditorGUIUtility.ShowObjectPicker<GameObject>(playerControls.otherHalf, true,"",0);
        }
        if(Event.current.commandName == "ObjectSelectorUpdated")
        {
            playerControls.otherHalf = (GameObject)EditorGUIUtility.GetObjectPickerObject();
        }
    }
}

