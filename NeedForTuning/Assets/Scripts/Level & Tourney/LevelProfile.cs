using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Tool/New Level")]
public class LevelProfile : ScriptableObject
{
    public int levelID;
    public int nbOfLine;
    public ChunkManager.Modules[] chunks;
    public ChunkManager.RoadType[] grounds;
#if UNITY_EDITOR
    public ChunkManager.Modules currentChunkSelected;
    public ChunkManager.RoadType currentGroundSelected;
    public Color[] modules;
    public Color[] sols;
#endif

}
