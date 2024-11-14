using UnityEngine;

[CreateAssetMenu(fileName = "NewMapData", menuName = "Map Data")]
public class MapData : ScriptableObject {
    public string mapName;
    public Sprite mapPreview;
    public string sceneName;
}