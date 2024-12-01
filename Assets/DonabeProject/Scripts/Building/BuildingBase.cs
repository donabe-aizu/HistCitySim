using Unity.Entities;
using UnityEngine;

namespace Building
{
    [System.Serializable]
    public struct BuildingBase : IComponentData
    {
        public int BuildingID;
        public BuildingType buildingType;
        public int numberEmployee;

        public void DecreaseAppetite(ref int appetite, int val)
        {
            appetite -= val;
            if (appetite <= 0)
            {
                appetite = 0;
            }
        }
    }
}