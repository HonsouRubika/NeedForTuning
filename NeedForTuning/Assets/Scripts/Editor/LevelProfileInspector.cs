using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelProfile))]
public class LevelProfileInspector : Editor
{
    SerializedProperty levelId, modules, sols, nbOfLine;

    private void OnEnable()
    {
        levelId = serializedObject.FindProperty("levelID");
        nbOfLine = serializedObject.FindProperty("nbOfLine");
        modules = serializedObject.FindProperty("modules");
        sols = serializedObject.FindProperty("sols");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (GUILayout.Button("Open Window")) OpenWindow();
        EditorGUILayout.PropertyField(levelId);
        EditorGUILayout.PropertyField(nbOfLine);
        EditorGUILayout.PropertyField(modules);
        EditorGUILayout.PropertyField(sols);
        

        serializedObject.ApplyModifiedProperties();
    }

    private void OpenWindow()
    {
        LdGenerator myWindow = EditorWindow.GetWindow(typeof(LdGenerator)) as LdGenerator;

        myWindow.InitializeWindow(target as LevelProfile); //fonction a créé
        myWindow.Show(); //fonction de base d'unity
    }
}
