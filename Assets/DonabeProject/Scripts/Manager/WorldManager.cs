using DonabeProject.Player;
using R3;
using Unity.Entities;
using UnityEngine;

namespace Manager
{
    public class WorldManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerClick _playerClick;
        private ECSPlayerInput ecsPlayerInput;
        
        void Start()
        {
            var world = World.DefaultGameObjectInjectionWorld;
            ecsPlayerInput = world.GetExistingSystemManaged<ECSPlayerInput>();

            _playerClick.OnClick.Subscribe(click =>
            {
                ecsPlayerInput.ClickRaycast(click.Item1,click.Item2);
            }).AddTo(this);
        }
    }
}