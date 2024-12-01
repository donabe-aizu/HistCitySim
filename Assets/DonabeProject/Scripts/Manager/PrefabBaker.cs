using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace DonabeProject.Manager
{
    public class PrefabBaker : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> prefabs;

        class Baker : Baker<PrefabBaker>
        {
            public override void Bake(PrefabBaker authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                var sample = new PrefabEntityComponent();
                var buffer = AddBuffer<PrefabElement>(entity);
                foreach (var prefab in authoring.prefabs)
                {
                    Debug.Log(prefab.name);
                    buffer.Add(new PrefabElement
                    {
                        prefabEntity = GetEntity(prefab, TransformUsageFlags.Dynamic)
                    });
                }
                AddComponent(entity, sample);
            }
        }
    }
}