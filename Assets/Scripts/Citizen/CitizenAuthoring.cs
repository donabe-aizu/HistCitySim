using Unity.Entities;
using UnityEngine;

namespace Citizen
{
    public class CitizenAuthoring : MonoBehaviour
    {
        [SerializeField]
        private int _pocketMoney;
        [SerializeField]
        private int _appetite;
        [SerializeField]
        private float _moveSpeed;
        [SerializeField]
        private Vector3 _destination;
        
        class Baker : Baker<CitizenAuthoring>
        {
            public override void Bake(CitizenAuthoring src)
            {
                var data = new CitizenBase()
                {
                    pocketMoney = src._pocketMoney,
                    appetite = src._appetite,
                    moveSpeed = src._moveSpeed,
                    destination = src._destination
                };
                AddComponent(GetEntity(TransformUsageFlags.Dynamic),data);
            } 
        }
    }
}