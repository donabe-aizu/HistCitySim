using Unity.Entities;
using UnityEngine;

[System.Serializable]
public struct CubeTest : IComponentData
{
    public float speed;
}

public class CubeAuthoring : MonoBehaviour
{
    public float _speed = 1;

    class Baker : Baker<CubeAuthoring>
    {
        public override void Bake(CubeAuthoring src)
        {
            var data = new CubeTest() { speed = src._speed };
            AddComponent(GetEntity(TransformUsageFlags.Dynamic),data);
        }
    }
}