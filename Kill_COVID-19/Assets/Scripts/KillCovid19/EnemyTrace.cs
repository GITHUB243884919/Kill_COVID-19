using System.Collections;
using System.Collections.Generic;
using HillUFrame;
using UnityEngine;

namespace Game
{
    public class EnemyTrace : MonoBehaviour
    {
        public float baseSpeed = 1f;
        public float acceleration = 1f;
        public float HP = 10;
        float speed;

        bool isActive = false;
        bool isArrived = false;

        Vector3 target = Vector3.zero;
        Vector3 dir;

        Transform cacheTrans;

        private void Awake()
        {
            cacheTrans = transform;
            speed = baseSpeed;
        }

        public void Active()
        {
            isActive = true;

            NextTarget();
        }

        void NextTarget()
        {
            target = Game.GlobalData.GlobalDataManager.GetInstance().transPlayer.position;

            dir = (target - cacheTrans.position).normalized;
        }

        void Update()
        {
            if (!isActive)
            {
                return;
            }

            speed = speed + acceleration;

            var delta = dir * Time.deltaTime * speed;
            cacheTrans.position += delta;
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                --HP;
                acceleration = acceleration * 0.5f;
                speed = speed * 0.5f;
                if (HP <= 0f)
                {
                    isActive = false;
                    GameObject.Destroy(gameObject);
                    GameObject.Destroy(collision.gameObject);
                }
            }
        }

        protected bool IsPassed(Vector3 pos)
        {
            //var localPos = cacheTrans.InverseTransformPoint(pos);
            ////return localPos.y <= 0;
            //return localPos.z <= 0;

            return Math_F.Approximate3D(cacheTrans.position, pos);
        }
    }

}
