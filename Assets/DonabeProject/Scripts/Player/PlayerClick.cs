using System;
using R3;
using UnityEngine;

namespace DonabeProject.Player
{
    public class PlayerClick : MonoBehaviour
    {
        public Camera camera_object;
        private RaycastHit hit;

        public Observable<(Vector3, Vector3)> OnClick => clickSubject;
        private Subject<(Vector3,Vector3)> clickSubject = new Subject<(Vector3, Vector3)>();

        void Update () {
            if (Input.GetMouseButtonDown(0)) //マウスがクリックされたら
            {
                Ray ray = camera_object.ScreenPointToRay(Input.mousePosition); //マウスのポジションを取得してRayに代入
                
                clickSubject.OnNext((ray.origin,ray.direction));

                if(Physics.Raycast(ray,out hit))  //マウスのポジションからRayを投げて何かに当たったらhitに入れる
                {
                    string objectName = hit.collider.gameObject.name; //オブジェクト名を取得して変数に入れる
                    Debug.Log(objectName); //オブジェクト名をコンソールに表示
                }
            }
        }
    }
}