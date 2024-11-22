using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Splines;

public struct Road : IComponentData
{
    public Entity splineContainer;
}