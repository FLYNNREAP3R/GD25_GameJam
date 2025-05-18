using UnityEditor;
using UnityEngine;

public class WaveEditorWindow : EditorWindow
{
    WaveSO currentWave;

    Vector2 scrollPos;

    [MenuItem("TD Tools/Wave Editor")]
    public static void OpenWindow()
    {
        GetWindow<WaveEditorWindow>("Wave Editor");
    }

    void OnGUI()
    {
        EditorGUILayout.Space();
        currentWave = (WaveSO)EditorGUILayout.ObjectField("Wave to Edit", currentWave, typeof(WaveSO), false);

        if (currentWave == null)
        {
            EditorGUILayout.HelpBox("Selecciona una WaveSO para editar.", MessageType.Info);
            return;
        }

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Enemies in Wave", EditorStyles.boldLabel);

        SerializedObject so = new SerializedObject(currentWave);
        SerializedProperty enemiesProp = so.FindProperty("enemies");

        for (int i = 0; i < enemiesProp.arraySize; i++)
        {
            var element = enemiesProp.GetArrayElementAtIndex(i);
            var enemyType = element.FindPropertyRelative("enemyType");
            var count = element.FindPropertyRelative("count");
            var delay = element.FindPropertyRelative("delayBetweenSpawns");

            EditorGUILayout.BeginVertical("box");

            string label = "Enemy " + (i + 1);
            var enemySO = enemyType.objectReferenceValue as EnemyTypeSO;
            if (enemySO != null && !string.IsNullOrEmpty(enemySO.enemyName))
            {
                label = enemySO.enemyName + $" (x{count.intValue})";
            }

            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(enemyType);
            EditorGUILayout.PropertyField(count);
            EditorGUILayout.PropertyField(delay);

            if (GUILayout.Button("Remove", GUILayout.Width(70)))
            {
                enemiesProp.DeleteArrayElementAtIndex(i);
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        if (GUILayout.Button("Add New Enemy"))
        {
            enemiesProp.InsertArrayElementAtIndex(enemiesProp.arraySize);
        }

        so.ApplyModifiedProperties();

        EditorGUILayout.EndScrollView();
        EditorUtility.SetDirty(currentWave);
        AssetDatabase.SaveAssets();

    }
}
