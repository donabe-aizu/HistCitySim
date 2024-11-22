using Unity.Entities;
using UnityEngine;

namespace Building
{
    public class BuildingAuthoring : MonoBehaviour
    {
        [SerializeField]
        private BuildingType _buildingType;
        [SerializeField]
        private int _numberEmployee;
        
        class Baker : Baker<BuildingAuthoring>
        {
            public override void Bake(BuildingAuthoring src)
            {
                var data = new BuildingBase()
                {
                    buildingType = src._buildingType,
                    numberEmployee = src._numberEmployee
                };
                AddComponent(GetEntity(TransformUsageFlags.Dynamic),data);
            } 
        }
    }
}