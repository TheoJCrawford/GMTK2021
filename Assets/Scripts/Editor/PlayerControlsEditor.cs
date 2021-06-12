using UnityEditor;

[CustomEditor(typeof(PlayerControls))]
public class PlayerControlsEditor:Editor
{


    public override void OnInspectorGUI()
    {
        PlayerControls playerControls = (PlayerControls)target;
        playerControls.jumpLimit = EditorGUILayout.IntSlider(playerControls.jumpLimit, 1, 10);
    }
}

