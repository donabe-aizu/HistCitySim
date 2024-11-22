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
        public Vector3 home;
        public Entity destinationEntity;
        public bool isNowMove;
        
        public void IncreaseRandomDesire()
        {
            appetite++;
        }
        
        public void ChangeDestination(Vector3 position)
        {
            if (isNowMove) return; 
            destination = position;
        }
    }
}