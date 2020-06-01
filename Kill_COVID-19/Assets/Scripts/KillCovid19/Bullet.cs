using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class Bullet : MonoBehaviour
	{

		public float speed = 1f;
		bool isActive = false;

		public void Active()
		{
			isActive = true;
		}

		public void Update()
		{
			if (!isActive) 
			{
				return;
			}
			transform.position += Vector3.forward * Time.deltaTime * speed;
		}

		//private void OnDestroy()
		//{
		//	Debug.Log("OnDestroy");
		//}
	}
}
