using Unity.Entities;

namespace DonabeProject.Manager
{
    [System.Serializable]
    public struct PrefabEntityComponent : IComponentData
    {
    }
    
    public struct PrefabElement : IBufferElementData
    {
        public Entity prefabEntity;
    }
}