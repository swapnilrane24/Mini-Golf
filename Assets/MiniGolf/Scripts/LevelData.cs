using UnityEngine;

/// <summary>
/// Class which store level data
/// </summary>
[System.Serializable]
public class LevelData
{
    public int shotCount;           //maximum shot player can take 
    public GameObject levelPrefab;  //reference to level prefab
}
