using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class Bullet_2 : MonoBehaviour
	{

		public float speed = 1f;
		bool isActive = false;
        Transform cacheTrans;

        public Vector3 dir;

        private void Awake()
        {
            cacheTrans = transform;
        }

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
            cacheTrans.position += dir * Time.deltaTime * speed;
		}

	}
}
