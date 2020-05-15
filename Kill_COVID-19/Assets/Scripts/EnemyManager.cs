using System.Collections;
using System.Collections.Generic;
using UFrame;
using UFrame.MiniGame;
using UnityEngine;

namespace Game
{
	public class EnemyManager : MonoBehaviour
	{
		VoidParamIntCDs multiCD;
		Vector3 leftTop;
		Vector3 rightBottom;

		void Start()
		{
			leftTop = GetScreenPointInGround(new Vector3(0, Screen.height, 0));
			rightBottom = GetScreenPointInGround(new Vector3(Screen.width, Screen.height * 0.7f, 0));
			multiCD = new VoidParamIntCDs();
			multiCD.AddCD(1000, null, Callback_Spawn);
			multiCD.Run();
		}


		void Update()
		{
			multiCD.Tick((int)(Time.deltaTime * 1000));
		}

		protected void Callback_Spawn(IntCD CD, IVoidParam spawnCarParam)
		{
			var enemy = ResourceManager.GetInstance().LoadGameObject("enemy");
			Vector3 pos = Vector3.zero;
			pos.x = UnityEngine.Random.Range(leftTop.x, rightBottom.x);
			pos.z = UnityEngine.Random.Range(rightBottom.z, leftTop.z);
			enemy.transform.position = pos;
			enemy.GetComponent<Enemy>().Active();

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
