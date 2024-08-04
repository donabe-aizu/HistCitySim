using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using UnityEngine.Splines;

public class RoadAuthoring : MonoBehaviour
{
    [SerializeField] private Entity _splineContainer;

    class Baker : Baker<RoadAuthoring>
    {
        public override void Bake(RoadAuthoring authoring)
        {
            var data = new Road()
            {
                splineContainer = authoring._splineContainer
            };
            AddComponent(GetEntity(GetComponent<SplineContainer>(),TransformUsageFlags.None),data);
        }
    }
}