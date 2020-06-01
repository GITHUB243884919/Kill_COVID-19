using Game.GlobalData;
using System.Collections;
using System.Collections.Generic;
using HillUFrame;
using HillUFrame.Common;
using HillUFrame.MiniGame;
using UnityEngine;

namespace Game
{
	public class EnemyManager : SingletonMono<EnemyManager>
    {
        public int spawnCD = 1000;

        VoidParamIntCDs multiCD;
		Vector3 spawnLeftTop {
            get { return GlobalDataManager.GetInstance().spawnLeftTop; }
            set { GlobalDataManager.GetInstance().spawnLeftTop = value; }
        }
		Vector3 spawnRightBottom {
            get { return GlobalDataManager.GetInstance().spawnRightBottom; }
            set { GlobalDataManager.GetInstance().spawnRightBottom = value; }
        }
        
		void Start()
		{
			spawnLeftTop = GetScreenPointInGround(new Vector3(0, Screen.height, 0));
			spawnRightBottom = GetScreenPointInGround(new Vector3(Screen.width, Screen.height * 0.7f, 0));

            multiCD = new VoidParamIntCDs();
			multiCD.AddCD(spawnCD, null, Callback_Spawn);
			multiCD.Run();
        }


		void Update()
		{
			multiCD.Tick((int)(Time.deltaTime * 1000));
		}

		protected void Callback_Spawn(IntCD CD, IVoidParam spawnCarParam)
		{
            SpawnWander();
            float p = Random.Range(0f, 1f);
            if (p >= 0.3f)
            {
                SpawnDropDown();
            }

            if (p >= 0.7f)
            {
                Vector3 pos = Vector3.zero;
                pos.x = Random.Range(spawnLeftTop.x, spawnRightBottom.x);
                pos.z = Random.Range(spawnRightBottom.z, spawnLeftTop.z);
                SpawnTrace(pos);
            }

            if (CD != null) 
			{
				CD.Reset();
				CD.Run();
			}
		}

		Vector3 GetScreenPointInGround(Vector3 pos)
		{
			Ray ray = Camera.main.ScreenPointToRay(pos);
			Vector3 groundPoint = HillUFrame.Math_F.GetIntersectWithLineAndGround(ray.origin, ray.direction);
#if UNITY_EDITOR
			GameObject.CreatePrimitive(PrimitiveType.Capsule).transform.position = groundPoint;
#endif
			return groundPoint;
		}

        public void SpawnWander()
        {
            var enemy = ResourceManager.GetInstance().LoadGameObject("EnemyWander");
            Vector3 pos = Vector3.zero;
            pos.x = Random.Range(spawnLeftTop.x, spawnRightBottom.x);
            pos.z = Random.Range(spawnRightBottom.z, spawnLeftTop.z);
            enemy.transform.position = pos;
            enemy.GetComponent<EnemyWander>().Active();
        }

        public void SpawnDropDown()
        {
            var enemy = ResourceManager.GetInstance().LoadGameObject("EnemyDropDown");
            Vector3 pos = Vector3.zero;
            pos.x = Random.Range(spawnLeftTop.x, spawnRightBottom.x);
            pos.z = Random.Range(spawnRightBottom.z, spawnLeftTop.z);
            enemy.transform.position = pos;
            enemy.GetComponent<EnemyDropDown>().Active();
        }

        public void SpawnTrace(Vector3 pos)
        {
            var enemy = ResourceManager.GetInstance().LoadGameObject("EnemyTrace");
            //Vector3 pos = Vector3.zero;
            //pos.x = Random.Range(spawnLeftTop.x, spawnRightBottom.x);
            //pos.z = Random.Range(spawnRightBottom.z, spawnLeftTop.z);
            enemy.transform.position = pos;
            enemy.GetComponent<EnemyTrace>().Active();
        }
	}

}
