using Game.GlobalData;
using System.Collections;
using System.Collections.Generic;
using HillUFrame;
using UnityEngine;

namespace Game
{
    public class EnemyWander : MonoBehaviour
    {
        public float speed = 1f;
        bool isActive = false;

        float min_x {
            get { return GlobalDataManager.GetInstance().spawnLeftTop.x; }
        }

        float max_x {
            get { return GlobalDataManager.GetInstance().spawnRightBottom.x; }
        }

        float min_z {
            get { return GlobalDataManager.GetInstance().spawnLeftTop.z; }
        }

        float max_z {
            get { return GlobalDataManager.GetInstance().spawnRightBottom.z; }
        }

        bool isArrived = false;
        Vector3 target = Vector3.zero;
        Vector3 dir;

        Transform cacheTrans;

        private void Awake()
        {
            cacheTrans = transform;
        }

        public void Active()
        {
            isActive = true;

            NextTarget();
        }

        void NextTarget()
        {
            target.x = Random.Range(min_x, max_x);
            target.z = Random.Range(min_z, max_z);

            dir = (target - cacheTrans.position).normalized;
        }

        void Update()
        {
            //return;
            if (!isActive)
            {
                return;
            }

            //if (Math_F.Approximate3D(transform.position, target) || IsPassed(target))
            if (IsPassed(target))
            {
                //Debug.Log("NextTarget");
                EnemyManager.GetInstance().SpawnTrace(target);
                NextTarget();
            }

            //cacheTrans.position += dir * Time.deltaTime * speed;

            var delta = dir * Time.deltaTime * speed;
            var distance = target - cacheTrans.position;
            if (delta.sqrMagnitude > distance.sqrMagnitude)
            {
                cacheTrans.position += distance;
            }
            else
            {
                cacheTrans.position += delta;
            }

        }

        void OnCollisionEnter(Collision collision)
        {
            //Debug.LogFormat("OnCollisionEnter {0}", collision.gameObject.tag);
            if (collision.gameObject.CompareTag("Bullet"))
            {
                //Debug.LogFormat("OnCollisionEnter {0}", collision.gameObject.tag);
                isActive = false;
                GameObject.Destroy(gameObject);
                GameObject.Destroy(collision.gameObject);
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
