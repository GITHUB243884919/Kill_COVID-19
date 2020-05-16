using System.Collections;
using System.Collections.Generic;
using UFrame;
using UFrame.MiniGame;
using UnityEngine;

namespace Game
{
	public class Enemy : MonoBehaviour
	{
		public float speed = 1f;
		bool isActive = false;

		public void Active()
		{
			isActive = true;
		}

		public void Update()
		{
			if (!isActive) {
				return;
			}
			transform.position += Vector3.back * Time.deltaTime * speed;
		}

		void OnCollisionEnter(Collision collision)
		{
			//Debug.LogFormat("OnCollisionEnter {0}", collision.gameObject.tag);
			if (collision.gameObject.CompareTag("Buillet")) 
			{
				//Debug.LogFormat("OnCollisionEnter {0}", collision.gameObject.tag);
				isActive = false;
				GameObject.Destroy(gameObject);
				GameObject.Destroy(collision.gameObject);
			}
		}
	}
}
