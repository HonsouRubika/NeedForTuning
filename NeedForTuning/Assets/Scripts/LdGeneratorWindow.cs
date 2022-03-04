using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LdGenerator : EditorWindow
{
    SerializedObject serializedObject;
    LevelProfile currentLevel;

    SerializedProperty nbOfLine, levelType, chunks, modules, currentChunkSelected;

    private bool isMouseDown;
    private int marginSize;
    private Vector2 curMousePosition;
    private int tileActu = 0;
    private Vector2 scrollPos;

    public void InitializeWindow(LevelProfile correspondingLevel)
    {
        currentLevel = correspondingLevel;
        serializedObject = new SerializedObject(currentLevel);

        nbOfLine = serializedObject.FindProperty("nbOfLine");
        levelType = serializedObject.FindProperty("levelType");
        chunks = serializedObject.FindProperty("chunks");
        modules = serializedObject.FindProperty("modules");
        currentChunkSelected = serializedObject.FindProperty("currentChunkSelected");

        marginSize = 32;
        isMouseDown = false;
        curMousePosition = Vector2.zero;
    }

    private void OnGUI()
    {
        serializedObject.Update();

        ProcessEvents();

        //affichage de l'objet level actuel
        EditorGUI.BeginDisabledGroup(true);
        EditorGUI.EndDisabledGroup();

        //
        EditorGUILayout.Space(5);

        #region Chunk Selector

        currentLevel.nbOfLine = EditorGUILayout.IntField("nbOfLine", currentLevel.nbOfLine);

        //EditorGUILayout.Space(10);

        EditorGUILayout.PropertyField(levelType);

        EditorGUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Current Brush : " + currentChunkSelected.enumValueIndex + " => " + currentChunkSelected.enumNames[currentChunkSelected.enumValueIndex]);


        //changer de couleur
        if (GUILayout.Button("<<"))
        {
            //Debug.Log("Changer chunk left");
            if (currentChunkSelected.enumValueIndex != 0) currentChunkSelected.enumValueIndex--;
            else currentChunkSelected.enumValueIndex = (int)ChunkManager.Modules.total - 1;
        }
        if (GUILayout.Button(">>"))
        {
            //Debug.Log("Changer chunk right");
            if (currentChunkSelected.enumValueIndex != (int)ChunkManager.Modules.total - 1) currentChunkSelected.enumValueIndex++;
            else currentChunkSelected.enumValueIndex = 0;
        }

        EditorGUILayout.Space(10);

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(5);

        /*EditorGUILayout.BeginHorizontal();
        
        EditorGUILayout.LabelField("Current road type : " + levelType.enumValueIndex + " => " + levelType.enumNames[levelType.enumValueIndex]);

        
        //changer de couleur
        if (GUILayout.Button("<<"))
        {
            if (levelType.enumValueIndex != 0) levelType.enumValueIndex--;
            else levelType.enumValueIndex = (int)ChunkManager.RoadType.total - 1;
        }
        if (GUILayout.Button(">>"))
        {
            if (levelType.enumValueIndex != (int)ChunkManager.RoadType.total - 1) levelType.enumValueIndex++;
            else levelType.enumValueIndex = 0;
        }

        EditorGUILayout.Space(10);

        EditorGUILayout.EndHorizontal();
        */

        EditorGUILayout.Space(5);

        EditorGUILayout.PropertyField(modules);

        if (GUILayout.Button("Clear Grid"))
        {
            if (EditorUtility.DisplayDialog("Attention", "Etes vous sur de vouloir vider la grille ?", "Oui", "Non"))
            {
                chunks.ClearArray();
                chunks.arraySize = 3 * currentLevel.nbOfLine;
            }

        }

        serializedObject.ApplyModifiedProperties();

        EditorUtility.SetDirty(currentLevel);

        if (currentLevel.nbOfLine < 0) return;

        #endregion

        #region Level view

        float marginRatio = 0.2f;
        float totalWidth = EditorGUIUtility.currentViewWidth;
        float gridWidth = totalWidth * (1f - 2f * marginRatio);
        float gridHeigh = gridWidth + ((nbOfLine.intValue - 3) * (gridWidth / nbOfLine.intValue)); //ref => r1

        EditorGUILayout.Space();
        Rect nextRect = EditorGUILayout.GetControlRect();
        if (nextRect.y == 0) return;

        ///le css c est de la merde
        Rect gridArea = new Rect(nextRect.x + totalWidth * marginRatio, nextRect.y, gridWidth, gridWidth * ((float)nbOfLine.intValue / (float)3)); //ref => r1
        EditorGUI.DrawRect(gridArea, new Color(0, 1, 1, 0.3f));


        // affichage des chunks
        float maxHeightAvailable = position.height - nextRect.y - (2 * EditorGUIUtility.singleLineHeight);
        float visibleAreaHeight = Mathf.Min(gridArea.height, maxHeightAvailable);

        Rect visibleArea = new Rect(gridArea.x, nextRect.y, gridArea.width + 15, visibleAreaHeight);

        scrollPos = GUI.BeginScrollView(visibleArea, scrollPos, gridArea);
        {
            float cellToSpaceRatio = 4f;
            float totalCellWidth = gridWidth * (cellToSpaceRatio) / (cellToSpaceRatio + 1);
            float cellWidth = totalCellWidth / 3f;
            float totalSpaceWidth = gridWidth - totalCellWidth;
            float spaceWidth = totalSpaceWidth / (3f + 1f);

            Event e = Event.current;
            bool isClick = e.type == EventType.MouseDown;

            float curY = gridArea.y + spaceWidth;
            for (int i = 0; i < currentLevel.nbOfLine; i++)
            {
                float curX = gridArea.x;

                //EditorGUILayout.BeginHorizontal();

                for (int j = 0; j < 3; j++)
                {
                    curX += spaceWidth; // on trace le 1er espace
                    Rect rect = new Rect(curX, curY, cellWidth, cellWidth);
                    curX += cellWidth;

                    //int tileindex = j * nbOfLine.intValue + i;
                    int tileindex = i * 3 + j;

                    //detec if left mouse pressed
                    bool isPaintingOverThis = isMouseDown && rect.Contains(Event.current.mousePosition);
                    if (isPaintingOverThis)
                    {
                        chunks.GetArrayElementAtIndex(tileindex).enumValueIndex = currentChunkSelected.enumValueIndex;
                    }

                    if (modules.arraySize == 0)
                    {
                        Color col = Color.magenta;
                        EditorGUI.DrawRect(rect, col);
                        //tilesProp.InsertArrayElementAtIndex(0);
                    }
                    else
                    {
                        if (chunks.arraySize > tileindex)
                        {
                            //if (tileindex == 0) Debug.Log("ERROR");
                            int enumIndexInPalette = chunks.GetArrayElementAtIndex(tileindex).enumValueIndex;
                            Color col = modules.GetArrayElementAtIndex(enumIndexInPalette).colorValue;
                            EditorGUI.DrawRect(rect, col);
                        }
                        else
                        {
                            //Debug.Log(chunks.arraySize);
                            //Debug.Log("changement d�t�ct� dans la taille de la grid");
                            //Debug.Log("actualisation de la grid size");
                            //update array size
                            for (int z = chunks.arraySize - 1; z < 3 * nbOfLine.intValue; z++)
                            {
                                chunks.InsertArrayElementAtIndex(z);
                            }
                            /*
                            chunks.arraySize = 3 * currentLevel.nbOfLine; // le GD a changer la taille de la grid depuis l'editor
                            for (int i = 0; i < chunks.arraySize; i++)
                            {
                                if chunks.InsertArrayElementAtIndex
                            }
                            */

                        }
                    }
                }

                curY += cellWidth;
                curY += spaceWidth;
            }

        }
        // End the scroll view that we began above.
        GUI.EndScrollView();

        #endregion


        serializedObject.ApplyModifiedProperties();

        Repaint(); // pour la mouse_position (Event)
    }

    private void ProcessEvents()
    {
        if (Event.current.type == EventType.MouseDown)
            isMouseDown = true;
        if (Event.current.type == EventType.MouseUp)
            isMouseDown = false;
    }

}
