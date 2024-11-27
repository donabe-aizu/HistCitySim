using UnityEngine;

[CreateAssetMenu(fileName = "BuildingList", menuName = "Scriptable Objects/BuildingList")]
public class BuildingList : ScriptableObject
{
    public BuildingData[] buildings;
}
