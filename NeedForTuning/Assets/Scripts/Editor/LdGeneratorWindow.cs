using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LdGenerator : EditorWindow
{
    SerializedObject serializedObject;
    LevelProfile currentLevel;

    SerializedProperty nbOfLine, levelType, chunks, grounds, modules, sols, currentChunkSelected, currentGroundSelected;

    private bool isMouseDown;
    private int marginSize;
    private Vector2 curMousePosition;
    private int tileActu = 0;
    private Vector2 scrollPos;
    private bool isBrushModule = true;
    private bool isBrushGround = false;

    public void InitializeWindow(LevelProfile correspondingLevel)
    {
        currentLevel = correspondingLevel;
        serializedObject = new SerializedObject(currentLevel);

        nbOfLine = serializedObject.FindProperty("nbOfLine");
        levelType = serializedObject.FindProperty("levelType");
        chunks = serializedObject.FindProperty("chunks");
        grounds = serializedObject.FindProperty("grounds");
        sols = serializedObject.FindProperty("sols");
        modules = serializedObject.FindProperty("modules");
        currentChunkSelected = serializedObject.FindProperty("currentChunkSelected");
        currentGroundSelected = serializedObject.FindProperty("currentGroundSelected");

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

        //Sols brush :
        EditorGUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Ground Brush : " + currentGroundSelected.enumValueIndex + " => " + currentGroundSelected.enumNames[currentGroundSelected.enumValueIndex]);


        //changer de couleur
        if (GUILayout.Button("<<"))
        {
            //Debug.Log("Changer chunk left");
            if (currentGroundSelected.enumValueIndex != 0) currentGroundSelected.enumValueIndex--;
            else currentGroundSelected.enumValueIndex = (int)ChunkManager.RoadType.total - 1;
        }
        if (GUILayout.Button(">>"))
        {
            //Debug.Log("Changer chunk right");
            if (currentGroundSelected.enumValueIndex != (int)ChunkManager.RoadType.total - 1) currentGroundSelected.enumValueIndex++;
            else currentGroundSelected.enumValueIndex = 0;
        }

        isBrushGround = EditorGUILayout.Toggle("", isBrushGround);

        isBrushModule = !isBrushGround;

        EditorGUILayout.Space(10);

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(5);

        //Modules brush :

        EditorGUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Modules Brush : " + currentChunkSelected.enumValueIndex + " => " + currentChunkSelected.enumNames[currentChunkSelected.enumValueIndex]);


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

        isBrushModule = EditorGUILayout.Toggle("", isBrushModule);
        isBrushGround = !isBrushModule;

        EditorGUILayout.Space(10);

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(5);

        // Liste

        EditorGUILayout.Space(5);

        EditorGUILayout.PropertyField(sols);

        EditorGUILayout.Space(5);

        EditorGUILayout.PropertyField(modules);


        if (GUILayout.Button("Clear Grid"))
        {
            if (EditorUtility.DisplayDialog("Attention", "Etes vous sur de vouloir vider la grille ?", "Oui", "Non"))
            {
                chunks.ClearArray();
                chunks.arraySize = 3 * currentLevel.nbOfLine;
                grounds.ClearArray();
                grounds.arraySize = 3 * currentLevel.nbOfLine;
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
                    Rect rectModule = new Rect(curX + cellWidth / 4, curY + cellWidth / 4, cellWidth/2, cellWidth/2);
                    curX += cellWidth;

                    //int tileindex = j * nbOfLine.intValue + i;
                    int tileindex = i * 3 + j;

                    //detec if left mouse pressed
                    bool isPaintingOverThis = isMouseDown && rect.Contains(Event.current.mousePosition);
                    if (isPaintingOverThis && isBrushModule)
                    {
                        chunks.GetArrayElementAtIndex(tileindex).enumValueIndex = currentChunkSelected.enumValueIndex;
                    }
                    else if(isPaintingOverThis && isBrushGround)
                    {
                        grounds.GetArrayElementAtIndex(tileindex).enumValueIndex = currentGroundSelected.enumValueIndex;
                    }

                    if (modules.arraySize == 0)
                    {
                        Color col = Color.magenta;
                        EditorGUI.DrawRect(rect, col);
                        EditorGUI.DrawRect(rectModule, col);
                        //tilesProp.InsertArrayElementAtIndex(0);
                    }
                    else if (sols.arraySize == 0)
                    {
                        Color col = Color.magenta;
                        EditorGUI.DrawRect(rect, col);
                        EditorGUI.DrawRect(rectModule, col);
                    }
                    else
                    {
                        if (chunks.arraySize > tileindex && grounds.arraySize > tileindex)
                        {
                            //if (tileindex == 0) Debug.Log("ERROR");
                            int enumIndexInPalette = grounds.GetArrayElementAtIndex(tileindex).enumValueIndex;
                            int enumIndexInPaletteModule = chunks.GetArrayElementAtIndex(tileindex).enumValueIndex;
                            Color col = sols.GetArrayElementAtIndex(enumIndexInPalette).colorValue;
                            Color colModule = modules.GetArrayElementAtIndex(enumIndexInPaletteModule).colorValue;
                            EditorGUI.DrawRect(rect, col);
                            EditorGUI.DrawRect(rectModule, colModule);
                        }
                        else
                        {
                            //Debug.Log(chunks.arraySize);
                            //Debug.Log("changement détécté dans la taille de la grid");
                            //Debug.Log("actualisation de la grid size");
                            //update array size
                            for (int z = chunks.arraySize - 1; z < 3 * nbOfLine.intValue; z++)
                            {
                                chunks.InsertArrayElementAtIndex(z);
                            }

                            for (int z = grounds.arraySize - 1; z < 3 * nbOfLine.intValue; z++)
                            {
                                grounds.InsertArrayElementAtIndex(z);
                            }

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
