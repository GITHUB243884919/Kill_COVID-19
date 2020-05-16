using System.Collections;
using System.Collections.Generic;
using UFrame;
using UFrame.MiniGame;
using UnityEngine;

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
			cacheTrans = transform;
			multiCD = new VoidParamIntCDs();
			multiCD.AddCD(cdVal, null, Callback_Spawn);
			multiCD.Run();
		}

		void Update()
		{
			if (!isFingerDown) 
			{
				return;
			}
			////for(int i = 0; i < ; i++) 
			//{
			//	var buillet = ResourceManager.GetInstance().LoadGameObject("buillet");
			//	buillet.transform.position = transform.position;
			//	buillet.GetComponent<Bullet>().Active();
			//}
			multiCD.Tick((int)(1000 * Time.deltaTime));

		}

		void OnDrag(DragGesture gesture)
		{
			if (gesture.State == GestureRecognitionState.InProgress && CouldDrag()) 
			{
				if (gesture.DeltaMove.SqrMagnitude() > 0) 
				{
                    //Vector2 screenSpaceMove = dragSensitivity * gesture.DeltaMove;
                    //Vector3 worldSpaceMove = screenSpaceMove.x * cacheTrans.right + screenSpaceMove.y * cacheTrans.up;

                    //dragMoveTo.x += worldSpaceMove.x;
                    //dragMoveTo.z += worldSpaceMove.z;

                    //transform.position = dragMoveTo;

                    //Vector3 w = gesture.Position.x * cacheTrans.right + gesture.Position.y * cacheTrans.up;
                    //transform.position = new Vector3(w.x, 0, w.z);

                    //Vector2 screenSpaceMove = gesture.DeltaMove;
                    //Vector3 worldSpaceMove = screenSpaceMove.x * Camera.main.transform.right + screenSpaceMove.y * Camera.main.transform.up;
                    //Debug.LogErrorFormat("{0}, {1}", gesture.DeltaMove, worldSpaceMove);
                    //transform.position += worldSpaceMove;

                    var v1 = GetScreenPointInGround(gesture.Position);
                    var v2 = GetScreenPointInGround(gesture.Position - gesture.DeltaMove);
                    transform.position += v1 - v2;
                    //               Ray ray = Camera.main.ScreenPointToRay(gesture.Position);
                    //Vector3 groundPoint = UFrame.Math_F.GetIntersectWithLineAndGround(ray.origin, ray.direction);
                    //transform.position = groundPoint;
                }
            }
            //else if (gesture.State == GestureRecognitionState.Started)
            //{
            //    isDraging = true;
            //}
            //else if ()
            //{
            //    isDraging = false;
            //}


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

		protected void Callback_Spawn(IntCD CD, IVoidParam spawnCarParam)
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

			var buillet = ResourceManager.GetInstance().LoadGameObject("buillet");
			buillet.transform.position = transform.position;
			buillet.GetComponent<Bullet>().Active();
		}

        Vector3 GetScreenPointInGround(Vector3 pos)
        {
            Ray ray = Camera.main.ScreenPointToRay(pos);
            Vector3 groundPoint = UFrame.Math_F.GetIntersectWithLineAndGround(ray.origin, ray.direction);
//#if UNITY_EDITOR
//            GameObject.CreatePrimitive(PrimitiveType.Capsule).transform.position = groundPoint;
//#endif
            return groundPoint;
        }
    }

}
