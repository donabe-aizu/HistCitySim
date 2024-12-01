using System;
using Unity.Entities;
using UnityEngine;

namespace Building
{
    public class BuildingAuthoring : MonoBehaviour
    {
        public int BuildingID;
        public BuildingType _buildingType;
        public int _numberEmployee;
        
        class Baker : Baker<BuildingAuthoring>
        {
            public override void Bake(BuildingAuthoring src)
            {
                var data = new BuildingBase()
                {
                    buildingType = src._buildingType,
                    numberEmployee = src._numberEmployee,
                    BuildingID = src.BuildingID
                };
                AddComponent(GetEntity(TransformUsageFlags.Dynamic),data);
            } 
        }
    }
}