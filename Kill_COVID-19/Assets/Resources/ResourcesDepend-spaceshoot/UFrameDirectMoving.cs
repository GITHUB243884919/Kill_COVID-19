using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFrame
{
	/// <summary>
	/// This script moves the attached object along the Y-axis with the defined speed
	/// </summary>
	public class UFrameDirectMoving : MonoBehaviour
	{

		[Tooltip("Moving speed on Y axis in local space")]
		public float speed;

		//moving the object with the defined speed
		private void Update()
		{
			//transform.Translate(Vector3.up * speed * Time.deltaTime);
			var delta = Vector3.forward * speed * Time.deltaTime;
			Debug.LogError(delta);
			transform.position += delta;
		}
	}

}
