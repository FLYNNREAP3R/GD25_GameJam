using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaveSO))]
public class WaveEditor : Editor
{
    SerializedProperty enemiesProp;

    void OnEnable()
    {
        enemiesProp = serializedObject.FindProperty("enemies");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Wave Configuration", EditorStyles.boldLabel);

        for (int i = 0; i < enemiesProp.arraySize; i++)
        {
            var element = enemiesProp.GetArrayElementAtIndex(i);
            var enemyType = element.FindPropertyRelative("enemyType");
            var count = element.FindPropertyRelative("count");
            var delay = element.FindPropertyRelative("delayBetweenSpawns");

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.PropertyField(enemyType, new GUIContent("Enemy Type"));
            EditorGUILayout.PropertyField(count, new GUIContent("Count"));
            EditorGUILayout.PropertyField(delay, new GUIContent("Delay"));
            if (GUILayout.Button("Remove"))
            {
                enemiesProp.DeleteArrayElementAtIndex(i);
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        if (GUILayout.Button("Add Enemy Spawn"))
        {
            enemiesProp.InsertArrayElementAtIndex(enemiesProp.arraySize);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
