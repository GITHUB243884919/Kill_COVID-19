using System.Collections;
using System.Collections.Generic;
using UFrame.MiniGame;
using UnityEngine;

namespace Game
{
	public class Player : MonoBehaviour
	{
		public float dragSensitivity = 1f;
		Transform cacheTrans;
		Vector3 dragMoveTo;

		bool isFingerDown = false;

		void Awake()
		{
			cacheTrans = transform;
		}

		void Update()
		{
			if (!isFingerDown) 
			{
				return;
			}

			var buillet = ResourceManager.GetInstance().LoadGameObject("buillet");
			buillet.transform.position = transform.position;
			buillet.GetComponent<Bullet>().Active();
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

					Ray ray = Camera.main.ScreenPointToRay(gesture.Position);
					Vector3 groundPoint = UFrame.Math_F.GetIntersectWithLineAndGround(ray.origin, ray.direction);
					transform.position = groundPoint;
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
	}

}
