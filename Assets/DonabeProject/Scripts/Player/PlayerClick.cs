using System;
using DonabeProject.Manager;
using R3;
using UnityEngine;

namespace DonabeProject.Player
{
    public class PlayerClick : MonoBehaviour
    {
        public Camera camera_object;

        public Observable<(Vector3, Vector3)> OnClick => clickSubject;
        private Subject<(Vector3,Vector3)> clickSubject = new Subject<(Vector3, Vector3)>();

        void Update () {
            if(PlayerStatusHolder.I.isBlockClickRaycast) return;
            if (Input.GetMouseButtonDown(0)) //マウスがクリックされたら
            {
                var ray = camera_object.ScreenPointToRay(Input.mousePosition); //マウスのポジションを取得してRayに代入
                
                clickSubject.OnNext((ray.origin,ray.direction));
            }
        }
    }
}