using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CharacterStats))]
public class PlayerCharacterStatsEditor:Editor
    {
    public override void OnInspectorGUI()
    {
        CharacterStats stats = (CharacterStats)target;
        GUILayout.Label("Health: " + stats.curHealth.ToString() + "/ " + stats.maxHealth.ToString());
        stats.SetMaxHealth(EditorGUILayout.IntField(stats.maxHealth));
    }
}

