using UnityEngine;

/// <summary>
/// This script attaches to ‘Background’ object, and would move it up if the object went down below the viewport border. 
/// This script is used for creating the effect of infinite movement. 
/// </summary>
namespace UFrame
{
	public class UFrameRepeatingBackground : MonoBehaviour
	{
		[Tooltip("vertical size of the sprite in the world space. Attach box collider2D to get the exact size")]
		public float verticalSize;

		Vector3 orgPos;
		Vector3 rightBottom;
		private void Awake()
		{
			rightBottom = GetScreenPointInGround(new Vector3(Screen.width, 0, 0));
			
			Debug.LogErrorFormat("{0}", rightBottom);

		}
		private void Update()
		{
			//if (transform.position.y < -verticalSize) //if sprite goes down below the viewport move the object up above the viewport
			//{
			//    RepositionBackground();
			//}

			//if sprite goes down below the viewport move the object up above the viewport
			//Debug.LogErrorFormat("{0}, {1}", transform.position, verticalSize);
			//if (transform.position.z <= -verticalSize)
			//{
			//	RepositionBackground();
			//}

			if (transform.position.z - orgPos.z < rightBottom.z) 
			{
				transform.position = orgPos;
			}
		}

		void RepositionBackground()
		{
			//Vector2 groundOffSet = new Vector2(0, verticalSize * 2f);
			//transform.position = (Vector2)transform.position + groundOffSet;
			Vector3 groundOffSet = Vector3.forward;
			groundOffSet.x = transform.position.x;
			groundOffSet.y = transform.position.y;
			groundOffSet.z = verticalSize * 2f;
			transform.position = groundOffSet;
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
