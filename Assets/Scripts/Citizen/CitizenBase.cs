using Unity.Entities;
using UnityEngine;

namespace Citizen
{
    [System.Serializable]
    public struct CitizenBase : IComponentData
    {
        public int pocketMoney;
        public int appetite;
        public float moveSpeed;
        public Vector3 destination;
    }
}