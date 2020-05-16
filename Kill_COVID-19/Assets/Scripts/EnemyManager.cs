using System.Collections;
using System.Collections.Generic;
using UFrame;
using UFrame.MiniGame;
using UnityEngine;

namespace Game
{
	public class EnemyManager : MonoBehaviour
	{
        public int spawnCD = 1000;

        VoidParamIntCDs multiCD;
		Vector3 leftTop;
		Vector3 rightBottom;



		void Start()
		{
			leftTop = GetScreenPointInGround(new Vector3(0, Screen.height, 0));
			rightBottom = GetScreenPointInGround(new Vector3(Screen.width, Screen.height * 0.7f, 0));
            Debug.LogErrorFormat("min_x = {0}, max_x = {1}", leftTop.x, rightBottom.x);
            Debug.LogErrorFormat("min_z = {0}, max_z = {1}", rightBottom.z, leftTop.z);

            multiCD = new VoidParamIntCDs();
			multiCD.AddCD(spawnCD, null, Callback_Spawn);
			multiCD.Run();
            //Callback_Spawn(null, null);
        }


		void Update()
		{
			multiCD.Tick((int)(Time.deltaTime * 1000));
		}

		protected void Callback_Spawn(IntCD CD, IVoidParam spawnCarParam)
		{
            {
                var enemy = ResourceManager.GetInstance().LoadGameObject("enemy");
                Vector3 pos = Vector3.zero;
                pos.x = UnityEngine.Random.Range(leftTop.x, rightBottom.x);
                pos.z = UnityEngine.Random.Range(rightBottom.z, leftTop.z);
                enemy.transform.position = pos;
                //enemy.GetComponent<Enemy>().Active();
                enemy.GetComponent<Enemy>().Active();
            }
            {
                var enemy = ResourceManager.GetInstance().LoadGameObject("enemy_2");
                Vector3 pos = Vector3.zero;
                pos.x = UnityEngine.Random.Range(leftTop.x, rightBottom.x);
                pos.z = UnityEngine.Random.Range(rightBottom.z, leftTop.z);
                enemy.transform.position = pos;
                //enemy.GetComponent<Enemy>().Active();
                enemy.GetComponent<Enemy_2>().Active();
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
			Vector3 groundPoint = UFrame.Math_F.GetIntersectWithLineAndGround(ray.origin, ray.direction);
#if UNITY_EDITOR
			GameObject.CreatePrimitive(PrimitiveType.Capsule).transform.position = groundPoint;
#endif
			return groundPoint;
		}
	}

}
