using System;
using UnityEngine;

namespace DonabeProject.Manager
{
    public class InGameManager : MonoBehaviour
    {
        private PlayerStatusHolder _playerStatusHolder;
        
        private void Awake()
        {
            _playerStatusHolder = new PlayerStatusHolder();
        }
    }
}