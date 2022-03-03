using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Tool/New Level")]
public class LevelProfile : ScriptableObject
{
    public int levelID;
    public int nbOfLine;
    public uint nbObstacle;
    public int nbLineInLD = 5;
    public ChunkManager.Modules[] chunks;
#if UNITY_EDITOR
    public ChunkManager.Modules currentChunkSelected;
    public Color[] modules;
#endif

}
