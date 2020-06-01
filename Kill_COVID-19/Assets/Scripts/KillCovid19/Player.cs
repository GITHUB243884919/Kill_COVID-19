using System.Collections;
using System.Collections.Generic;
using HillUFrame;
using HillUFrame.MiniGame;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
	public class Player : MonoBehaviour
	{
		public float dragSensitivity = 1f;
		Transform cacheTrans;
		Vector3 dragMoveTo;
		VoidParamIntCDs multiCD;

		public int cdVal = 500;
		bool isFingerDown = false;
        //bool isDraging = false;
		void Awake()
		{
            FingerGestures.GlobalTouchFilter = IsFointOnUI;
            cacheTrans = transform;
			multiCD = new VoidParamIntCDs();
			multiCD.AddCD(cdVal, null, Callback_SpawnNormalBullet);
			multiCD.Run();
		}

		void Update()
		{
			if (!isFingerDown) 
			{
				return;
			}
			multiCD.Tick((int)(1000 * Time.deltaTime));

		}

        bool IsFointOnUI(int fingerIndex, Vector2 position)
        {
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                //屏幕触摸触发
#if UNITY_EDITOR
                if (EventSystem.current.IsPointerOverGameObject())
#else
                 if (EventSystem.current.IsPointerOverGameObject(fingerIndex))
#endif
                {
                    return false;
                }

                return true;
            }
            return false;
        }

        void OnDrag(DragGesture gesture)
		{
			if (gesture.State == GestureRecognitionState.InProgress && CouldDrag()) 
			{
				if (gesture.DeltaMove.SqrMagnitude() > 0) 
				{
                    //移动鼠标移动的delta
                    var v1 = GetScreenPointInGround(gesture.Position);
                    var v2 = GetScreenPointInGround(gesture.Position - gesture.DeltaMove);
                    transform.position += v1 - v2;
                    
                    //移动到鼠标
                    //Ray ray = Camera.main.ScreenPointToRay(gesture.Position);
                    //Vector3 groundPoint = UFrame.Math_F.GetIntersectWithLineAndGround(ray.origin, ray.direction);
                    //transform.position = groundPoint;
                }
            }
		}

        void OnFingerUp()
        {
            isFingerDown = false;
        }

        void OnFingerDown()
        {
            isFingerDown = true;
        }

        bool CouldDrag()
		{
			return true;
		}

		protected void Callback_SpawnNormalBullet(IntCD CD, IVoidParam spawnCarParam)
		{
			if (CD != null) 
			{
				CD.Reset();
				CD.Run();
			}

			if (!isFingerDown)
			{
				return;
			}

			var bullet = ResourceManager.GetInstance().LoadGameObject("bullet");
			bullet.transform.position = transform.position;
			bullet.GetComponent<Bullet>().Active();
		}

        Vector3 GetScreenPointInGround(Vector3 pos)
        {
            Ray ray = Camera.main.ScreenPointToRay(pos);
            Vector3 groundPoint = HillUFrame.Math_F.GetIntersectWithLineAndGround(ray.origin, ray.direction);
//#if UNITY_EDITOR
//            GameObject.CreatePrimitive(PrimitiveType.Capsule).transform.position = groundPoint;
//#endif
            return groundPoint;
        }

        public void Skill_1()
        {
            //一排子弹
            //Debug.LogError("skill1");
            int count = 11;
            var center = cacheTrans.position;
            float space = 1f;
            Vector3 start = center;
            start.x = center.x - count / 2 * space;
            //Debug.LogErrorFormat("{0} {1}", center, start);
            for (int i = 0; i < count; i++)
            {
                var bullet = ResourceManager.GetInstance().LoadGameObject("bullet");
                var pos = start + Vector3.right * space * i;
                //Debug.LogErrorFormat("{0}", pos);
                bullet.transform.position = pos;
                bullet.GetComponent<Bullet>().Active();
            }

        }

        public void Skill_2()
        {
            //散弹
            //Debug.LogError("skill2");

            float r = 3f;
            Vector3 dir = Vector3.zero;
            float startAngle = 30f;
            float endAngle = 90 + (90 - 30);
            int count = 11;
            float deltaAnagle = (endAngle - startAngle) / count;
            for (int i = 0; i < count; i++)
            {
                dir.x = Mathf.Cos(Math_F.AngleToRadian(startAngle + i * deltaAnagle));
                dir.z = Mathf.Sin(Math_F.AngleToRadian(startAngle + i * deltaAnagle));
                var bullet = ResourceManager.GetInstance().LoadGameObject("bullet_2");
                bullet.GetComponent<Bullet_2>().dir = dir;
                dir *= r;
                bullet.transform.position = dir + cacheTrans.position;
                bullet.GetComponent<Bullet_2>().Active();
            }


        }

    }

}
