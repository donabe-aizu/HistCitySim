using System;
using DonabeProject.Player;
using R3;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
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
                ecsPlayerInput.inputOrigin = click.Item1;
                ecsPlayerInput.inputDirection = click.Item2;
            }).AddTo(this);
        }
    }
}