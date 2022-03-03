using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelProfile))]
public class LevelProfileInspector : Editor
{
    SerializedProperty levelId, modules;

    private void OnEnable()
    {
        levelId = serializedObject.FindProperty("levelID");
        modules = serializedObject.FindProperty("modules");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (GUILayout.Button("Open Window")) OpenWindow();
        EditorGUILayout.PropertyField(levelId);
        EditorGUILayout.PropertyField(modules);

        serializedObject.ApplyModifiedProperties();
    }

    private void OpenWindow()
    {
        LdGenerator myWindow = EditorWindow.GetWindow(typeof(LdGenerator)) as LdGenerator;

        myWindow.InitializeWindow(target as LevelProfile); //fonction a créé
        myWindow.Show(); //fonction de base d'unity
    }
}
