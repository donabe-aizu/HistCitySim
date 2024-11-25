using Unity.Entities;
using UnityEngine;

namespace Citizen
{
    public class CitizenAuthoring : MonoBehaviour
    {
        [SerializeField] 
        private string citizenName;
        [SerializeField]
        private int _pocketMoney;
        [SerializeField]
        private int _appetite;
        [SerializeField]
        private float _moveSpeed;
        [SerializeField] 
        private Transform homeObject;
        [SerializeField]
        private bool _isNowMove = false;
        [SerializeField]
        private Vector3 _destination;
        
        class Baker : Baker<CitizenAuthoring>
        {
            public override void Bake(CitizenAuthoring src)
            {
                var data = new CitizenBase()
                {
                    //citizenName = src.citizenName,
                    pocketMoney = src._pocketMoney,
                    appetite = src._appetite,
                    moveSpeed = src._moveSpeed,
                    home = src.homeObject.position,
                    isNowMove = src._isNowMove,
                    destination = src._destination
                };
                AddComponent(GetEntity(TransformUsageFlags.Dynamic),data);
            } 
        }
    }
}