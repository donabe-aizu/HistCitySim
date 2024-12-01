using UnityEngine;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/BuildingData")]
public class BuildingData : ScriptableObject
{
    public Texture2D thumbnail;
    public string buildingName;
    public int buildingID;
}
